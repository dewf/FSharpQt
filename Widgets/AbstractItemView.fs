module FSharpQt.Widgets.AbstractItemView

open System
open FSharpQt.Attrs
open FSharpQt.MiscTypes
open Org.Whatever.MinimalQtForFSharp

type private Signal =
    | Activated of index: ModelIndexProxy
    | Clicked of index: ModelIndexProxy
    | DoubleClicked of index: ModelIndexProxy
    | Entered of index: ModelIndexProxy
    | IconSizeChanged of size: Size
    | Pressed of index: ModelIndexProxy
    | ViewportEntered

type DragDropMode =
    | NoDragDrop
    | DragOnly
    | DropOnly
    | DragDrop
    | InternalMove
with
    member internal this.QtValue =
        match this with
        | NoDragDrop -> AbstractItemView.DragDropMode.NoDragDrop
        | DragOnly -> AbstractItemView.DragDropMode.DragOnly
        | DropOnly -> AbstractItemView.DragDropMode.DropOnly
        | DragDrop -> AbstractItemView.DragDropMode.DragDrop
        | InternalMove -> AbstractItemView.DragDropMode.InternalMove
    
type EditTriggers =
    | CurrentChanged
    | DoubleClicked
    | SelectedClicked
    | EditKeyPressed
    | AnyKeyPressed
with
    static member internal None = enum<AbstractItemView.EditTriggers> 0
    static member internal All = AbstractItemView.EditTriggers.AllEditTriggers
    static member internal QtFlagsFrom (values: EditTriggers seq) =
        (enum<AbstractItemView.EditTriggers> 0, values)
        ||> Seq.fold (fun acc item ->
            let flag =
                match item with
                | CurrentChanged -> AbstractItemView.EditTriggers.CurrentChanged
                | DoubleClicked -> AbstractItemView.EditTriggers.DoubleClicked
                | SelectedClicked -> AbstractItemView.EditTriggers.SelectedClicked
                | EditKeyPressed -> AbstractItemView.EditTriggers.EditKeyPressed
                | AnyKeyPressed -> AbstractItemView.EditTriggers.AnyKeyPressed
            acc ||| flag)

type ScrollMode =
    | ScrollPerItem
    | ScrollPerPixel
with
    member internal this.QtValue =
        match this with
        | ScrollPerItem -> AbstractItemView.ScrollMode.ScrollPerItem
        | ScrollPerPixel -> AbstractItemView.ScrollMode.ScrollPerPixel
    
type SelectionBehavior =
    | SelectItems
    | SelectRows
    | SelectColumns
with
    member internal this.QtValue =
        match this with
        | SelectItems -> AbstractItemView.SelectionBehavior.SelectItems
        | SelectRows -> AbstractItemView.SelectionBehavior.SelectRows
        | SelectColumns -> AbstractItemView.SelectionBehavior.SelectColumns
    
type SelectionMode =
    | NoSelection
    | SingleSelection
    | MultiSelection
    | ExtendedSelection
    | ContiguousSelection
with
    member internal this.QtValue =
        match this with
        | NoSelection -> AbstractItemView.SelectionMode.NoSelection
        | SingleSelection -> AbstractItemView.SelectionMode.SingleSelection
        | MultiSelection -> AbstractItemView.SelectionMode.MultiSelection
        | ExtendedSelection -> AbstractItemView.SelectionMode.ExtendedSelection
        | ContiguousSelection -> AbstractItemView.SelectionMode.ContiguousSelection

type internal Attr =
    | AlternatingRowColors of state: bool
    | AutoScroll of state: bool
    | AutoScrollMargin of margin: int
    | DefaultDropAction of action: DropAction
    | DragDropMode of mode: DragDropMode
    | DragDropOverwriteMode of mode: bool
    | DragEnabled of enabled: bool
    | EditTriggers of triggers: Set<EditTriggers>
    | HorizontalScrollMode of mode: ScrollMode
    | IconSize of size: Size
    | SelectionBehavior of behavior: SelectionBehavior
    | SelectionMode of mode: SelectionMode
    | DropIndicatorShown of state: bool
    | TabKeyNavigation of state: bool
    | TextElideMode of mode: TextElideMode
    | VerticalScrollMode of mode: ScrollMode
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
            | AlternatingRowColors _ -> "abstractitemview:alternatingrowcolors"
            | AutoScroll _ -> "abstractitemview:autoscroll"
            | AutoScrollMargin _ -> "abstractitemview:autoscrollmargin"
            | DefaultDropAction _ -> "abstractitemview:defaultdropaction"
            | DragDropMode _ -> "abstractitemview:dragdropmode"
            | DragDropOverwriteMode _ -> "abstractitemview:dragdropoverwritemode"
            | DragEnabled _ -> "abstractitemview:dragenabled"
            | EditTriggers _ -> "abstractitemview:edittriggers"
            | HorizontalScrollMode _ -> "abstractitemview:horizontalscrollmode"
            | IconSize _ -> "abstractitemview:iconsize"
            | SelectionBehavior _ -> "abstractitemview:selectionbehavior"
            | SelectionMode _ -> "abstractitemview:selectionmode"
            | DropIndicatorShown _ -> "abstractitemview:dropindicatorshown"
            | TabKeyNavigation _ -> "abstractitemview:tabkeynavigation"
            | TextElideMode _ -> "abstractitemview:textelidemode"
            | VerticalScrollMode _ -> "abstractitemview:verticalscrollmode"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyAbstractItemViewAttr(this)
            | _ ->
                printfn "warning: AbstractItemView.Attr couldn't ApplyTo() unknown target type [%A]" target
                
and internal AttrTarget =
    interface
        inherit AbstractScrollArea.AttrTarget
        abstract member ApplyAbstractItemViewAttr: Attr -> unit
    end

type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)
    
type Props<'msg>() =
    inherit AbstractScrollArea.Props<'msg>()
    
    let mutable onActivated: (ModelIndexProxy -> 'msg) option = None
    let mutable onClicked: (ModelIndexProxy -> 'msg) option = None
    let mutable onDoubleClicked: (ModelIndexProxy -> 'msg) option = None
    let mutable onEntered: (ModelIndexProxy -> 'msg) option = None
    let mutable onIconSizeChanged: (Size -> 'msg) option = None
    let mutable onPressed: (ModelIndexProxy -> 'msg) option = None
    let mutable onViewportEntered: 'msg option = None

    member internal this.SignalMask = enum<AbstractItemView.SignalMask> (int this._signalMask)
    
    member this.OnActivated with set value =
        onActivated <- Some value
        this.AddSignal(int AbstractItemView.SignalMask.Activated)
        
    member this.OnClicked with set value =
        onClicked <- Some value
        this.AddSignal(int AbstractItemView.SignalMask.Clicked)
        
    member this.OnDoubleClicked with set value =
        onDoubleClicked <- Some value
        this.AddSignal(int AbstractItemView.SignalMask.DoubleClickedBit)
        
    member this.OnEntered with set value =
        onEntered <- Some value
        this.AddSignal(int AbstractItemView.SignalMask.Entered)
        
    member this.OnIconSizeChanged with set value =
        onIconSizeChanged <- Some value
        this.AddSignal(int AbstractItemView.SignalMask.IconSizeChanged)
        
    member this.OnPressed with set value =
        onPressed <- Some value
        this.AddSignal(int AbstractItemView.SignalMask.Pressed)
        
    member this.OnViewportEntered with set value =
        onViewportEntered <- Some value
        this.AddSignal(int AbstractItemView.SignalMask.ViewportEntered)
        
    member internal this.SignalMapList =
        let thisFunc = function
            | Activated index ->
                onActivated
                |> Option.map (fun f -> f index)
            | Clicked index ->
                onClicked
                |> Option.map (fun f -> f index)
            | Signal.DoubleClicked index ->
                onDoubleClicked
                |> Option.map (fun f -> f index)
            | Entered index ->
                onEntered
                |> Option.map (fun f -> f index)
            | IconSizeChanged size ->
                onIconSizeChanged
                |> Option.map (fun f -> f size)
            | Pressed index ->
                onPressed
                |> Option.map (fun f -> f index)
            | ViewportEntered ->
                onViewportEntered
        // prepend to parent list
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList

    member this.AlternatingRowColors with set value =
        this.PushAttr(AlternatingRowColors value)
        
    member this.AutoScroll with set value =
        this.PushAttr(AutoScroll value)
        
    member this.AutoScrollMargin with set value =
        this.PushAttr(AutoScrollMargin value)
        
    member this.DefaultDropAction with set value =
        this.PushAttr(DefaultDropAction value)
        
    member this.DragDropMode with set value =
        this.PushAttr(DragDropMode value)
        
    member this.DragDropOverwriteMode with set value =
        this.PushAttr(DragDropOverwriteMode value)
        
    member this.DragEnabled with set value =
        this.PushAttr(DragEnabled value)
        
    member this.EditTriggers with set value =
        this.PushAttr(value |> Set.ofSeq |> EditTriggers)
        
    member this.HorizontalScrollMode with set value =
        this.PushAttr(HorizontalScrollMode value)
        
    member this.IconSize with set value =
        this.PushAttr(IconSize value)
    
    member this.SelectionBehavior with set value =
        this.PushAttr(SelectionBehavior value)
    
    member this.SelectionMode with set value =
        this.PushAttr(SelectionMode value)
    
    member this.DropIndicatorShown with set value =
        this.PushAttr(DropIndicatorShown value)
    
    member this.TabKeyNavigation with set value =
        this.PushAttr(TabKeyNavigation value)
    
    member this.TextElideMode with set value =
        this.PushAttr(TextElideMode value)
    
    member this.VerticalScrollMode with set value =
        this.PushAttr(VerticalScrollMode value)

type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit AbstractScrollArea.ModelCore<'msg>(dispatch)
    let mutable abstractItemView: AbstractItemView.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    // abstract, no signal mask
    
    // binding guards:
    let mutable lastIconSize = Size.Invalid

    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.AbstractItemView
        with get() = abstractItemView
        and set value =
            this.AbstractScrollArea <- value
            abstractItemView <- value

    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "AbstractItemView.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "AbstractItemView.ModelCore: signal map assignment didn't have a head element"
            
    // abstract class, no signal mask setter
    
    interface AttrTarget with
        member this.ApplyAbstractItemViewAttr attr =
            match attr with
            | AlternatingRowColors state ->
                abstractItemView.SetAlternatingRowColors(state)
            | AutoScroll state ->
                abstractItemView.SetAutoScroll(state)
            | AutoScrollMargin margin ->
                abstractItemView.SetAutoScrollMargin(margin)
            | DefaultDropAction action ->
                abstractItemView.SetDefaultDropAction(action.QtValue)
            | DragDropMode mode ->
                abstractItemView.SetDragDropMode(mode.QtValue)
            | DragDropOverwriteMode mode ->
                abstractItemView.SetDragDropOverwriteMode(mode)
            | DragEnabled enabled ->
                abstractItemView.SetDragEnabled(enabled)
            | EditTriggers triggers ->
                abstractItemView.SetEditTriggers(EditTriggers.QtFlagsFrom triggers)
            | HorizontalScrollMode mode ->
                abstractItemView.SetHorizontalScrollMode(mode.QtValue)
            | IconSize size ->
                if size <> lastIconSize then
                    lastIconSize <- size
                    abstractItemView.SetIconSize(size.QtValue)
            | SelectionBehavior behavior ->
                abstractItemView.SetSelectionBehavior(behavior.QtValue)
            | SelectionMode mode ->
                abstractItemView.SetSelectionMode(mode.QtValue)
            | DropIndicatorShown state ->
                abstractItemView.SetDropIndicatorShown(state)
            | TabKeyNavigation state ->
                abstractItemView.SetTabKeyNavigation(state)
            | TextElideMode mode ->
                abstractItemView.SetTextElideMode(mode.QtValue)
            | VerticalScrollMode mode ->
                abstractItemView.SetVerticalScrollMode(mode.QtValue)
                
    interface AbstractItemView.SignalHandler with
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
        // Frame ==========================
        // (none)
        // AbstractScrollArea =============
        // (none)
        // AbstractItemView ===============
        member this.Activated index =
            signalDispatch (ModelIndexProxy(index) |> Activated)
        member this.Clicked index =
            signalDispatch (ModelIndexProxy(index) |> Clicked)
        member this.DoubleClicked index =
            signalDispatch (ModelIndexProxy(index) |> Signal.DoubleClicked)
        member this.Entered index =
            signalDispatch (ModelIndexProxy(index) |> Entered)
        member this.IconSizeChanged size =
            signalDispatch (Size.From size |> IconSizeChanged)
        member this.Pressed index =
            signalDispatch (ModelIndexProxy(index) |> Pressed)
        member this.ViewportEntered() =
            signalDispatch ViewportEntered

    interface IDisposable with
        member this.Dispose() =
            abstractItemView.Dispose()
