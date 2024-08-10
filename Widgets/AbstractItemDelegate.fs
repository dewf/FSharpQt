module FSharpQt.Widgets.AbstractItemDelegate

open System
open FSharpQt.Attrs

open Org.Whatever.MinimalQtForFSharp
open FSharpQt.MiscTypes

type EndEditHint =
    | NoHint
    | EditNextItem
    | EditPreviousItem
    | SubmitModelCache
    | RevertModelCache
with
    static member internal From (qtHint: AbstractItemDelegate.EndEditHint) =
        match qtHint with
        | AbstractItemDelegate.EndEditHint.NoHint -> NoHint
        | AbstractItemDelegate.EndEditHint.EditNextItem -> EditNextItem
        | AbstractItemDelegate.EndEditHint.EditPreviousItem -> EditPreviousItem
        | AbstractItemDelegate.EndEditHint.SubmitModelCache -> SubmitModelCache
        | AbstractItemDelegate.EndEditHint.RevertModelCache -> RevertModelCache
        | _ -> failwith "AbstractItemDelegate.EndEditHint.From - bad incoming value"

type private Signal =
    | CloseEditor of editor: WidgetProxy * hint: EndEditHint
    | CommitData of editor: WidgetProxy
    | SizeHintChanged of index: ModelIndexProxy
    
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
            | NoneYet -> "abstractitemdelegate:noneyet!!!"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyAbstractItemDelegateAttr(this)
            | _ ->
                printfn "warning: AbstractItemDelegate.Attr couldn't ApplyTo() unknown object type [%A]" target
    
and internal AttrTarget =
    interface
        inherit QObject.AttrTarget
        abstract member ApplyAbstractItemDelegateAttr: Attr -> unit
    end
    
type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)

type Props<'msg>() =
    inherit QObject.Props<'msg>()
    
    let mutable onCloseEditor: (WidgetProxy * EndEditHint -> 'msg) option = None
    let mutable onCommitData: (WidgetProxy -> 'msg) option = None
    let mutable onSizeHintChanged: (ModelIndexProxy -> 'msg) option = None
    
    member internal this.SignalMask = enum<AbstractItemDelegate.SignalMask> (int this._signalMask)
    
    member this.OnCloseEditor with set value =
        onCloseEditor <- Some value
        this.AddSignal(int AbstractItemDelegate.SignalMask.CloseEditor)
        
    member this.OnCommitData with set value =
        onCommitData <- Some value
        this.AddSignal(int AbstractItemDelegate.SignalMask.CommitData)
        
    member this.OnSizeHintChanged with set value =
        onSizeHintChanged <- Some value
        this.AddSignal(int AbstractItemDelegate.SignalMask.SizeHintChanged)
     
    member internal this.SignalMapList =
        let thisFunc = function
            | CloseEditor (editor, hint) ->
                onCloseEditor
                |> Option.map (fun f -> f (editor, hint))
            | CommitData editor ->
                onCommitData
                |> Option.map (fun f -> f editor)
            | SizeHintChanged index ->
                onSizeHintChanged
                |> Option.map (fun f -> f index)
        // we inherit from Object, so prepend to its signals
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList
        
    // no attrs yet

type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit QObject.ModelCore<'msg>(dispatch)
    let mutable abstractItemDelegate: AbstractItemDelegate.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    // not instantiable, so no signal mask
    // no bindings guards (yet?)
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
    
    member this.AbstractItemDelegate
        with get() = abstractItemDelegate
        and set value =
            this.Object <- value
            abstractItemDelegate <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "AbstractItemDelegate.ModelCore.SignalMaps: wrong func type"
            // assign the remainder up the hierarchy
            base.SignalMaps <- etc
        | _ ->
            failwith "AbstractItemDelegate.ModelCore: signal map assignment didn't have a head element"
            
    // no signalmask setter (not directly instantiable)
            
    interface AttrTarget with
        member this.ApplyAbstractItemDelegateAttr attr =
            match attr with
            | NoneYet -> failwith "todo"
            
    interface AbstractItemDelegate.SignalHandler with
        // object =========================
        member this.Destroyed(obj: Object.Handle) =
            (this :> Object.SignalHandler).Destroyed(obj)
        member this.ObjectNameChanged(name: string) =
            (this :> Object.SignalHandler).ObjectNameChanged(name)
        // abstractitemdelegate ===========
        member this.CloseEditor(editor: Widget.Handle, qtHint: AbstractItemDelegate.EndEditHint) =
            signalDispatch (CloseEditor(WidgetProxy(editor), EndEditHint.From qtHint))
        member this.CommitData(editor: Widget.Handle) =
            signalDispatch (CommitData(WidgetProxy(editor)))
        member this.SizeHintChanged(index: ModelIndex.Handle) =
            signalDispatch(SizeHintChanged(new ModelIndexProxy(index)))
            
    interface IDisposable with
        member this.Dispose() =
            abstractItemDelegate.Dispose()
          
            
            
