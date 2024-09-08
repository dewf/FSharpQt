module FSharpQt.Widgets.PlainTextEdit

open System
open FSharpQt.Attrs
open FSharpQt.BuilderNode
open Org.Whatever.MinimalQtForFSharp

open FSharpQt.MiscTypes

type private Signal =
    | BlockCountChanged of newCount: int
    | CopyAvailable of state: bool
    | CursorPositionChanged
    | ModificationChanged of changed: bool
    | RedoAvailable of state: bool
    | SelectionChanged
    | TextChanged
    | UndoAvailable of state: bool
    | UpdateRequest of rect: Rect * dy: int
    
type LineWrapMode =
    | NoWrap
    | WidgetWidth
with
    member internal this.QtValue =
        match this with
        | NoWrap -> PlainTextEdit.LineWrapMode.NoWrap
        | WidgetWidth -> PlainTextEdit.LineWrapMode.WidgetWidth
    
type internal Attr =
    | BackgroundVisible of visible: bool
    | CenterOnScroll of state: bool
    | CursorWidth of width: int
    | DocumentTitle of title: string
    | LineWrapMode of mode: LineWrapMode
    | MaximumBlockCount of count: int
    | OverwriteMode of overwrite: bool
    | PlaceholderText of text: string
    | PlainText of text: string
    | ReadOnly of state: bool
    | TabChangesFocus of state: bool
    | TabStopDistance of distance: double
    | TextInteractionFlags of flags: Set<TextInteractionFlag>
    | UndoRedoEnabled of enabled: bool
    | WordWrapMode of mode: FSharpQt.MiscTypes.TextOption.WrapMode
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
            | BackgroundVisible _ -> "plaintextedit:backgroundvisible"
            | CenterOnScroll _ -> "plaintextedit:centeronscroll"
            | CursorWidth _ -> "plaintextedit:cursorwidth"
            | DocumentTitle _ -> "plaintextedit:documentitle"
            | LineWrapMode _ -> "plaintextedit:linewrapmode"
            | MaximumBlockCount _ -> "plaintextedit:maximumblockcount"
            | OverwriteMode _ -> "plaintextedit:overwritemode"
            | PlaceholderText _ -> "plaintextedit:placeholdertext"
            | PlainText _ -> "plaintextedit:plaintext"
            | ReadOnly _ -> "plaintextedit:readonly"
            | TabChangesFocus _ -> "plaintextedit:tabchangesfocus"
            | TabStopDistance _ -> "plaintextedit:tabstopdistance"
            | TextInteractionFlags _ -> "plaintextedit:textinteractionflags"
            | UndoRedoEnabled _ -> "plaintextedit:undoredoenabled"
            | WordWrapMode _ -> "plaintextedit:wordwrapmode"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyPlainTextEditAttr(this)
            | _ ->
                printfn "warning: PlainTextEdit.Attr couldn't ApplyTo() unknown target type [%A]" target
                
and internal AttrTarget =
    interface
        inherit AbstractScrollArea.AttrTarget
        abstract member ApplyPlainTextEditAttr: Attr -> unit
    end
    
type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)
    
type Props<'msg>() =
    inherit AbstractScrollArea.Props<'msg>()
    
    let mutable onBlockCountChanged: (int -> 'msg) option = None
    let mutable onCopyAvailable: (bool -> 'msg) option = None
    let mutable onCursorPositionChanged: 'msg option = None
    let mutable onModificationChanged: (bool -> 'msg) option = None
    let mutable onRedoAvailable: (bool -> 'msg) option = None
    let mutable onSelectionChanged: 'msg option = None
    let mutable onTextChanged: 'msg option = None
    let mutable onUndoAvailable: (bool -> 'msg) option = None
    let mutable onUpdateRequest: (Rect * int -> 'msg) option = None
    
    member internal this.SignalMask = enum<PlainTextEdit.SignalMask> (int this._signalMask)
    
    member this.OnBlockCountChanged with set value =
        onBlockCountChanged <- Some value
        this.AddSignal(int PlainTextEdit.SignalMask.BlockCountChanged)
        
    member this.OnCopyAvailable with set value =
        onCopyAvailable <- Some value
        this.AddSignal(int PlainTextEdit.SignalMask.CopyAvailable)
        
    member this.OnCursorPositionChanged with set value =
        onCursorPositionChanged <- Some value
        this.AddSignal(int PlainTextEdit.SignalMask.CursorPositionChanged)
        
    member this.OnModificationChanged with set value =
        onModificationChanged <- Some value
        this.AddSignal(int PlainTextEdit.SignalMask.ModificationChanged)
        
    member this.OnRedoAvailable with set value =
        onRedoAvailable <- Some value
        this.AddSignal(int PlainTextEdit.SignalMask.RedoAvailable)
        
    member this.OnSelectionChanged with set value =
        onSelectionChanged <- Some value
        this.AddSignal(int PlainTextEdit.SignalMask.SelectionChanged)
        
    member this.OnTextChanged with set value =
        onTextChanged <- Some value
        this.AddSignal(int PlainTextEdit.SignalMask.TextChanged)
        
    member this.OnUndoAvailable with set value =
        onUndoAvailable <- Some value
        this.AddSignal(int PlainTextEdit.SignalMask.UndoAvailable)
        
    member this.OnUpdateRequest with set value =
        onUpdateRequest <- Some value
        this.AddSignal(int PlainTextEdit.SignalMask.UpdateRequest)
        
    member internal this.SignalMapList =
        let thisFunc = function
            | BlockCountChanged newCount ->
                onBlockCountChanged
                |> Option.map (fun f -> f newCount)
            | CopyAvailable state ->
                onCopyAvailable
                |> Option.map (fun f -> f state)
            | CursorPositionChanged ->
                onCursorPositionChanged
            | ModificationChanged changed ->
                onModificationChanged
                |> Option.map (fun f -> f changed)
            | RedoAvailable state ->
                onRedoAvailable
                |> Option.map (fun f -> f state)
            | SelectionChanged ->
                onSelectionChanged
            | TextChanged ->
                onTextChanged
            | UndoAvailable state ->
                onUndoAvailable
                |> Option.map (fun f -> f state)
            | UpdateRequest(rect, dy) ->
                onUpdateRequest
                |> Option.map (fun f -> f (rect, dy))
        // prepend to parent list
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList
        
    member this.BackgroundVisible with set value =
        this.PushAttr(BackgroundVisible value)

    member this.CenterOnScroll with set value =
        this.PushAttr(CenterOnScroll value)

    member this.CursorWidth with set value =
        this.PushAttr(CursorWidth value)

    member this.DocumentTitle with set value =
        this.PushAttr(DocumentTitle value)

    member this.LineWrapMode with set value =
        this.PushAttr(LineWrapMode value)

    member this.MaximumBlockCount with set value =
        this.PushAttr(MaximumBlockCount value)

    member this.OverwriteMode with set value =
        this.PushAttr(OverwriteMode value)

    member this.PlaceholderText with set value =
        this.PushAttr(PlaceholderText value)

    member this.PlainText with set value =
        this.PushAttr(PlainText value)

    member this.ReadOnly with set value =
        this.PushAttr(ReadOnly value)

    member this.TabChangesFocus with set value =
        this.PushAttr(TabChangesFocus value)

    member this.TabStopDistance with set value =
        this.PushAttr(TabStopDistance value)

    member this.TextInteractionFlags with set value =
        this.PushAttr(value |> Set.ofSeq |> TextInteractionFlags)

    member this.UndoRedoEnabled with set value =
        this.PushAttr(UndoRedoEnabled value)

    member this.WordWrapMode with set value =
        this.PushAttr(WordWrapMode value)
        
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit AbstractScrollArea.ModelCore<'msg>(dispatch)
    let mutable plainTextEdit: PlainTextEdit.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<PlainTextEdit.SignalMask> 0
    
    // no binding guards required (seemingly, just a cursory glance at attrs + signals)
    // pooooossibly the text value, but iffy since:
    // 1) it requires a method to access (not a signal),
    // 2) can be fetched in multiple formats
        
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.PlainTextEdit
        with get() = plainTextEdit
        and set value =
            this.AbstractScrollArea <- value
            plainTextEdit <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "PlainTextEdit.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "PlainTextEdit.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            plainTextEdit.SetSignalMask(value)
            currentMask <- value
            
    interface AttrTarget with
        member this.ApplyPlainTextEditAttr attr =
            match attr with
            | BackgroundVisible visible ->
                plainTextEdit.SetBackgroundVisible(visible)
            | CenterOnScroll state ->
                plainTextEdit.SetCenterOnScroll(state)
            | CursorWidth width ->
                plainTextEdit.SetCursorWidth(width)
            | DocumentTitle title ->
                plainTextEdit.SetDocumentTitle(title)
            | LineWrapMode mode ->
                plainTextEdit.SetLineWrapMode(mode.QtValue)
            | MaximumBlockCount count ->
                plainTextEdit.SetMaximumBlockCount(count)
            | OverwriteMode overwrite ->
                plainTextEdit.SetOverwriteMode(overwrite)
            | PlaceholderText text ->
                plainTextEdit.SetPlaceholderText(text)
            | PlainText text ->
                // not sure how or if to do a binding guard on this ... needs some thought
                // if text <> plainTextEdit.ToPlainText() then
                plainTextEdit.SetPlainText(text)
            | ReadOnly state ->
                plainTextEdit.SetReadOnly(state)
            | TabChangesFocus state ->
                plainTextEdit.SetTabChangesFocus(state)
            | TabStopDistance distance ->
                plainTextEdit.SetTabStopDistance(distance)
            | TextInteractionFlags flags ->
                plainTextEdit.SetTextInteractionFlags(TextInteractionFlag.QtSetFrom flags)
            | UndoRedoEnabled enabled ->
                plainTextEdit.SetUndoRedoEnabled(enabled)
            | WordWrapMode mode ->
                plainTextEdit.SetWordWrapMode(mode.QtValue)
                
    interface PlainTextEdit.SignalHandler with
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
        // PlainTextEdit ==================
        member this.BlockCountChanged newCount =
            signalDispatch (BlockCountChanged newCount)
        member this.CopyAvailable state =
            signalDispatch (CopyAvailable state)
        member this.CursorPositionChanged () =
            signalDispatch CursorPositionChanged
        member this.ModificationChanged changed =
            signalDispatch (ModificationChanged changed)
        member this.RedoAvailable state =
            signalDispatch (RedoAvailable state)
        member this.SelectionChanged () =
            signalDispatch SelectionChanged
        member this.TextChanged () =
            // see note in the AttrTarget - not sure whether to do a binding guard on this, since the signal carries no data
            // currentText <- plainTextEdit.ToPlainText()
            signalDispatch TextChanged
        member this.UndoAvailable state =
            signalDispatch (UndoAvailable state)
        member this.UpdateRequest (qRect, dy) =
            signalDispatch (UpdateRequest (Rect.From qRect, dy))
            
    interface IDisposable with
        member this.Dispose() =
            plainTextEdit.Dispose()
    
type private Model<'msg>(dispatch: 'msg -> unit) as this =
    inherit ModelCore<'msg>(dispatch)
    do
        this.PlainTextEdit <- PlainTextEdit.Create(this)

let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: PlainTextEdit.SignalMask) =
    let model = new Model<'msg>(dispatch)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: PlainTextEdit.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()
    
type PlainTextEditBinding internal(handle: PlainTextEdit.Handle) =
    interface IViewBinding
    member this.ToPlainText() =
        handle.ToPlainText()
    member this.BlockCount =
        handle.BlockCount()
    
let bindNode (name: string) (map: Map<string, IViewBinding>) =
    match map.TryFind name with
    | Some (:? PlainTextEditBinding as pte) ->
        pte
    | _ ->
        failwith "PlainTextEdit.bindNode fail"
            
type PlainTextEdit<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    interface IWidgetNode<'msg> with
        override this.Dependencies = []

        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask
            
        override this.AttachDeps () =
            ()

        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> PlainTextEdit<'msg>)
            let nextAttrs =
                diffAttrs left'.Attrs this.Attrs
                |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask

        override this.Dispose() =
            (this.model :> IDisposable).Dispose()

        override this.Widget =
            this.model.PlainTextEdit
            
        override this.ContentKey =
            this.model.PlainTextEdit
            
        override this.Attachments =
            this.Attachments

        override this.Binding =
            this.MaybeBoundName
            |> Option.map (fun name ->
                name, PlainTextEditBinding(this.model.PlainTextEdit))
             
