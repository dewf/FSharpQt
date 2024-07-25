module FSharpQt.Widgets.Menu

open System
open FSharpQt
open FSharpQt.BuilderNode
open FSharpQt.Reactor
open Microsoft.FSharp.Core
open Org.Whatever.MinimalQtForFSharp
open Extensions

open FSharpQt.Attrs
open FSharpQt.MiscTypes

type private Signal =
    | AboutToHide
    | AboutToShow
    | Hovered of action: ActionProxy
    | Triggered of action: ActionProxy
    
type internal Attr =
    | IconAttr of icon: Icon
    | SeparatorsCollapsible of state: bool
    | TearOffEnabled of state: bool
    | Title of title: string
    | ToolTipsVisible of visible: bool
with
    interface IAttr with
        override this.AttrEquals other =
            match other with
            | :? Attr as otherAttr ->
                this = otherAttr
            | _ ->
                false
        override this.Key =
            match this with
            | IconAttr _ -> "menu:icon"
            | SeparatorsCollapsible _ -> "menu:separatorscollapsible"
            | TearOffEnabled _ -> "menu:tearoffenabled"
            | Title _ -> "menu:title"
            | ToolTipsVisible _ -> "menu:tooltipsvisible"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyMenuAttr(this)
            | _ ->
                printfn "warning: Menu.Attr couldn't ApplyTo() unknown object type [%A]" target
                
and internal AttrTarget =
    interface
        inherit Widget.AttrTarget
        abstract member ApplyMenuAttr: Attr -> unit
    end
                
type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)
                
type Props<'msg>() =
    inherit Widget.Props<'msg>()
    
    let mutable onAboutToHide: 'msg option = None
    let mutable onAboutToShow: 'msg option = None
    let mutable onHovered: (ActionProxy -> 'msg) option = None
    let mutable onTriggered: (ActionProxy -> 'msg) option = None
    
    member internal this.SignalMask = enum<Menu.SignalMask> (int this._signalMask)
    
    member this.OnAboutToHide with set value =
        onAboutToHide <- Some value
        this.AddSignal(int Menu.SignalMask.AboutToHide)
    member this.OnAboutToShow with set value =
        onAboutToShow <- Some value
        this.AddSignal(int Menu.SignalMask.AboutToShow)
    member this.OnHovered with set value =
        onHovered <- Some value
        this.AddSignal(int Menu.SignalMask.Hovered)
    member this.OnTriggered with set value =
        onTriggered <- Some value
        this.AddSignal(int Menu.SignalMask.Triggered)
    
    member internal this.SignalMapList =
        let thisFunc = function
            | AboutToHide ->
                onAboutToHide
            | AboutToShow ->
                onAboutToShow
            | Hovered action ->
                onHovered
                |> Option.map (fun f -> f action)
            | Triggered action ->
                onTriggered
                |> Option.map (fun f -> f action)
        // we inherit from Widget, so prepend to its signals
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList
    
    member this.Icon with set value =
        this.PushAttr(IconAttr value)
        
    member this.SeparatorsCollapsible with set value =
        this.PushAttr(SeparatorsCollapsible value)
        
    member this.TearOffEnabled with set value =
        this.PushAttr(TearOffEnabled value)
        
    member this.Title with set value =
        this.PushAttr(Title value)
        
    member this.ToolTipsVisible with set value =
        this.PushAttr(ToolTipsVisible value)
    
type internal ItemInternal<'msg> =
    | ActionItem of action: IActionNode<'msg>
    | Separator
    | Nothing
    
type MenuItem<'msg> internal(item: ItemInternal<'msg>) =
    new(action: IActionNode<'msg>) =
        MenuItem(ActionItem action)
    new(?separator: bool) =
        match defaultArg separator true with
        | true -> MenuItem(Separator)
        | false -> MenuItem(Nothing)
    // internal stufF:
    member internal this.ContentKey =
        match item with
        | ActionItem action -> action.ContentKey
        | Separator -> Separator
        | Nothing -> Nothing
    member internal this.MaybeNode =
        match item with
        | ActionItem action -> Some action
        | Separator -> None
        | Nothing -> None
    member internal this.AddTo (menu: Menu.Handle) =
        match item with
        | ActionItem action ->
            menu.AddAction(action.Action)
        | Separator ->
            menu.AddSeparator()
            |> ignore
        | Nothing ->
            ()
        
type Separator<'msg>() =
    inherit MenuItem<'msg>(ItemInternal.Separator)
    
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit Widget.ModelCore<'msg>(dispatch)
    let mutable menu: Menu.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<Menu.SignalMask> 0
    // no binding guards
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.Menu
        with get() = menu
        and set value =
            this.Widget <- value
            menu <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "Menu.ModelCore.SignalMaps: wrong func type"
            // assign the remainder up the hierarchy
            base.SignalMaps <- etc
        | _ ->
            failwith "Menu.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            menu.SetSignalMask(value)
            currentMask <- value
            
    interface AttrTarget with
        member this.ApplyMenuAttr attr =
            match attr with
            | IconAttr icon ->
                menu.SetIcon(icon.QtValue)
            | SeparatorsCollapsible state ->
                menu.SetSeparatorsCollapsible(state)
            | TearOffEnabled state ->
                menu.SetTearOffEnabled(state)
            | Title title ->
                menu.SetTitle(title)
            | ToolTipsVisible visible ->
                menu.SetToolTipsVisible(visible)
        
    interface Menu.SignalHandler with
        // object =========================
        member this.Destroyed(obj: Object.Handle) =
            (this :> Object.SignalHandler).Destroyed(obj)
        member this.ObjectNameChanged(name: string) =
            (this :> Object.SignalHandler).ObjectNameChanged(name)
        // widget =========================
        member this.CustomContextMenuRequested pos =
            (this :> Widget.SignalHandler).CustomContextMenuRequested pos
        member this.WindowIconChanged icon =
            (this :> Widget.SignalHandler).WindowIconChanged icon
        member this.WindowTitleChanged title =
            (this :> Widget.SignalHandler).WindowTitleChanged title
        // menu ===========================
        member this.AboutToHide() =
            signalDispatch AboutToHide
        member this.AboutToShow() =
            signalDispatch AboutToShow
        member this.Hovered action =
            signalDispatch (ActionProxy(action) |> Hovered)
        member this.Triggered action =
            signalDispatch (ActionProxy(action) |> Triggered)
            
    interface IDisposable with
        member this.Dispose() =
            menu.Dispose()
    
type private Model<'msg>(dispatch: 'msg -> unit) as this =
    inherit ModelCore<'msg>(dispatch)
    let menu = Menu.Create(this)
    do
        this.Menu <- menu
        
    member this.Refill(items: MenuItem<'msg> list) =
        menu.Clear()
        for item in items do
            item.AddTo(menu)

let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: Menu.SignalMask) =
    let model = new Model<'msg>(dispatch)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: Menu.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps<- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()
    
type MenuBinding internal(handle: Menu.Handle) =
    interface IViewBinding
    member this.Popup(loc: Point) =
        handle.Popup(loc.QtValue)
    
let bindNode (name: string) (map: Map<string, IViewBinding>) =
    match map.TryFind name with
    | Some (:? MenuBinding as menu) ->
        menu
    | _ ->
        failwith "Menu.bindNode fail"

type Menu<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>

    member val Items: MenuItem<'msg> list = [] with get, set
    
    member private this.MigrateContent (leftMenu: Menu<'msg>) =
        let leftItems =
            leftMenu.Items
            |> List.map (_.ContentKey)
        let thisItems =
            this.Items
            |> List.map (_.ContentKey)
        if leftItems <> thisItems then
            this.model.Refill(this.Items)
        else
            ()
        
    interface IMenuNode<'msg> with
        override this.Dependencies =
            // see long note on same BoxLayout method
            this.Items
            |> List.zipWithIndex
            |> List.choose (fun (i, item) ->
                item.MaybeNode
                |> Option.map (fun node -> IntKey i, node :> IBuilderNode<'msg>))
            
        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask
            
        override this.AttachDeps () =
            this.model.Refill(this.Items)
            
        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let leftNode = (left :?> Menu<'msg>)
            let nextAttrs =
                diffAttrs leftNode.Attrs this.Attrs
                |> createdOrChanged
            this.model <- migrate leftNode.model nextAttrs this.SignalMapList this.SignalMask
            this.MigrateContent(leftNode)
            
        override this.Dispose() =
            (this.model :> IDisposable).Dispose()
            
        override this.Menu =
            this.model.Menu
            
        override this.ContentKey =
            this.model.Menu
            
        override this.Attachments =
            this.Attachments

        override this.Binding =
            this.MaybeBoundName
            |> Option.map (fun name ->
                name, MenuBinding(this.model.Menu))

let showMenuAtPoint name point relativeTo =
    Cmd.ViewExec (fun bindings ->
        viewexec bindings {
            let! menu = bindNode name
            let! relativeToWidget = Widget.bindNode relativeTo
            let p2 = relativeToWidget.MapToGlobal(point)
            menu.Popup(p2)
        })
