module FSharpQt.Widgets.GroupBox

open FSharpQt.Attrs
open FSharpQt.BuilderNode
open System
open FSharpQt.MiscTypes
open Org.Whatever.MinimalQtForFSharp

type private Signal =
    | Clicked of checked_: bool
    | Toggled of state: bool

type internal Attr =
    | Alignment of align: Alignment
    | Checkable of state: bool
    | Checked of state: bool
    | Flat of state: bool
    | Title of title: string
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
            | Alignment _ -> "groupbox:alignment"
            | Checkable _ -> "groupbox:checkable"
            | Checked _ -> "groupbox:checked"
            | Flat _ -> "groupbox:flat"
            | Title _ -> "groupbox:title"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyGroupBoxAttr(this)
            | _ ->
                printfn "warning: GroupBox.Attr couldn't ApplyTo() unknown target type [%A]" target
                
and internal AttrTarget =
    interface
        inherit Widget.AttrTarget
        abstract member ApplyGroupBoxAttr: Attr -> unit
    end
    
type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)

type Props<'msg>() =
    inherit Widget.Props<'msg>()
    
    let mutable onClicked: (bool -> 'msg) option = None
    let mutable onToggled: (bool -> 'msg) option = None
    
    member internal this.SignalMask = enum<GroupBox.SignalMask> (int this._signalMask)
    
    member this.OnClicked with set value =
        onClicked <- Some value
        this.AddSignal(int GroupBox.SignalMask.Clicked)
        
    member this.OnToggled with set value =
        onToggled <- Some value
        this.AddSignal(int GroupBox.SignalMask.Toggled)
        
    member internal this.SignalMapList =
        let thisFunc = function
            | Clicked checked_ ->
                onClicked
                |> Option.map (fun f -> f checked_)
            | Toggled state ->
                onToggled
                |> Option.map (fun f -> f state)
        // prepend to parent signal map funcs
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList
        
    member this.Alignment with set value =
        this.PushAttr(Alignment value)

    member this.Checkable with set value =
        this.PushAttr(Checkable value)

    member this.Checked with set value =
        this.PushAttr(Checked value)

    member this.Flat with set value =
        this.PushAttr(Flat value)

    member this.Title with set value =
        this.PushAttr(Title value)
        
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit Widget.ModelCore<'msg>(dispatch)
    let mutable groupBox: GroupBox.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<GroupBox.SignalMask> 0
    
    // binding guards
    let mutable lastCheckedState = false
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.GroupBox
        with get() = groupBox
        and set value =
            // assign to base
            this.Widget <- value
            groupBox <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "GroupBox.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "GroupBox.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            groupBox.SetSignalMask(value)
            currentMask <- value

    interface AttrTarget with
        member this.ApplyGroupBoxAttr attr =
            match attr with
            | Alignment align ->
                groupBox.SetAlignment(align.QtValue)
            | Checkable state ->
                groupBox.SetCheckable(state)
            | Checked state ->
                if state <> lastCheckedState then
                    lastCheckedState <- state
                    groupBox.SetChecked(state)
            | Flat state ->
                groupBox.SetFlat(state)
            | Title title ->
                groupBox.SetTitle(title)
                
    interface GroupBox.SignalHandler with
        // Object =========================
        member this.Destroyed(obj: Object.Handle) =
            (this :> Object.SignalHandler).Destroyed(obj)
        member this.ObjectNameChanged(name: string) =
            (this :> Object.SignalHandler).ObjectNameChanged(name)
        // Widget =========================
        member this.CustomContextMenuRequested pos =
            (this :> Widget.SignalHandler).CustomContextMenuRequested pos
        member this.WindowIconChanged icon =
            (this :> Widget.SignalHandler).WindowIconChanged icon
        member this.WindowTitleChanged title =
            (this :> Widget.SignalHandler).WindowTitleChanged title
        // GroupBox =======================
        member this.Clicked state =
            lastCheckedState <- state
            signalDispatch (Clicked state)
        member this.Toggled state =
            lastCheckedState <- state
            signalDispatch (Toggled state)
            
    interface IDisposable with
        member this.Dispose() =
            groupBox.Dispose()
            
type private Model<'msg>(dispatch: 'msg -> unit) as this =
    inherit ModelCore<'msg>(dispatch)
    let groupBox = GroupBox.Create(this)
    do
        this.GroupBox <- groupBox
            
    member this.RemoveLayout() =
        let existing =
            groupBox.GetLayout()
        existing.RemoveAll()
        groupBox.SetLayout(null)
        
    member this.AddLayout(layout: Layout.Handle) =
        groupBox.SetLayout(layout)

let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: GroupBox.SignalMask) =
    let model = new Model<'msg>(dispatch)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: GroupBox.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()

type GroupBox<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    let mutable maybeLayout: ILayoutNode<'msg> option = None
    member this.Layout with set value = maybeLayout <- Some value
    
    member private this.MigrateContent (changeMap: Map<DepsKey, DepsChange>) =
        match changeMap.TryFind (StrKey "layout") with
        | Some change ->
            match change with
            | Unchanged ->
                ()
            | Added ->
                this.model.AddLayout(maybeLayout.Value.Layout)
            | Removed ->
                this.model.RemoveLayout()
            | Swapped ->
                this.model.RemoveLayout()
                this.model.AddLayout(maybeLayout.Value.Layout)
        | None ->
            // neither side had a layout
            ()
            
    interface IWidgetNode<'msg> with
        override this.Dependencies =
            maybeLayout
            |> Option.map (fun content -> (StrKey "layout", content :> IBuilderNode<'msg>))
            |> Option.toList
        
        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask
            
        override this.AttachDeps () =
            maybeLayout
            |> Option.iter (fun node -> this.model.AddLayout(node.Layout))

        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> GroupBox<'msg>)
            let nextAttrs = diffAttrs left'.Attrs this.Attrs |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask
            this.MigrateContent (depsChanges |> Map.ofList)
            
        override this.Dispose() =
            (this.model :> IDisposable).Dispose()

        override this.Widget =
            this.model.GroupBox
            
        override this.ContentKey =
            this.model.GroupBox
            
        override this.Attachments =
            this.Attachments

        override this.Binding = None
