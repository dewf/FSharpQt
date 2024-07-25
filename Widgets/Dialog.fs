module FSharpQt.Widgets.Dialog

open System
open FSharpQt.BuilderNode
open FSharpQt.Reactor
open Org.Whatever.MinimalQtForFSharp

open FSharpQt.Attrs
open FSharpQt.MiscTypes

type private Signal =
    | Accepted
    | Finished of result: int
    | Rejected

type internal Attr =
    | Modal of state: bool
    | SizeGripEnabled of enabled: bool
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
            | Modal state -> "dialog:modal"
            | SizeGripEnabled enabled -> "dialog:sizegripenabled"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyDialogAttr(this)
            | _ ->
                printfn "warning: LineEdit.Attr couldn't ApplyTo() unknown target type [%A]" target
                
and internal AttrTarget =
    interface
        inherit Widget.AttrTarget
        abstract member ApplyDialogAttr: Attr -> unit
    end
                
type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)
                
type Props<'msg>() =
    inherit Widget.Props<'msg>()
    
    let mutable onAccepted: 'msg option = None
    let mutable onFinished: (int -> 'msg) option = None
    let mutable onRejected: 'msg option = None
    
    member this.SignalMask = enum<Dialog.SignalMask> (int this._signalMask)
        
    member this.OnAccepted with set value =
        onAccepted <- Some value
        this.AddSignal(int Dialog.SignalMask.Accepted)
    
    member this.OnFinished with set value =
        onFinished <- Some value
        this.AddSignal(int Dialog.SignalMask.Finished)
    
    member this.OnRejected with set value =
        onRejected <- Some value
        this.AddSignal(int Dialog.SignalMask.Rejected)
        
    member internal this.SignalMapList =
        let thisFunc = function
            | Accepted ->
                onAccepted
            | Finished result ->
                onFinished
                |> Option.map (fun f -> f result)
            | Rejected ->
                onRejected
        // prepend to parent signal map funcs
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList
    
    member this.Modal with set value =
        this.PushAttr(Modal value)
        
    member this.SizeGripEnabled with set value =
        this.PushAttr(SizeGripEnabled value)
        
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit Widget.ModelCore<'msg>(dispatch)
    let mutable dialog: Dialog.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<Dialog.SignalMask> 0
    
    // no binding guards
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.Dialog
        with get() = dialog
        and set value =
            // assign to base
            this.Widget <- value
            dialog <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "Dialog.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "Dialog.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            dialog.SetSignalMask(value)
            currentMask <- value
            
    interface AttrTarget with
        member this.ApplyDialogAttr attr =
            match attr with
            | Modal state ->
                dialog.SetModal(state)
            | SizeGripEnabled enabled ->
                dialog.SetSizeGripEnabled(enabled)
                
    interface Dialog.SignalHandler with
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
        // Dialog =========================
        member this.Accepted() =
            signalDispatch Accepted
        member this.Finished result =
            // I think this is from .done(result) method which we don't currently use
            signalDispatch (Finished result)
        member this.Rejected() =
            signalDispatch Rejected
            
    interface IDisposable with
        member this.Dispose() =
            dialog.Dispose()
    
type private Model<'msg>(dispatch: 'msg -> unit, maybeParent: Widget.Handle option) as this =
    inherit ModelCore<'msg>(dispatch)
    let dialog =
        let parentHandle =
            maybeParent
            |> Option.defaultValue null
        Dialog.Create(parentHandle, this)
    do
        this.Dialog <- dialog

    member this.RemoveLayout() =
        let existing =
            dialog.GetLayout()
        existing.RemoveAll()
        dialog.SetLayout(null)
        
    member this.AddLayout (layout: Layout.Handle) =
        dialog.SetLayout(layout)

let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (initialMask: Dialog.SignalMask) (maybeParent: Widget.Handle option) =
    let model = new Model<'msg>(dispatch, maybeParent)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- initialMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: Dialog.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()
    
type DialogBinding internal(handle: Dialog.Handle) =
    inherit Widget.WidgetBinding(handle)
    member this.Exec() =
        handle.Exec()
    member this.Show() =
        handle.Show()
    member this.Move(p: Point) =
        handle.Move(p.QtValue)
    member this.Accept() =
        handle.Accept()
    member this.Reject() =
        handle.Reject()
    
let bindNode (name: string) (map: Map<string, IViewBinding>) =
    match map.TryFind name with
    | Some (:? DialogBinding as dialog) ->
        dialog
    | _ ->
        failwith "Dialog.bindNode fail"

type Dialog<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>

    let mutable maybeLayout: ILayoutNode<'msg> option = None
    member private this.MaybeLayout = maybeLayout
    member this.Layout with set value = maybeLayout <- Some value

    member private this.MigrateContent (chamgeMap: Map<DepsKey, DepsChange>) =
        match chamgeMap.TryFind (StrKey "layout") with
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
            // neither side had layout
            ()
        // layout only - parent can't be changed after creation
    
    interface IDialogNode<'msg> with
        override this.Dependencies =
            maybeLayout
            |> Option.map (fun content -> (StrKey "layout", content :> IBuilderNode<'msg>))
            |> Option.toList
            
        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask buildContext.ContainingWindow
            
        override this.AttachDeps () =
            maybeLayout
            |> Option.iter (fun layout -> this.model.AddLayout layout.Layout)
            
        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> Dialog<'msg>)
            let nextAttrs =
                diffAttrs left'.Attrs this.Attrs
                |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask
            this.MigrateContent (depsChanges |> Map.ofList)
            
        override this.Dispose() =
            (this.model :> IDisposable).Dispose()
            
        override this.Dialog =
            this.model.Dialog
            
        override this.ContentKey =
            this.model.Dialog
            
        override this.Attachments =
            this.Attachments
            
        override this.Binding =
            this.MaybeBoundName
            |> Option.map (fun name ->
                name, DialogBinding(this.model.Dialog))

let acceptDialog name =
    Cmd.ViewExec (fun bindings ->
        viewexec bindings {
            let! dialog = bindNode name
            dialog.Accept()
        })
    
let rejectDialog name =
    Cmd.ViewExec (fun bindings ->
        viewexec bindings {
            let! dialog = bindNode name
            dialog.Reject()
        })
    
let execDialog name =
    Cmd.ViewExec (fun bindings ->
        viewexec bindings {
            let! dialog = bindNode name
            dialog.Exec() |> ignore
        })
    
let execDialogAtPoint name point relativeTo =
    Cmd.ViewExec (fun bindings ->
        viewexec bindings {
            let! dialog = bindNode name
            let! relativeToWidget = Widget.bindNode relativeTo
            let p2 = relativeToWidget.MapToGlobal(point)
            dialog.Move p2
            dialog.Exec() |> ignore
        })
    
let private resultCodeToBool (code: int) =
    match code with
    | 1 -> true
    | 0 -> false
    | _ -> failwithf "Dialog: unknown result code %d" code
    
let execDialogWithResult name msg =
    Cmd.ViewExec (fun bindings ->
        viewexec bindings {
            let! dialog = bindNode name
            return dialog.Exec() |> resultCodeToBool |> msg
        })
    
let execDialogWithResultAtPoint name msg point relativeTo =
    Cmd.ViewExec (fun bindings ->
        viewexec bindings {
            let! dialog = bindNode name
            let! relativeToWidget = Widget.bindNode relativeTo
            let p2 = relativeToWidget.MapToGlobal(point)
            dialog.Move p2
            return dialog.Exec() |> resultCodeToBool |> msg
        })

// // some utility stuff for Cmd.Dialog
//
// let execDialog (id: string) (msgFunc: bool -> 'msg) =
//     let msgFunc2 intValue =
//         match intValue with
//         | 1 -> true
//         | _ -> false
//         |> msgFunc
//     id, DialogOp.ExecWithResult msgFunc2
//     
// let execDialogAtPoint (id: string) (p: Point) (msgFunc: bool -> 'msg) =
//     let msgFunc2 intValue =
//         match intValue with
//         | 1 -> true
//         | _ -> false
//         |> msgFunc
//     id, DialogOp.ExecAtPointWithResult (p, msgFunc2)
