module FSharpQt.Widgets.FileDialog

open System
open FSharpQt.BuilderNode
open Org.Whatever.MinimalQtForFSharp

open FSharpQt.Attrs

type internal Signal =
    | CurrentChanged of path: string
    | CurrentUrlChanged of url: string
    | DirectoryEntered of dir: string
    | DirectoryUrlEntered of url: string
    | FileSelected of file: string
    | FilesSelected of files: string list
    | FilterSelected of filter: string
    | UrlSelected of url: string
    | UrlsSelected of urls: string list
    
type FileMode =
    | AnyFile
    | ExistingFile
    | Directory
    | ExistingFiles
with
    member internal this.QtValue =
        match this with
        | AnyFile -> FileDialog.FileMode.AnyFile
        | ExistingFile -> FileDialog.FileMode.ExistingFile
        | Directory -> FileDialog.FileMode.Directory
        | ExistingFiles -> FileDialog.FileMode.ExistingFiles

type ViewMode =
    | Detail
    | List
with
    member internal this.QtValue =
        match this with
        | Detail -> FileDialog.ViewMode.Detail
        | List -> FileDialog.ViewMode.List
    
type AcceptMode =
    | Open
    | Save
with
    member internal this.QtValue =
        match this with
        | Open -> FileDialog.AcceptMode.Open
        | Save -> FileDialog.AcceptMode.Save
        
type FileDialogOption =
    | ShowDirsOnly
    | DontResolveSymlinks
    | DontConfirmOverwrite
    | DontUseNativeDialog
    | ReadOnly
    | HideNameFilterDetails
    | DontUseCustomDirectoryIcons
with
    static member QtSetFrom (options: FileDialogOption seq) =
        (enum<FileDialog.Options> 0, options)
        ||> Seq.fold (fun acc opt ->
            let flag =
                match opt with
                | ShowDirsOnly -> FileDialog.Options.ShowDirsOnly
                | DontResolveSymlinks -> FileDialog.Options.DontResolveSymlinks
                | DontConfirmOverwrite -> FileDialog.Options.DontConfirmOverwrite
                | DontUseNativeDialog -> FileDialog.Options.DontUseNativeDialog
                | ReadOnly -> FileDialog.Options.ReadOnly
                | HideNameFilterDetails -> FileDialog.Options.HideNameFilterDetails
                | DontUseCustomDirectoryIcons -> FileDialog.Options.DontUseCustomDirectoryIcons
            acc ||| flag)

type internal Attr =
    | AcceptMode of mode: AcceptMode
    | DefaultSuffix of suffix: string
    | FileMode of mode: FileMode
    | Options of opts: Set<FileDialogOption>
    | SupportedSchemes of schemes: string list
    | ViewMode of mode: ViewMode
    | NameFilter of filter: string
    | NameFilters of filters: string list
    | MimeTypeFilters of filters: string list
    | Directory of dir: string
    | SelectedFile of file: string
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
            | AcceptMode _ -> "filedialog:acceptmode"
            | DefaultSuffix _ -> "filedialog:defaultsuffix"
            | FileMode _ -> "filedialog:filemode"
            | Options _ -> "filedialog:options"
            | SupportedSchemes _ -> "filedialog:supportschemes"
            | ViewMode _ -> "filedialog:viewmode"
            | NameFilter _ -> "filedialog:namefilter"
            | NameFilters _ -> "filedialog:namefilters"
            | MimeTypeFilters _ -> "filedialog:mimetypefilters"
            | Directory _ -> "filedialog:directory"
            | SelectedFile _ -> "filedialog:selectedfile"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyFileDialogAttr(this)
            | _ ->
                printfn "warning: FileDialog.Attr couldn't ApplyTo() unknown target type [%A]" target
                
and internal AttrTarget =
    interface
        inherit Dialog.AttrTarget
        abstract member ApplyFileDialogAttr: Attr -> unit
    end
        
type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)

type Props<'msg>() =
    inherit Dialog.Props<'msg>()
    
    let mutable onCurrentChanged: (string -> 'msg) option = None
    let mutable onCurrentUrlChanged: (string -> 'msg) option = None
    let mutable onDirectoryEntered: (string -> 'msg) option = None
    let mutable onDirectoryUrlEntered: (string -> 'msg) option = None
    let mutable onFileSelected: (string -> 'msg) option = None
    let mutable onFilesSelected: (string list -> 'msg) option = None
    let mutable onFilterSelected: (string -> 'msg) option = None
    let mutable onUrlSelected: (string -> 'msg) option = None
    let mutable onUrlsSelected: (string list -> 'msg) option = None
    
    member this.SignalMask = enum<FileDialog.SignalMask> (int this._signalMask)
    
    member this.OnCurrentChanged with set value =
        onCurrentChanged <- Some value
        this.AddSignal(int FileDialog.SignalMask.CurrentChanged)
        
    member this.OnCurrentUrlChanged with set value =
        onCurrentUrlChanged <- Some value
        this.AddSignal(int FileDialog.SignalMask.CurrentUrlChanged)
        
    member this.OnDirectoryEntered with set value =
        onDirectoryEntered <- Some value
        this.AddSignal(int FileDialog.SignalMask.DirectoryEntered)
        
    member this.OnDirectoryUrlEntered with set value =
        onDirectoryUrlEntered <- Some value
        this.AddSignal(int FileDialog.SignalMask.DirectoryUrlEntered)
        
    member this.OnFileSelected with set value =
        onFileSelected <- Some value
        this.AddSignal(int FileDialog.SignalMask.FileSelected)
        
    member this.OnFilesSelected with set value =
        onFilesSelected <- Some value
        this.AddSignal(int FileDialog.SignalMask.FilesSelected)
        
    member this.OnFilterSelected with set value =
        onFilterSelected <- Some value
        this.AddSignal(int FileDialog.SignalMask.FilterSelected)
        
    member this.OnUrlSelected with set value =
        onUrlSelected <- Some value
        this.AddSignal(int FileDialog.SignalMask.UrlSelected)
        
    member this.OnUrlsSelected with set value =
        onUrlsSelected <- Some value
        this.AddSignal(int FileDialog.SignalMask.UrlsSelected)
        
    member internal this.SignalMapList =
        let thisFunc = function
            | CurrentChanged path ->
                onCurrentChanged
                |> Option.map (fun f -> f path)
            | CurrentUrlChanged url ->
                onCurrentUrlChanged
                |> Option.map (fun f -> f url)
            | DirectoryEntered dir ->
                onDirectoryEntered
                |> Option.map (fun f -> f dir)
            | DirectoryUrlEntered url ->
                onDirectoryUrlEntered
                |> Option.map (fun f -> f url)
            | FileSelected file ->
                onFileSelected
                |> Option.map (fun f -> f file)
            | FilesSelected files ->
                onFilesSelected
                |> Option.map (fun f -> f files)
            | FilterSelected filter ->
                onFilterSelected
                |> Option.map (fun f -> f filter)
            | UrlSelected url ->
                onUrlSelected
                |> Option.map (fun f -> f url)
            | UrlsSelected urls ->
                onUrlsSelected
                |> Option.map (fun f -> f urls)
        // prepend to parent signal map funcs
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList
        
    member this.AcceptMode with set value =
        this.PushAttr(AcceptMode value)

    member this.DefaultSuffix with set value =
        this.PushAttr(DefaultSuffix value)

    member this.FileMode with set value =
        this.PushAttr(FileMode value)

    member this.Options with set value =
        this.PushAttr(value |> Set.ofSeq |> Options)

    member this.SupportedSchemes with set value =
        this.PushAttr(value |> Seq.toList |> SupportedSchemes)

    member this.ViewMode with set value =
        this.PushAttr(ViewMode value)

    member this.NameFilter with set value =
        this.PushAttr(NameFilter value)

    member this.NameFilters with set value =
        this.PushAttr(value |> Seq.toList |> NameFilters)

    member this.MimeTypeFilters with set value =
        this.PushAttr(value |> Seq.toList |> MimeTypeFilters)

    member this.Directory with set value =
        this.PushAttr(Directory value)

    member this.SelectedFile with set value =
        this.PushAttr(SelectedFile value)
                
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit Dialog.ModelCore<'msg>(dispatch)
    let mutable fileDialog: FileDialog.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<FileDialog.SignalMask> 0
    
    // TODO: binding guards (extremely unclear)
    // lastCurrent
    // lastSelectedFile
    // lastSelectedFilter
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.FileDialog
        with get() = fileDialog
        and set value =
            // assign to base
            this.Dialog <- value
            fileDialog <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "FileDialog.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "FileDialog.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            fileDialog.SetSignalMask(value)
            currentMask <- value
            
    interface AttrTarget with
        member this.ApplyFileDialogAttr attr =
            match attr with
            | AcceptMode mode ->
                fileDialog.SetAcceptMode(mode.QtValue)
            | DefaultSuffix suffix ->
                fileDialog.SetDefaultSuffix(suffix)
            | FileMode mode ->
                fileDialog.SetFileMode(mode.QtValue)
            | Options opts ->
                fileDialog.SetOptions(FileDialogOption.QtSetFrom opts)
            | SupportedSchemes schemes ->
                fileDialog.SetSupportedSchemes(schemes |> Array.ofList)
            | ViewMode mode ->
                fileDialog.SetViewMode(mode.QtValue)
            | NameFilter filter ->
                fileDialog.SetNameFilter(filter)
            | NameFilters filters ->
                fileDialog.SetNameFilters(filters |> Array.ofList)
            | MimeTypeFilters filters ->
                fileDialog.SetMimeTypeFilters(filters |> Array.ofList)
            | Directory dir ->
                fileDialog.SetDirectory(dir)
            | SelectedFile file ->
                fileDialog.SelectFile(file)
        
    interface FileDialog.SignalHandler with
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
        member this.CurrentChanged path =
            signalDispatch (CurrentChanged path)
        member this.CurrentUrlChanged url =
            signalDispatch (CurrentUrlChanged url)
        member this.DirectoryEntered dir =
            signalDispatch (DirectoryEntered dir)
        member this.DirectoryUrlEntered url =
            signalDispatch (DirectoryUrlEntered url)
        member this.FileSelected file =
            signalDispatch (FileSelected file)
        member this.FilesSelected files =
            signalDispatch (files |> Array.toList |> FilesSelected)
        member this.FilterSelected filter =
            signalDispatch (FilterSelected filter)
        member this.UrlSelected url =
            signalDispatch (UrlSelected url)
        member this.UrlsSelected urls =
            signalDispatch (urls |> Array.toList |> UrlsSelected)
            
    interface IDisposable with
        member this.Dispose() =
            fileDialog.Dispose()

type private Model<'msg>(dispatch: 'msg -> unit, maybeParent: Widget.Handle option) as this =
    inherit ModelCore<'msg>(dispatch)
    let fileDialog =
        let parentHandle =
            maybeParent
            |> Option.defaultValue null
        FileDialog.Create(parentHandle, this)
    do
        this.FileDialog <- fileDialog

let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (initialMask: FileDialog.SignalMask) (maybeParent: Widget.Handle option) =
    let model = new Model<'msg>(dispatch, maybeParent)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- initialMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: FileDialog.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()

type FileDialog<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    interface IDialogNode<'msg> with
        override this.Dependencies = []
            
        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask buildContext.ContainingWindow
            
        override this.AttachDeps () =
            ()
            
        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> FileDialog<'msg>)
            let nextAttrs =
                diffAttrs left'.Attrs this.Attrs
                |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask
            
        override this.Dispose() =
            (this.model :> IDisposable).Dispose()
            
        override this.Dialog =
            this.model.FileDialog
            
        override this.ContentKey =
            this.model.FileDialog
            
        override this.Attachments =
            this.Attachments

        override this.Binding = None
