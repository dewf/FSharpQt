module FSharpQt.Widgets.MenuBar

open System
open FSharpQt.BuilderNode
open Org.Whatever.MinimalQtForFSharp

open FSharpQt.Attrs
open FSharpQt.MiscTypes

type private Signal =
    | Hovered of action: ActionProxy
    | Triggered of action: ActionProxy

type internal Attr =
    | DefaultUp of state: bool
    | NativeMenuBar of state: bool
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
            | DefaultUp _ -> "menubar:defaultup"
            | NativeMenuBar _ -> "menubar:nativemenubar"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyMenuBarAttr(this)
            | _ ->
                printfn "warning: MenuBar.Attr couldn't ApplyTo() unknown object type [%A]" target
                
and internal AttrTarget =
    interface
        inherit Widget.AttrTarget
        abstract member ApplyMenuBarAttr: Attr -> unit
    end
                
type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)
                
type Props<'msg>() =
    inherit Widget.Props<'msg>()
    
    let mutable onHovered: (ActionProxy -> 'msg) option = None
    let mutable onTriggered: (ActionProxy -> 'msg) option = None
    
    member internal this.SignalMask = enum<MenuBar.SignalMask> (int this._signalMask)

    member this.OnHovered with set value =
        onHovered <- Some value
        this.AddSignal(int MenuBar.SignalMask.Hovered)
        
    member this.OnTriggered with set value =
        onTriggered <- Some value
        this.AddSignal(int MenuBar.SignalMask.Triggered)

    member internal this.SignalMapList =
        let thisFunc = function
            | Hovered action ->
                onHovered
                |> Option.map (fun f -> f action)
            | Triggered action ->
                onTriggered
                |> Option.map (fun f -> f action)
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList
    
    member this.DefaultUp with set value =
        this.PushAttr(DefaultUp value)
        
    member this.NativeMenuBar with set value =
        this.PushAttr(NativeMenuBar value)
        
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit Widget.ModelCore<'msg>(dispatch)
    let mutable menuBar: MenuBar.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<MenuBar.SignalMask> 0
    // no binding guards
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.MenuBar
        with get() = menuBar
        and set value =
            this.Widget <- value
            menuBar <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "MenuBar.ModelCore.SignalMaps: wrong func type"
            // assign the remainder up the hierarchy
            base.SignalMaps <- etc
        | _ ->
            failwith "MenuBar.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            menuBar.SetSignalMask(value)
            currentMask <- value
            
    interface AttrTarget with
        member this.ApplyMenuBarAttr attr =
            match attr with
            | DefaultUp state ->
                menuBar.SetDefaultUp(state)
            | NativeMenuBar state ->
                menuBar.SetNativeMenuBar(state)
        
    interface MenuBar.SignalHandler with
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
        // menuBar ========================
        override this.Hovered action =
            signalDispatch (ActionProxy(action) |> Hovered)
        override this.Triggered action =
            signalDispatch (ActionProxy(action) |> Triggered)
            
    interface IDisposable with
        member this.Dispose() =
            menuBar.Dispose()
    
type private Model<'msg>(dispatch: 'msg -> unit) as this =
    inherit ModelCore<'msg>(dispatch)
    let mutable menuBar = MenuBar.Create(this)
    do
        this.MenuBar <- menuBar
    
    member this.Refill(menus: Menu.Handle list) =
        menuBar.Clear()
        for menu in menus do
            menuBar.AddMenu(menu)
    
let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: MenuBar.SignalMask) =
    let model = new Model<'msg>(dispatch)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: MenuBar.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()
    
type MenuBar<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    member val Menus: IMenuNode<'msg> list = [] with get, set
    
    member private this.MigrateContent (leftMenuBar: MenuBar<'msg>) =
        let leftKeys =
            leftMenuBar.Menus
            |> List.map (_.ContentKey)
        let thisKeys =
            this.Menus
            |> List.map (_.ContentKey)
        if leftKeys <> thisKeys then
            let menuHandles =
                this.Menus
                |> List.map (_.Menu)
            this.model.Refill(menuHandles)
        else
            ()
            
    interface IMenuBarNode<'msg> with
        override this.Dependencies =
            // see long note on same BoxLayout method
            this.Menus
            |> List.mapi (fun i menu -> (IntKey i, menu :> IBuilderNode<'msg>))
            
        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask
            
        override this.AttachDeps() =
            let menuHandles =
                this.Menus
                |> List.map (_.Menu)
            this.model.Refill(menuHandles)
            
        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let leftNode = (left :?> MenuBar<'msg>)
            let nextAttrs =
                diffAttrs leftNode.Attrs this.Attrs
                |> createdOrChanged
            this.model <- migrate leftNode.model nextAttrs this.SignalMapList this.SignalMask
            this.MigrateContent(leftNode)
            
        override this.Dispose() =
            (this.model :> IDisposable).Dispose()
            
        override this.MenuBar =
            this.model.MenuBar
            
        override this.ContentKey =
            this.model.MenuBar
        
        override this.Attachments =
            this.Attachments

        override this.Binding = None
