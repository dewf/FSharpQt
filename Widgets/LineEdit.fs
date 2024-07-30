module FSharpQt.Widgets.LineEdit

open System
open FSharpQt.BuilderNode
open FSharpQt.Reactor
open Org.Whatever.MinimalQtForFSharp

open FSharpQt.MiscTypes
open FSharpQt.Attrs

type private Signal =
    | CursorPositionChanged of oldPos: int * newPos: int
    | EditingFinished
    | InputRejected
    | ReturnPressed
    | SelectionChanged
    | TextChanged of text: string  // also emitted when the text is changed programmatically
    | TextEdited of text: string   // user input only
    
type EchoMode =
    | Normal
    | NoEcho
    | Password
    | PasswordEchoOnEdit
with
    member internal this.QtValue =
        match this with
        | Normal -> LineEdit.EchoMode.Normal
        | NoEcho -> LineEdit.EchoMode.NoEcho
        | Password -> LineEdit.EchoMode.Password
        | PasswordEchoOnEdit -> LineEdit.EchoMode.PasswordEchoOnEdit
        
type internal Attr =
    | Alignment of align: Alignment
    | ClearButtonEnabled of enabled: bool
    | CursorMoveStyle of style: CursorMoveStyle
    | CursorPosition of pos: int
    | DragEnabled of enabled: bool
    | EchoMode of mode: EchoMode
    | Frame of enabled: bool
    | InputMask of mask: string
    | MaxLength of length: int
    | Modified of state: bool
    | PlaceholderText of text: string
    | ReadOnly of value: bool
    | Text of text: string
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
            | Alignment _ -> "lineedit:alignment"
            | ClearButtonEnabled _ -> "lineedit:clearbuttonenabled"
            | CursorMoveStyle _ -> "lineedit:cursormovestyle"
            | CursorPosition _ -> "lineedit:cursorposition"
            | DragEnabled _ -> "lineedit:dragenabled"
            | EchoMode _ -> "lineedit:echomode"
            | Frame _ -> "lineedit:frame"
            | InputMask _ -> "lineedit:inputmask"
            | MaxLength _ -> "lineedit:maxlength"
            | Modified _ -> "lineedit:modified"
            | PlaceholderText _ -> "lineedit:placeholdertext"
            | ReadOnly _ -> "lineedit:readonly"
            | Text _ -> "lineedit:text"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyLineEditAttr(this)
            | _ ->
                printfn "warning: LineEdit.Attr couldn't ApplyTo() unknown target type [%A]" target
                
and internal AttrTarget =
    interface
        inherit Widget.AttrTarget
        abstract member ApplyLineEditAttr: Attr -> unit
    end

type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)

type Props<'msg>() =
    inherit Widget.Props<'msg>()
    
    let mutable onCursorPositionChanged: (int * int -> 'msg) option = None
    let mutable onEditingFinished: 'msg option = None
    let mutable onInputRejected: 'msg option = None
    let mutable onReturnPressed: 'msg option = None
    let mutable onSelectionChanged: 'msg option = None
    let mutable onTextChanged: (string -> 'msg) option = None
    let mutable onTextEdited: (string -> 'msg) option = None

    member internal this.SignalMask = enum<LineEdit.SignalMask> (int this._signalMask)
    
    member this.OnCursorPositionChanged with set value =
        onCursorPositionChanged <- Some value
        this.AddSignal(int LineEdit.SignalMask.CursorPositionChanged)
        
    member this.OnEditingFinished with set value =
        onEditingFinished <- Some value
        this.AddSignal(int LineEdit.SignalMask.EditingFinished)
        
    member this.OnInputRejected with set value =
        onInputRejected <- Some value
        this.AddSignal(int LineEdit.SignalMask.InputRejected)
        
    member this.OnReturnPressed with set value =
        onReturnPressed <- Some value
        this.AddSignal(int LineEdit.SignalMask.ReturnPressed)
        
    member this.OnSelectionChanged with set value =
        onSelectionChanged <- Some value
        this.AddSignal(int LineEdit.SignalMask.SelectionChanged)
        
    member this.OnTextChanged with set value =
        onTextChanged <- Some value
        this.AddSignal(int LineEdit.SignalMask.TextChanged)
        
    member this.OnTextEdited with set value =
        onTextEdited <- Some value
        this.AddSignal(int LineEdit.SignalMask.TextEdited)

    member internal this.SignalMapList =
        let thisFunc = function
            | CursorPositionChanged(oldPos, newPos) ->
                onCursorPositionChanged
                |> Option.map (fun f -> f (oldPos, newPos))
            | EditingFinished ->
                onEditingFinished
            | InputRejected ->
                onInputRejected
            | ReturnPressed ->
                onReturnPressed
            | SelectionChanged ->
                onSelectionChanged
            | TextChanged text ->
                onTextChanged
                |> Option.map (fun f -> f text)
            | TextEdited text ->
                onTextEdited
                |> Option.map (fun f -> f text)
        // prepend to parent signal map funcs
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList
    
    member this.Alignment with set value =
        this.PushAttr(Alignment value)
        
    member this.ClearButtonEnabled with set value =
        this.PushAttr(ClearButtonEnabled value)
        
    member this.CursorMoveStyle with set value =
        this.PushAttr(CursorMoveStyle value)
        
    member this.CursorPosition with set value =
        this.PushAttr(CursorPosition value)
        
    member this.DragEnabled with set value =
        this.PushAttr(DragEnabled value)
        
    member this.EchoMode with set value =
        this.PushAttr(EchoMode value)
        
    member this.Frame with set value =
        this.PushAttr(Frame value)
        
    member this.InputMask with set value =
        this.PushAttr(InputMask value)
        
    member this.MaxLength with set value =
        this.PushAttr(MaxLength value)
        
    member this.Modified with set value =
        this.PushAttr(Modified value)
        
    member this.PlaceholderText with set value =
        this.PushAttr(PlaceholderText value)
        
    member this.Readonly with set value =
        this.PushAttr(ReadOnly value)
        
    member this.Text with set value =
        this.PushAttr(Text value)
        
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit Widget.ModelCore<'msg>(dispatch)
    let mutable lineEdit: LineEdit.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<LineEdit.SignalMask> 0
    
    // binding guards
    let mutable lastCursorPos = -1
    let mutable lastText = ""
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.LineEdit
        with get() = lineEdit
        and set value =
            // assign to base
            this.Widget <- value
            lineEdit <- value
    
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "LineEdit.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "LineEdit.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            lineEdit.SetSignalMask(value)
            currentMask <- value
            
    interface AttrTarget with
        member this.ApplyLineEditAttr attr =
            match attr with
            | Alignment align ->
                lineEdit.SetAlignment(align.QtValue)
            | ClearButtonEnabled enabled ->
                lineEdit.SetClearButtonEnabled(enabled)
            | CursorMoveStyle style ->
                lineEdit.SetCursorMoveStyle(style.QtValue)
            | CursorPosition pos ->
                if pos <> lastCursorPos then
                    lastCursorPos <- pos
                    lineEdit.SetCursorPosition(pos)
            | DragEnabled enabled ->
                lineEdit.SetDragEnabled(enabled)
            | EchoMode mode ->
                lineEdit.SetEchoMode(mode.QtValue)
            | Frame enabled ->
                lineEdit.SetFrame(enabled)
            | InputMask mask ->
                lineEdit.SetInputMask(mask)
            | MaxLength length ->
                lineEdit.SetMaxLength(length)
            | Modified state ->
                lineEdit.SetModified(state)
            | PlaceholderText text ->
                lineEdit.SetPlaceholderText(text)
            | ReadOnly value ->
                lineEdit.SetReadOnly(value)
            | Text text ->
                if text <> lastText then
                    lastText <- text
                    lineEdit.SetText(text)
                
    interface LineEdit.SignalHandler with
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
        // LineEdit =======================
        member this.CursorPositionChanged (oldPos, newPos) =
            lastCursorPos <- newPos
            signalDispatch (CursorPositionChanged (oldPos, newPos))
        member this.EditingFinished () =
            signalDispatch EditingFinished
        member this.InputRejected () =
            signalDispatch InputRejected
        member this.ReturnPressed () =
            signalDispatch ReturnPressed
        member this.SelectionChanged () =
            signalDispatch SelectionChanged
        member this.TextChanged text =
            lastText <- text
            signalDispatch (TextChanged text)
        member this.TextEdited text =
            lastText <- text
            signalDispatch (TextEdited text)
                
    interface IDisposable with
        member this.Dispose() =
            lineEdit.Dispose()

type private Model<'msg>(dispatch: 'msg -> unit) as this =
    inherit ModelCore<'msg>(dispatch)
    do
        this.LineEdit <- LineEdit.Create(this)

let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: LineEdit.SignalMask) =
    let model = new Model<'msg>(dispatch)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: LineEdit.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()
    
type LineEditBinding internal(handle: LineEdit.Handle) =
    inherit Widget.WidgetBinding(handle)
    member this.Text =
        handle.Text()
    member this.Clear() =
        handle.Clear()
    member this.Copy() =
        handle.Copy()
    member this.Cut() =
        handle.Cut()
    member this.Paste() =
        handle.Paste()
    member this.Redo() =
        handle.Redo()
    member this.SelectAll() =
        handle.SelectAll()
    member this.Undo() =
        handle.Undo()
        
let bindNode (name: string) (map: Map<string, IViewBinding>) =
    match map.TryFind name with
    | Some (:? LineEditBinding as lineEdit) ->
        lineEdit
    | _ ->
        failwith "LineEdit.bindNode fail"

type LineEdit<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    interface IWidgetNode<'msg> with
        override this.Dependencies = []
        
        override this.Create dispatch buildContextr =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask
            
        override this.AttachDeps () =
            ()
            
        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> LineEdit<'msg>)
            let nextAttrs =
                diffAttrs left'.Attrs this.Attrs
                |> createdOrChanged
            this.model <-
                migrate left'.model nextAttrs this.SignalMapList this.SignalMask
                
        override this.Dispose() =
            (this.model :> IDisposable).Dispose()
            
        override this.Widget =
            this.model.LineEdit
            
        override this.ContentKey =
            this.model.LineEdit
            
        override this.Attachments =
            this.Attachments

        override this.Binding =
            this.MaybeBoundName
            |> Option.map (fun name ->
                name, LineEditBinding(this.model.LineEdit))

let lineEditGetAndClear name msgFunc =
    Cmd.ViewExec (fun bindings ->
        viewexec bindings {
            let! lineEdit = bindNode name
            let text = lineEdit.Text
            lineEdit.Clear()
            return msgFunc text
        })
