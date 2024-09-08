module FSharpQt.Widgets.MessageBox

open System
open FSharpQt
open BuilderNode
open FSharpQt.Attrs
open FSharpQt.MiscTypes
open Reactor
open Org.Whatever.MinimalQtForFSharp

type private Signal =
    | ButtonClicked of button: AbstractButtonProxy

type StandardButton =
    | Ok
    | Save
    | SaveAll
    | Open
    | Yes
    | YesToAll
    | No
    | NoToAll
    | Abort
    | Retry
    | Ignore
    | Close
    | Cancel
    | Discard
    | Help
    | Apply
    | Reset
    | RestoreDefaults
with
    member internal this.QtValue =
        match this with
        | Ok -> MessageBox.StandardButton.Ok
        | Save -> MessageBox.StandardButton.Save
        | SaveAll -> MessageBox.StandardButton.SaveAll
        | Open -> MessageBox.StandardButton.Open
        | Yes -> MessageBox.StandardButton.Yes
        | YesToAll -> MessageBox.StandardButton.YesToAll
        | No -> MessageBox.StandardButton.No
        | NoToAll -> MessageBox.StandardButton.NoToAll
        | Abort -> MessageBox.StandardButton.Abort
        | Retry -> MessageBox.StandardButton.Retry
        | Ignore -> MessageBox.StandardButton.Ignore
        | Close -> MessageBox.StandardButton.Close
        | Cancel -> MessageBox.StandardButton.Cancel
        | Discard -> MessageBox.StandardButton.Discard
        | Help -> MessageBox.StandardButton.Help
        | Apply -> MessageBox.StandardButton.Apply
        | Reset -> MessageBox.StandardButton.Reset
        | RestoreDefaults -> MessageBox.StandardButton.RestoreDefaults

    member internal this.QtFlag =
        int this.QtValue
        |> enum<MessageBox.StandardButtonSet> // notice it's the '...Set' enum

    static member internal FromQtValue(raw: MessageBox.StandardButton) =
        match raw with
        // NoButton is currently not supported, it appears to be OK
        | MessageBox.StandardButton.Ok -> Ok
        | MessageBox.StandardButton.Save -> Save
        | MessageBox.StandardButton.SaveAll -> SaveAll
        | MessageBox.StandardButton.Open -> Open
        | MessageBox.StandardButton.Yes -> Yes
        | MessageBox.StandardButton.YesToAll -> YesToAll
        | MessageBox.StandardButton.No -> No
        | MessageBox.StandardButton.NoToAll -> NoToAll
        | MessageBox.StandardButton.Abort -> Abort
        | MessageBox.StandardButton.Retry -> Retry
        | MessageBox.StandardButton.Ignore -> Ignore
        | MessageBox.StandardButton.Close -> Close
        | MessageBox.StandardButton.Cancel -> Cancel
        | MessageBox.StandardButton.Discard -> Discard
        | MessageBox.StandardButton.Help -> Help
        | MessageBox.StandardButton.Apply -> Apply
        | MessageBox.StandardButton.Reset -> Reset
        | MessageBox.StandardButton.RestoreDefaults -> RestoreDefaults
        | _ -> failwithf "MessageBox.StandardButton.FromQtValue - unknown input %A" raw

    static member internal QtSetFrom (buttons: StandardButton seq) =
        (enum<MessageBox.StandardButtonSet> 0, buttons)
        ||> Seq.fold (fun acc button -> acc ||| button.QtFlag)
        
type Icon =
    | Information
    | Warning
    | Critical
    | Question
with
    member internal this.QtValue =
        match this with
        | Information -> MessageBox.MessageBoxIcon.Information
        | Warning -> MessageBox.MessageBoxIcon.Warning
        | Critical -> MessageBox.MessageBoxIcon.Critical
        | Question -> MessageBox.MessageBoxIcon.Question
        
type Option =
    | DontUseNativeDialog
with
    static member internal QtSetFrom (values: Option seq) =
        (enum<MessageBox.Options> 0, values)
        ||> Seq.fold (fun acc value ->
            let flag =
                match value with
                | DontUseNativeDialog -> MessageBox.Options.DontUseNativeDialog
            acc ||| flag)
        
type internal Attr =
    | DetailedText of text: string
    | IconAttr of icon: Icon
    // | IconPixmap of pixmap: xxxx
    | InformativeText of text: string
    | Options of opts: Set<Option>
    | StandardButtons of buttons: Set<StandardButton>
    | Text of text: string
    | TextFormat of format: TextFormat
    | TextInteractionFlags of flags: Set<TextInteractionFlag>
    | DefaultButton of button: StandardButton
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
            | DetailedText _ -> "messagebox:detailedtext"
            | IconAttr _ -> "messagebox:iconattr"
            | InformativeText _ -> "messagebox:informativetext"
            | Options _ -> "messagebox:options"
            | StandardButtons _ -> "messagebox:standardbuttons"
            | Text _ -> "messagebox:text"
            | TextFormat _ -> "messagebox:textformat"
            | TextInteractionFlags _ -> "messagebox:textinteractionflags"
            | DefaultButton _ -> "messagebox:defaultbutton"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyMessageBoxAttr(this)
            | _ ->
                printfn "warning: MessageBox.Attr couldn't ApplyTo() unknown target type [%A]" target
                
and internal AttrTarget =
    interface
        inherit Dialog.AttrTarget
        abstract member ApplyMessageBoxAttr: Attr -> unit
    end
        
type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)
    
type Props<'msg>() =
    inherit Dialog.Props<'msg>()
    
    let mutable onButtonClicked: (AbstractButtonProxy -> 'msg) option = None
    
    member this.SignalMask = enum<MessageBox.SignalMask> (int this._signalMask)

    member this.OnButtonClicked with set value =
        onButtonClicked <- Some value
        this.AddSignal(int MessageBox.SignalMask.ButtonClicked)
        
    member internal this.SignalMapList =
        let thisFunc = function
            | ButtonClicked button ->
                onButtonClicked
                |> Option.map (fun f -> f button)
        // prepend to parent signal map funcs
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList
        
    member this.DetailedText with set value =
        this.PushAttr(DetailedText value)

    member this.Icon with set value =
        this.PushAttr(IconAttr value)

    // // | IconPixmap of pixmap: xxxx
    // member this.IconPixmap with set value =
    //     this.PushAttr(IconPixmap value)

    member this.InformativeText with set value =
        this.PushAttr(InformativeText value)

    member this.Options with set value =
        this.PushAttr(value |> Set.ofSeq |> Options)

    member this.StandardButtons with set value =
        this.PushAttr(value |> Set.ofSeq |> StandardButtons)

    member this.Text with set value =
        this.PushAttr(Text value)

    member this.TextFormat with set value =
        this.PushAttr(TextFormat value)

    member this.TextInteractionFlags with set value =
        this.PushAttr(value |> Set.ofSeq |> TextInteractionFlags)

    member this.DefaultButton with set value =
        this.PushAttr(DefaultButton value)
        
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit Dialog.ModelCore<'msg>(dispatch)
    let mutable messageBox: MessageBox.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<MessageBox.SignalMask> 0
    
    // no binding guards
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.MessageBox
        with get() = messageBox
        and set value =
            // assign to base
            this.Dialog <- value
            messageBox <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "MessageBox.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "MessageBox.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            messageBox.SetSignalMask(value)
            currentMask <- value
            
    interface AttrTarget with
        member this.ApplyMessageBoxAttr attr =
            match attr with
            | DetailedText text ->
                messageBox.SetDetailedText(text)
            | IconAttr icon ->
                messageBox.SetIcon(icon.QtValue)
            | InformativeText text ->
                messageBox.SetInformativeText(text)
            | Options opts ->
                messageBox.SetOptions(Option.QtSetFrom opts)
            | StandardButtons buttons ->
                messageBox.SetStandardButtons(StandardButton.QtSetFrom buttons)
            | Text text ->
                messageBox.SetText(text)
            | TextFormat format ->
                messageBox.SetTextFormat(format.QtValue)
            | TextInteractionFlags flags ->
                messageBox.SetTextInteractionFlags(TextInteractionFlag.QtSetFrom flags)
            | DefaultButton button ->
                messageBox.SetDefaultButton(button.QtValue)
                
    interface MessageBox.SignalHandler with
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
            (this :> Dialog.SignalHandler).Accepted()
        member this.Finished result =
            (this :> Dialog.SignalHandler).Finished(result)
        member this.Rejected() =
            (this :> Dialog.SignalHandler).Rejected()
        // FileDialog =====================
        member this.ButtonClicked button =
            signalDispatch (AbstractButtonProxy(button) |> ButtonClicked)
            
    interface IDisposable with
        member this.Dispose() =
            messageBox.Dispose()
    
type private Model<'msg>(dispatch: 'msg -> unit, maybeParent: Widget.Handle option) as this =
    inherit ModelCore<'msg>(dispatch)
    let messageBox =
        let parentHandle =
            maybeParent
            |> Option.defaultValue null
        MessageBox.Create(parentHandle, this)
    do
        this.MessageBox <- messageBox

let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (initialMask: MessageBox.SignalMask) (maybeParent: Widget.Handle option) =
    let model = new Model<'msg>(dispatch, maybeParent)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- initialMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: MessageBox.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()
    
type MessageBoxBinding internal(handle: MessageBox.Handle) =
    inherit Dialog.DialogBinding(handle)
    // inherits from Dialog, use .Exec() I guess?
    // not sure if we want the other methods but ... we're staying true to the Qt API I guess
    
let bindNode (name: string) (map: Map<string, IViewBinding>) =
    match map.TryFind name with
    | Some (:? MessageBoxBinding as messageBox) ->
        messageBox
    | _ ->
        failwith "MessageBox.bindNode fail"

type MessageBox<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    interface IDialogNode<'msg> with
        override this.Dependencies = []
        
        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask buildContext.ContainingWindow
            
        override this.AttachDeps() =
            ()

        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> MessageBox<'msg>)
            let nextAttrs =
                diffAttrs left'.Attrs this.Attrs
                |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask
            
        override this.Dispose() =
            (this.model :> IDisposable).Dispose()

        override this.Dialog =
            this.model.MessageBox
            
        override this.ContentKey =
            this.model.MessageBox
            
        override this.Attachments =
            this.Attachments

        override this.Binding =
            this.MaybeBoundName
            |> Option.map (fun name ->
                name, MessageBoxBinding(this.model.MessageBox))
            
let execMessageBox name (msgFunc: StandardButton -> 'msg) =
    Cmd.ViewExec (fun bindings ->
        viewexec bindings {
            let! dialog = bindNode name
            let code =
                dialog.Exec()
                |> enum<MessageBox.StandardButton>
                |> StandardButton.FromQtValue
            return (msgFunc code)
        })
    
let execMessageBoxWithoutResult name =
    Cmd.ViewExec (fun bindings ->
        viewexec bindings {
            let! dialog = bindNode name
            dialog.Exec() |> ignore
        })
