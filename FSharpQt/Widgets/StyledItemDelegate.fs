module FSharpQt.Widgets.StyledItemDelegate

open System
open FSharpQt.Attrs
open FSharpQt.BuilderNode

open Org.Whatever.MinimalQtForFSharp
open FSharpQt.MiscTypes

type private Signal = unit

type internal Attr =
    | NoneYet
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
            | NoneYet -> "styleditemdelegate:noneyet"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyStyledItemDelegateAttr(this)
            | _ ->
                printfn "warning: StyledItemDelegate.Attr couldn't ApplyTo() unknown target type [%A]" target
                
and internal AttrTarget =
    interface
        inherit AbstractItemDelegate.AttrTarget
        abstract member ApplyStyledItemDelegateAttr: Attr -> unit
    end
   
type Props<'msg>() =
    inherit AbstractItemDelegate.Props<'msg>()
    
    member internal this.SignalMask = enum<StyledItemDelegate.SignalMask> (int this._signalMask)
    
    member internal this.SignalMapList =
        NullSignalMapFunc() :> ISignalMapFunc :: base.SignalMapList
        
    // no attrs yet
  
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit AbstractItemDelegate.ModelCore<'msg>(dispatch)
    let mutable styledItemDelegate: StyledItemDelegate.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<StyledItemDelegate.SignalMask> 0
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.StyledItemDelegate
        with get() = styledItemDelegate
        and set value =
            this.AbstractItemDelegate <- value
            styledItemDelegate <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? NullSignalMapFunc ->
                ()
            | _ ->
                failwith "StyledItemDelegate.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "StyledItemDelegate.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            styledItemDelegate.SetSignalMask(value)
            currentMask <- value
            
    interface AttrTarget with
        member this.ApplyStyledItemDelegateAttr attr =
            match attr with
            | NoneYet -> failwith "no"

    interface StyledItemDelegate.SignalHandler with
        // object =========================
        member this.Destroyed(obj: Object.Handle) =
            (this :> Object.SignalHandler).Destroyed(obj)
        member this.ObjectNameChanged(name: string) =
            (this :> Object.SignalHandler).ObjectNameChanged(name)
        // abstractitemdelegate ===========
        member this.CloseEditor(editor: Widget.Handle, qtHint: AbstractItemDelegate.EndEditHint) =
            (this :> AbstractItemDelegate.SignalHandler).CloseEditor(editor, qtHint)
        member this.CommitData(editor: Widget.Handle) =
            (this :> AbstractItemDelegate.SignalHandler).CommitData(editor)
        member this.SizeHintChanged(index: Org.Whatever.MinimalQtForFSharp.ModelIndex.Handle) =
            (this :> AbstractItemDelegate.SignalHandler).SizeHintChanged(index)
        // styleditemdelegate =============
        // (none)
        
    interface IDisposable with
        member this.Dispose() =
            styledItemDelegate.Dispose()
            
type IEventDelegate<'msg> =
    interface
        abstract member CreateEditor: StyleOptionViewItemProxy -> Org.Whatever.MinimalQtForFSharp.ModelIndex.Handle -> IWidgetNode<'msg>
        abstract member SetEditorDataRaw: Widget.Handle -> Org.Whatever.MinimalQtForFSharp.ModelIndex.Handle -> unit
        abstract member SetModelDataRaw: Widget.Handle -> AbstractItemModel.Handle -> Org.Whatever.MinimalQtForFSharp.ModelIndex.Handle -> unit
    end
    
[<AbstractClass>]
type ComboBoxItemDelegateBase<'msg>() =
    abstract member CreateEditor: StyleOptionViewItemProxy -> ModelIndex -> FSharpQt.Widgets.ComboBox.ComboBox<'msg>
    abstract member SetEditorData: ComboBoxProxy -> ModelIndex -> unit
    abstract member SetModelData: ComboBoxProxy -> AbstractItemModelProxy -> ModelIndex -> unit
    interface IEventDelegate<'msg> with
        member this.CreateEditor option index =
            this.CreateEditor option (new ModelIndex(index))
        member this.SetEditorDataRaw editor index =
            // interesting, F# casting (:?>) can't cant work because (I think) the .NET side only knows this as a widget handle, that's how it came to us in this callback
            // (because it was built from an IWidgetNode), then passed to the other side
            // we don't have any inherent continuity of the raw opaque handles being passed from one side to another
            // so we need a way of "force-casting" an opaque handle, from the C++ side
            let combo = ComboBox.DowncastFrom(editor)
            this.SetEditorData (ComboBoxProxy(combo)) (new ModelIndex(index))
        member this.SetModelDataRaw editor model index =
            let combo = ComboBox.DowncastFrom(editor)
            this.SetModelData (ComboBoxProxy(combo)) (AbstractItemModelProxy(model)) (new ModelIndex(index))
    
// [<AbstractClass>]
// type AbstractEventDelegate<'msg,'widgetProxy>(proxyFunc: Widget.Handle -> 'widgetProxy) =
//     abstract member CreateEditor: WidgetProxy -> StyleOptionViewItemProxy -> ModelIndexProxy -> IWidgetNode<'msg>
//     abstract member SetEditorData: 'widgetProxy -> ModelIndexProxy -> unit
//     abstract member SetModelData: 'widgetProxy -> AbstractItemModelProxy -> ModelIndexProxy -> unit
//     interface IEventDelegate<'msg> with
//         member this.CreateEditor widget option index =
//             this.CreateEditor widget option index
//         member this.SetEditorDataRaw widget index =
//             this.SetEditorData (proxyFunc widget.Handle) index
//         member this.SetModelDataRaw widget model index =
//             this.SetModelData (proxyFunc widget.Handle) model index

type private Model<'msg>(dispatch: 'msg -> unit, eventDelegate: IEventDelegate<'msg>) as this =
    inherit ModelCore<'msg>(dispatch)
    let mutable EditorRoot: IBuilderNode<'msg> option = None
    let methodMask =
        StyledItemDelegate.MethodMask.CreateEditor |||
        StyledItemDelegate.MethodMask.SetEditorData |||
        StyledItemDelegate.MethodMask.SetModelData |||
        StyledItemDelegate.MethodMask.DestroyEditor
    let styledItemDelegate = StyledItemDelegate.CreatedSubclassed(this, methodMask, this)
    let mutable eventDelegate = eventDelegate
    do
        this.StyledItemDelegate <- styledItemDelegate
        
    member this.EventDelegate with set value =
        eventDelegate <- value
        
    interface StyledItemDelegate.MethodDelegate with
        member this.CreateEditor(parent, option, index) =
            let root = eventDelegate.CreateEditor (StyleOptionViewItemProxy(option)) index
            let events = DiffEventsList()
            build dispatch (root :> IBuilderNode<'msg>) { ContainingWindow = None } events
            root.Widget.SetParent(parent)
            EditorRoot <- Some root
            // process diff events (currently only window creation events, which aren't really relevant here ...)
            events.ProcessEvents()
            // return
            root.Widget
        member this.SetEditorData(editor, index) =
            eventDelegate.SetEditorDataRaw editor index
        member this.SetModelData(editor, model, index) =
            eventDelegate.SetModelDataRaw editor model index
        member this.DestroyEditor(editor, index) =
            // need to destroy the tree built in .CreateEditor()
            match EditorRoot with
            | Some root ->
                disposeTree root
                EditorRoot <- None
            | None ->
                ()

let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: StyledItemDelegate.SignalMask) (eventDelegate: IEventDelegate<'msg>) =
    let model = new Model<'msg>(dispatch, eventDelegate)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: StyledItemDelegate.SignalMask) (eventDelegate: IEventDelegate<'msg>) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model.EventDelegate <- eventDelegate
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()
    
type StyledItemDelegate<'msg>(eventDelegate: IEventDelegate<'msg>) =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    interface IAbstractItemDelegateNode<'msg> with
        override this.Dependencies = []
        
        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask eventDelegate
            
        override this.AttachDeps () =
            ()
            
        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> StyledItemDelegate<'msg>)
            let nextAttrs =
                diffAttrs left'.Attrs this.Attrs
                |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask eventDelegate
                
        override this.Dispose() =
            (this.model :> IDisposable).Dispose()
            
        override this.AbstractItemDelegate =
            this.model.AbstractItemDelegate
            
        override this.ContentKey =
            this.model.AbstractItemDelegate
            
        override this.Attachments =
            this.Attachments

        override this.Binding = None
   
