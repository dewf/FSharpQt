module FSharpQt.Widgets.ComboBox

open System
open FSharpQt.BuilderNode
open Org.Whatever.MinimalQtForFSharp

open FSharpQt.MiscTypes
open FSharpQt.Attrs

type private Signal =
    | Activated of index: int option
    | CurrentIndexChanged of index: int option
    | CurrentTextChanged of text: string option
    | EditTextChanged of text: string
    | Highlighted of index: int option
    | TextActivated of text: string
    | TextHighlighted of text: string
    
type InsertPolicy =
    | NoInsert
    | InsertAtTop
    | InsertAtCurrent
    | InsertAtBottom
    | InsertAfterCurrent
    | InsertBeforeCurrent
    | InsertAlphabetically
with
    member this.QtValue =
        match this with
        | NoInsert -> ComboBox.InsertPolicy.NoInsert
        | InsertAtTop -> ComboBox.InsertPolicy.InsertAtTop
        | InsertAtCurrent -> ComboBox.InsertPolicy.InsertAtCurrent
        | InsertAtBottom -> ComboBox.InsertPolicy.InsertAtBottom
        | InsertAfterCurrent -> ComboBox.InsertPolicy.InsertAfterCurrent
        | InsertBeforeCurrent -> ComboBox.InsertPolicy.InsertBeforeCurrent
        | InsertAlphabetically -> ComboBox.InsertPolicy.InsertAlphabetically
        
type SizeAdjustPolicy =
    | AdjustToContents
    | AdjustToContentsOnFirstShow
    | AdjustToMinimumContentsLengthWithIcon
with
    member this.QtValue =
        match this with
        | AdjustToContents -> ComboBox.SizeAdjustPolicy.AdjustToContents
        | AdjustToContentsOnFirstShow -> ComboBox.SizeAdjustPolicy.AdjustToContentsOnFirstShow
        | AdjustToMinimumContentsLengthWithIcon -> ComboBox.SizeAdjustPolicy.AdjustToMinimumContentsLengthWithIcon
    
type internal Attr =
    | CurrentIndex of maybeIndex: int option
    | CurrentText of maybeText: string option
    | DuplicatesEnabled of enabled: bool
    | Editable of editable: bool
    | Frame of hasFrame: bool
    | IconSize of size: Size
    | InsertPolicy of policy: InsertPolicy
    | MaxCount of count: int
    | MaxVisibleItems of count: int
    | MinimumContentsLength of length: int
    | ModelColumn of column: int
    | PlaceholderText of text: string
    | SizeAdjustPolicy of policy: SizeAdjustPolicy
    // ours:
    | StringItems of items: string list // C++ .addItems()
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
            | CurrentIndex _ -> "combobox:currentindex"
            | CurrentText _ -> "combobox:currenttext"
            | DuplicatesEnabled _ -> "combobox:duplicatesenabled"
            | Editable _ -> "combobox:editable"
            | Frame _ -> "combobox:hasframe"
            | IconSize _ -> "combobox:iconsize"
            | InsertPolicy _ -> "combobox:insertpolicy"
            | MaxCount _ -> "combobox:maxcount"
            | MaxVisibleItems _ -> "combobox:maxvisibleitems"
            | MinimumContentsLength _ -> "combobox:minimumcontentslength"
            | ModelColumn _ -> "combobox:modelcolumn"
            | PlaceholderText _ -> "combobox:placeholdertext"
            | SizeAdjustPolicy _ -> "combobox:sizeadjustpolicy"
            | StringItems _ -> "combobox:stringitems"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyComboBoxAttr(this)
            | _ ->
                printfn "warning: ComboBox.Attr couldn't ApplyTo() unknown target type [%A]" target

and internal AttrTarget =
    interface
        inherit Widget.AttrTarget
        abstract member ApplyComboBoxAttr: Attr -> unit
    end
    
type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)
                
type Props<'msg>() =
    inherit Widget.Props<'msg>()

    let mutable onActivated: (int option -> 'msg) option = None
    let mutable onCurrentIndexChanged: (int option -> 'msg) option = None
    let mutable onCurrentTextChanged: (string option -> 'msg) option = None
    let mutable onEditTextChanged: (string -> 'msg) option = None
    let mutable onHighlighted: (int option -> 'msg) option = None
    let mutable onTextActivated: (string -> 'msg) option = None
    let mutable onTextHighlighted: (string -> 'msg) option = None

    member internal this.SignalMask = enum<ComboBox.SignalMask> (int this._signalMask)

    member this.OnActivated with set value =
        onActivated <- Some value
        this.AddSignal(int ComboBox.SignalMask.Activated)
        
    member this.OnCurrentIndexChanged with set value =
        onCurrentIndexChanged <- Some value
        this.AddSignal(int ComboBox.SignalMask.CurrentIndexChanged)
        
    member this.OnCurrentTextChanged with set value =
        onCurrentTextChanged <- Some value
        this.AddSignal(int ComboBox.SignalMask.CurrentTextChanged)
        
    member this.OnEditTextChanged with set value =
        onEditTextChanged <- Some value
        this.AddSignal(int ComboBox.SignalMask.EditTextChanged)
        
    member this.OnHighlighted with set value =
        onHighlighted <- Some value
        this.AddSignal(int ComboBox.SignalMask.Highlighted)
        
    member this.OnTextActivated with set value =
        onTextActivated <- Some value
        this.AddSignal(int ComboBox.SignalMask.TextActivated)
        
    member this.OnTextHighlighted with set value =
        onTextHighlighted <- Some value
        this.AddSignal(int ComboBox.SignalMask.TextHighlighted)
        
    member internal this.SignalMapList =
        let thisFunc = function
            | Activated index ->
                onActivated
                |> Option.map (fun f -> f index)
            | CurrentIndexChanged index ->
                onCurrentIndexChanged
                |> Option.map (fun f -> f index)
            | CurrentTextChanged text ->
                onCurrentTextChanged
                |> Option.map (fun f -> f text)
            | EditTextChanged text ->
                onEditTextChanged
                |> Option.map (fun f -> f text)
            | Highlighted index ->
                onHighlighted
                |> Option.map (fun f -> f index)
            | TextActivated text ->
                onTextActivated
                |> Option.map (fun f -> f text)
            | TextHighlighted text ->
                onTextHighlighted
                |> Option.map (fun f -> f text)
        // prepend to parent's
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList
    
    member this.CurrentIndex with set value =
        this.PushAttr(CurrentIndex value)

    member this.CurrentText with set value =
        this.PushAttr(CurrentText value)

    member this.DuplicatesEnabled with set value =
        this.PushAttr(DuplicatesEnabled value)

    member this.Editable with set value =
        this.PushAttr(Editable value)

    member this.Frame with set value =
        this.PushAttr(Frame value)

    member this.IconSize with set value =
        this.PushAttr(IconSize value)

    member this.InsertPolicy with set value =
        this.PushAttr(InsertPolicy value)

    member this.MaxCount with set value =
        this.PushAttr(MaxCount value)

    member this.MaxVisibleItems with set value =
        this.PushAttr(MaxVisibleItems value)

    member this.MinimumContentsLength with set value =
        this.PushAttr(MinimumContentsLength value)

    member this.ModelColumn with set value =
        this.PushAttr(ModelColumn value)

    member this.PlaceholderText with set value =
        this.PushAttr(PlaceholderText value)

    member this.SizeAdjustPolicy with set value =
        this.PushAttr(SizeAdjustPolicy value)
        
    member this.StringItems with set value =
        this.PushAttr(StringItems value)
        
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit Widget.ModelCore<'msg>(dispatch)
    let mutable comboBox: ComboBox.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<ComboBox.SignalMask> 0
    
    // binding guards:
    let mutable lastCurrentIndex: int option = None
    let mutable lastCurrentText: string option = None
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    let someIfPositive (i: int) =
        if i >= 0 then Some i else None
        
    let someIfNotEmpty (str: string) =
        if str <> "" then Some str else None
        
    member this.ComboBox
        with get() = comboBox
        and set value =
            // assign to base
            this.Widget <- value
            comboBox <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "ComboBox.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "ComboBox.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            comboBox.SetSignalMask(value)
            currentMask <- value
            
    interface AttrTarget with
        member this.ApplyComboBoxAttr attr =
            match attr with
            | CurrentIndex maybeIndex ->
                if maybeIndex <> lastCurrentIndex then
                    lastCurrentIndex <- maybeIndex
                    comboBox.SetCurrentIndex(maybeIndex |> Option.defaultValue -1)
            | CurrentText maybeText ->
                if maybeText <> lastCurrentText then
                    lastCurrentText <- maybeText
                    comboBox.SetCurrentText(maybeText |> Option.defaultValue "")
            | DuplicatesEnabled enabled ->
                comboBox.SetDuplicatesEnabled(enabled)
            | Editable editable ->
                comboBox.SetEditable(editable)
            | Frame hasFrame ->
                comboBox.SetFrame(hasFrame)
            | IconSize size ->
                comboBox.SetIconSize(size.QtValue)
            | InsertPolicy policy ->
                comboBox.SetInsertPolicy(policy.QtValue)
            | MaxCount count ->
                comboBox.SetMaxCount(count)
            | MaxVisibleItems count ->
                comboBox.SetMaxVisibleItems(count)
            | MinimumContentsLength length ->
                comboBox.SetMinimumContentsLength(length)
            | ModelColumn column ->
                comboBox.SetModelColumn(column)
            | PlaceholderText text ->
                comboBox.SetPlaceholderText(text)
            | SizeAdjustPolicy policy ->
                comboBox.SetSizeAdjustPolicy(policy.QtValue)
            | StringItems items ->
                comboBox.Clear()
                comboBox.AddItems(items |> Array.ofList)
                // necessary/OK?
                lastCurrentIndex <- None
                lastCurrentText <- None
                
    interface ComboBox.SignalHandler with
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
        // ComboBox =======================
        override this.Activated index =
            signalDispatch (someIfPositive index |> Activated)
        override this.CurrentIndexChanged qIndex =
            let maybeIndex = someIfPositive qIndex
            lastCurrentIndex <- maybeIndex
            signalDispatch (maybeIndex |> CurrentIndexChanged)
        override this.CurrentTextChanged text =
            let maybeText = someIfNotEmpty text
            lastCurrentText <- maybeText
            signalDispatch (maybeText |> CurrentTextChanged)
        override this.EditTextChanged text =
            signalDispatch (EditTextChanged text)
        override this.Highlighted index =
            signalDispatch (someIfPositive index |> Highlighted)
        override this.TextActivated text =
            signalDispatch (TextActivated text)
        override this.TextHighlighted text =
            signalDispatch (TextHighlighted text)
            
    interface IDisposable with
        member this.Dispose() =
            comboBox.Dispose()

type private Model<'msg>(dispatch: 'msg -> unit) as this =
    inherit ModelCore<'msg>(dispatch)
    do
        this.ComboBox <- ComboBox.Create(this)

let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: ComboBox.SignalMask) =
    let model = new Model<'msg>(dispatch)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: ComboBox.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()

type ComboBox<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    interface IWidgetNode<'msg> with
        override this.Dependencies = []
        
        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask
            
        override this.AttachDeps () =
            ()
            
        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> ComboBox<'msg>)
            let nextAttrs =
                diffAttrs left'.Attrs this.Attrs
                |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask
                
        override this.Dispose() =
            (this.model :> IDisposable).Dispose()
            
        override this.Widget =
            this.model.ComboBox
            
        override this.ContentKey =
            this.model.ComboBox
            
        override this.Attachments =
            this.Attachments

        override this.Binding = None
