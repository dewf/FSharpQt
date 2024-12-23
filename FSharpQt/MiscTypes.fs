﻿module FSharpQt.MiscTypes

open System
open Org.Whatever.MinimalQtForFSharp
open FSharpQt.InputEnums

type SizePolicy =
    | Fixed
    | Minimum
    | Maximum
    | Preferred
    | MinimumExpanding
    | Expanding
    | Ignored
with
    member this.QtValue =
        match this with
        | Fixed -> SizePolicy.Policy.Fixed
        | Minimum -> SizePolicy.Policy.Minimum
        | Maximum -> SizePolicy.Policy.Maximum
        | Preferred -> SizePolicy.Policy.Preferred
        | MinimumExpanding -> SizePolicy.Policy.MinimumExpanding
        | Expanding -> SizePolicy.Policy.Expanding
        | Ignored -> SizePolicy.Policy.Ignored

type Alignment =
    | Left
    | Leading
    | Right
    | Trailing
    | HCenter
    | Justify
    | Absolute
    | Top
    | Bottom
    | VCenter
    | Baseline
    | Center
with
    member internal this.QtValue =
        match this with
        | Left -> Enums.Alignment.AlignLeft
        | Leading -> Enums.Alignment.AlignLeading
        | Right -> Enums.Alignment.AlignRight
        | Trailing -> Enums.Alignment.AlignTrailing
        | HCenter -> Enums.Alignment.AlignHCenter
        | Justify -> Enums.Alignment.AlignJustify
        | Absolute -> Enums.Alignment.AlignAbsolute
        | Top -> Enums.Alignment.AlignTop
        | Bottom -> Enums.Alignment.AlignBottom
        | VCenter ->Enums.Alignment.AlignVCenter
        | Baseline -> Enums.Alignment.AlignBaseline
        | Center -> Enums.Alignment.AlignCenter
        
type Orientation =
    | Horizontal
    | Vertical
with
    member this.QtValue =
        match this with
        | Horizontal -> Enums.Orientation.Horizontal
        | Vertical -> Enums.Orientation.Vertical
    static member From (o: Enums.Orientation) =
        match o with
        | Enums.Orientation.Horizontal -> Horizontal
        | Enums.Orientation.Vertical -> Vertical
        | _ -> failwith "Orientation.From - unknown input value"
    
type Point = {
    X: int
    Y: int
} with
    static member From (x, y) =
        { X = x; Y = y }
    static member internal From(p: Common.Point) =
        { X = p.X; Y = p.Y }
    member internal this.QtValue =
        Common.Point(X = this.X, Y = this.Y)
        
type PointF = {
    X: double
    Y: double
} with
    static member From (x, y) =
        { X = x; Y = y }
    static member From(p: Point) =
        { X = double p.X; Y = p.Y }
    static member internal From(p: Common.Point) =
        { X = p.X; Y = p.Y }
    static member internal From(p: Common.PointF) =
        { X = p.X; Y = p.Y }
    member internal this.QtValue =
        Common.PointF(X = this.X, Y = this.Y)
        
type Size = {
    Width: int
    Height: int
} with
    static member Invalid =
        // useful in some places
        { Width = -1; Height = -1 }
    static member Zero =
        { Width = 0; Height = 0 }
    static member From (w, h) =
        { Width = w; Height = h }
    static member internal From (sz: Common.Size) =
        { Width = sz.Width; Height = sz.Height }
    member internal this.QtValue =
        Common.Size(Width = this.Width, Height = this.Height)
        
type Rect = {
    X: int
    Y: int
    Width: int
    Height: int
} with
    static member From (x, y, width, height) =
        { X = x; Y = y; Width = width; Height = height }
    static member From (size: Size) =
        { X = 0; Y = 0; Width = size.Width; Height = size.Height }
    static member internal From(rect: Common.Rect) =
        { X = rect.X; Y = rect.Y; Width = rect.Width; Height = rect.Height }
    member internal this.QtValue =
        Common.Rect(X = this.X, Y = this.Y, Width = this.Width, Height = this.Height)

type RectF = {
    X: double
    Y: double
    Width: double
    Height: double
} with
    static member From (x, y, width, height) =
        { X = x; Y = y; Width = width; Height = height }
    static member From (size: Size) =
        { X = 0; Y = 0; Width = size.Width; Height = size.Height }
    static member internal From(rect: Common.Rect) =
        { X = double rect.X; Y = rect.Y; Width = rect.Width; Height = rect.Height }
    static member internal From(rect: Common.RectF) =
        { X = rect.X; Y = rect.Y; Width = rect.Width; Height = rect.Height }
    member internal this.QtValue =
        Common.RectF(X = this.X, Y = this.Y, Width = this.Width, Height = this.Height)
        
type WindowModality =
    | NonModal
    | WindowModal
    | ApplicationModal
with
    member internal this.QtValue =
        match this with
        | NonModal -> Enums.WindowModality.NonModal
        | WindowModal -> Enums.WindowModality.WindowModal
        | ApplicationModal -> Enums.WindowModality.ApplicationModal
        
type ContextMenuPolicy =
    | NoContextMenu
    | DefaultContextMenu
    | ActionsContextMenu
    | CustomContextMenu
    | PreventContextMenu
with
    member internal this.QtValue =
        match this with
        | NoContextMenu -> Enums.ContextMenuPolicy.NoContextMenu
        | DefaultContextMenu -> Enums.ContextMenuPolicy.DefaultContextMenu
        | ActionsContextMenu -> Enums.ContextMenuPolicy.ActionsContextMenu
        | CustomContextMenu -> Enums.ContextMenuPolicy.CustomContextMenu
        | PreventContextMenu -> Enums.ContextMenuPolicy.PreventContextMenu
        
type CursorMoveStyle =
    | LogicalMoveStyle
    | VisualModeStyle
with
    member internal this.QtValue =
        match this with
        | LogicalMoveStyle -> Enums.CursorMoveStyle.LogicalMoveStyle
        | VisualModeStyle -> Enums.CursorMoveStyle.VisualMoveStyle
        
type DropAction =
    | Ignore
    | Copy
    | Move
    | Link
with
    static member internal From (qtDropAction: Enums.DropAction) =
        match qtDropAction with
        | Enums.DropAction.Ignore -> Ignore
        | Enums.DropAction.Copy -> Copy
        | Enums.DropAction.Move -> Move
        | Enums.DropAction.Link -> Link
        | _ -> failwith "DropAction.From - unhandled DropAction case (only move/copy/link supported)"
    member internal this.QtValue =
        match this with
        | Ignore -> Enums.DropAction.Ignore
        | Copy -> Enums.DropAction.Copy
        | Move -> Enums.DropAction.Move
        | Link -> Enums.DropAction.Link
    static member internal SetFrom (qtDropActionSet: Enums.DropActionSet) =
        let pairs = [
            // Enums.DropActionSet.Ignore, Ignore // 0 value
            Enums.DropActionSet.Copy, Copy
            Enums.DropActionSet.Move, Move
            Enums.DropActionSet.Link, Link
        ]
        (Set.empty<DropAction>, pairs)
        ||> List.fold (fun acc (flag, action) ->
            if qtDropActionSet.HasFlag flag then
                acc.Add(action)
            else
                acc)
    static member internal QtSetFrom (actions: DropAction seq) =
        (enum<Enums.DropActionSet> 0, actions)
        ||> Seq.fold (fun acc action ->
            let flag =
                match action with
                | Ignore -> Enums.DropActionSet.Ignore // 0 value, meaningless in a set
                | Copy -> Enums.DropActionSet.Copy
                | Move -> Enums.DropActionSet.Move
                | Link -> Enums.DropActionSet.Link
            acc ||| flag)
        
type TextFormat =
    | PlainText
    | RichText
    | AutoText
    | MarkdownText
with
    member internal this.QtValue =
        match this with
        | PlainText -> Enums.TextFormat.PlainText
        | RichText -> Enums.TextFormat.RichText
        | AutoText -> Enums.TextFormat.AutoText
        | MarkdownText -> Enums.TextFormat.MarkdownText
        
type TextInteractionFlag =
    | TextSelectableByMouse
    | TextSelectableByKeyboard
    | LinksAccessibleByMouse
    | LinksAccessibleByKeyboard
    | TextEditable
    | TextEditorInteraction
    | TextBrowserInteraction
with
    static member internal QtSetFrom (flags: TextInteractionFlag seq) =
        (enum<Enums.TextInteractionFlags> 0, flags)
        ||> Seq.fold (fun acc item ->
            let flag =
                match item with
                | TextSelectableByMouse -> Enums.TextInteractionFlags.TextSelectableByMouse
                | TextSelectableByKeyboard -> Enums.TextInteractionFlags.TextSelectableByMouse
                | LinksAccessibleByMouse -> Enums.TextInteractionFlags.TextSelectableByMouse
                | LinksAccessibleByKeyboard -> Enums.TextInteractionFlags.TextSelectableByMouse
                | TextEditable -> Enums.TextInteractionFlags.TextSelectableByMouse
                | TextEditorInteraction -> Enums.TextInteractionFlags.TextSelectableByMouse
                | TextBrowserInteraction -> Enums.TextInteractionFlags.TextSelectableByMouse
            acc ||| flag)
        
type CaseSensitivity =
    | CaseInsensitive
    | CaseSensitive
with
    static member internal From (qtValue: Enums.CaseSensitivity) =
        match qtValue with
        | Enums.CaseSensitivity.CaseInsensitive -> CaseInsensitive
        | Enums.CaseSensitivity.CaseSensitive -> CaseSensitive
        | _ -> failwithf "CaseSensitivity.From: unknown input value [%d]" (int qtValue)
    member internal this.QtValue =
        match this with
        | CaseInsensitive -> Enums.CaseSensitivity.CaseInsensitive
        | CaseSensitive -> Enums.CaseSensitivity.CaseSensitive
        
type TextElideMode =
    | ElideLeft
    | ElideRight
    | ElideMiddle
    | ElideNone
with
    member internal this.QtValue =
        match this with
        | ElideLeft -> Enums.TextElideMode.ElideLeft
        | ElideRight -> Enums.TextElideMode.ElideRight
        | ElideMiddle -> Enums.TextElideMode.ElideMiddle
        | ElideNone -> Enums.TextElideMode.ElideNone

type ShortcutContext =
    | WidgetShortcut
    | WindowShortcut
    | ApplicationShortcut
    | WidgetWithChildrenShortcut
with
    member internal this.QtValue =
        match this with
        | WidgetShortcut -> Enums.ShortcutContext.WidgetShortcut
        | WindowShortcut -> Enums.ShortcutContext.WindowShortcut
        | ApplicationShortcut -> Enums.ShortcutContext.ApplicationShortcut
        | WidgetWithChildrenShortcut -> Enums.ShortcutContext.WidgetWithChildrenShortcut
        
// in C++ Qt this is a class,
// will decide over time what it needs to be here ...
module TextOption =
    type WrapMode =
        | NoWrap
        | WordWrap
        | ManualWrap
        | WrapAnywhere
        | WrapAtWordBoundaryOrAnywhere
    with
        member internal this.QtValue =
            match this with
            | NoWrap -> TextOption.WrapMode.NoWrap
            | WordWrap -> TextOption.WrapMode.WordWrap
            | ManualWrap -> TextOption.WrapMode.ManualWrap
            | WrapAnywhere -> TextOption.WrapMode.WrapAnywhere
            | WrapAtWordBoundaryOrAnywhere -> TextOption.WrapMode.WrapAtWordBoundaryOrAnywhere
            
type TimerType =
    | PreciseTimer
    | CoarseTimer
    | VeryCoarseTimer
with
    member internal this.QtValue =
        match this with
        | PreciseTimer -> Enums.TimerType.PreciseTimer
        | CoarseTimer -> Enums.TimerType.CoarseTimer
        | VeryCoarseTimer -> Enums.TimerType.VeryCoarseTimer
        
type ToolBarArea =
    | LeftToolBarArea
    | RightToolBarArea
    | TopToolBarArea
    | BottomToolBarArea
with
    static member internal NoToolBarAreas = Set.empty<ToolBarArea>
    static member internal AllToolBarAreas =
        [ LeftToolBarArea; RightToolBarArea; TopToolBarArea; BottomToolBarArea ]
        |> Set.ofList
    member internal this.QtValue =
        match this with
        | LeftToolBarArea -> Enums.ToolBarAreas.LeftToolBarArea
        | RightToolBarArea -> Enums.ToolBarAreas.RightToolBarArea
        | TopToolBarArea -> Enums.ToolBarAreas.TopToolBarArea
        | BottomToolBarArea -> Enums.ToolBarAreas.BottomToolBarArea
        // | AllToolBarAreas -> Enums.ToolBarAreas.AllToolBarAreas
        // | NoToolBarArea -> Enums.ToolBarAreas.NoToolBarArea
    static member internal QtSetFrom (values: ToolBarArea seq) =
        (enum<Enums.ToolBarAreas> 0, values)
        ||> Seq.fold (fun acc item ->
            acc ||| item.QtValue)
    static member internal SetFrom (qtValues: Enums.ToolBarAreas) =
        let pairs = [
            Enums.ToolBarAreas.LeftToolBarArea, LeftToolBarArea
            Enums.ToolBarAreas.RightToolBarArea, RightToolBarArea
            Enums.ToolBarAreas.TopToolBarArea, TopToolBarArea
            Enums.ToolBarAreas.BottomToolBarArea, BottomToolBarArea
        ]
        (Set.empty<ToolBarArea>, pairs)
        ||> List.fold (fun acc (flag, value) ->
            if qtValues.HasFlag(flag) then
                acc.Add(value)
            else
                acc)

type ToolButtonStyle =
    | ToolButtonIconOnly
    | ToolButtonTextOnly
    | ToolButtonTextBesideIcon
    | ToolButtonTextUnderIcon
    | ToolButtonFollowStyle
with
    member internal this.QtValue =
        match this with
        | ToolButtonIconOnly -> Enums.ToolButtonStyle.ToolButtonIconOnly
        | ToolButtonTextOnly -> Enums.ToolButtonStyle.ToolButtonTextOnly
        | ToolButtonTextBesideIcon -> Enums.ToolButtonStyle.ToolButtonTextBesideIcon
        | ToolButtonTextUnderIcon -> Enums.ToolButtonStyle.ToolButtonTextUnderIcon
        | ToolButtonFollowStyle -> Enums.ToolButtonStyle.ToolButtonFollowStyle
    static member internal From (style: Enums.ToolButtonStyle) =
        match style with
        | Enums.ToolButtonStyle.ToolButtonIconOnly -> ToolButtonIconOnly
        | Enums.ToolButtonStyle.ToolButtonTextOnly -> ToolButtonTextOnly
        | Enums.ToolButtonStyle.ToolButtonTextBesideIcon -> ToolButtonTextBesideIcon
        | Enums.ToolButtonStyle.ToolButtonTextUnderIcon -> ToolButtonTextUnderIcon
        | Enums.ToolButtonStyle.ToolButtonFollowStyle -> ToolButtonFollowStyle
        | _ -> failwith "ToolButtonStyle.From - unknown input value"
    
type ThemeIcon =
    | AddressBookNew = 0
    | ApplicationExit = 1
    | AppointmentNew = 2
    | CallStart = 3
    | CallStop = 4
    | ContactNew = 5
    | DocumentNew = 6
    | DocumentOpen = 7
    | DocumentOpenRecent = 8
    | DocumentPageSetup = 9
    | DocumentPrint = 10
    | DocumentPrintPreview = 11
    | DocumentProperties = 12
    | DocumentRevert = 13
    | DocumentSave = 14
    | DocumentSaveAs = 15
    | DocumentSend = 16
    | EditClear = 17
    | EditCopy = 18
    | EditCut = 19
    | EditDelete = 20
    | EditFind = 21
    | EditPaste = 22
    | EditRedo = 23
    | EditSelectAll = 24
    | EditUndo = 25
    | FolderNew = 26
    | FormatIndentLess = 27
    | FormatIndentMore = 28
    | FormatJustifyCenter = 29
    | FormatJustifyFill = 30
    | FormatJustifyLeft = 31
    | FormatJustifyRight = 32
    | FormatTextDirectionLtr = 33
    | FormatTextDirectionRtl = 34
    | FormatTextBold = 35
    | FormatTextItalic = 36
    | FormatTextUnderline = 37
    | FormatTextStrikethrough = 38
    | GoDown = 39
    | GoHome = 40
    | GoNext = 41
    | GoPrevious = 42
    | GoUp = 43
    | HelpAbout = 44
    | HelpFaq = 45
    | InsertImage = 46
    | InsertLink = 47
    | InsertText = 48
    | ListAdd = 49
    | ListRemove = 50
    | MailForward = 51
    | MailMarkImportant = 52
    | MailMarkRead = 53
    | MailMarkUnread = 54
    | MailMessageNew = 55
    | MailReplyAll = 56
    | MailReplySender = 57
    | MailSend = 58
    | MediaEject = 59
    | MediaPlaybackPause = 60
    | MediaPlaybackStart = 61
    | MediaPlaybackStop = 62
    | MediaRecord = 63
    | MediaSeekBackward = 64
    | MediaSeekForward = 65
    | MediaSkipBackward = 66
    | MediaSkipForward = 67
    | ObjectRotateLeft = 68
    | ObjectRotateRight = 69
    | ProcessStop = 70
    | SystemLockScreen = 71
    | SystemLogOut = 72
    | SystemSearch = 73
    | SystemReboot = 74
    | SystemShutdown = 75
    | ToolsCheckSpelling = 76
    | ViewFullscreen = 77
    | ViewRefresh = 78
    | ViewRestore = 79
    | WindowClose = 80
    | WindowNew = 81
    | ZoomFitBest = 82
    | ZoomIn = 83
    | ZoomOut = 84
    | AudioCard = 85
    | AudioInputMicrophone = 86
    | Battery = 87
    | CameraPhoto = 88
    | CameraVideo = 89
    | CameraWeb = 90
    | Computer = 91
    | DriveHarddisk = 92
    | DriveOptical = 93
    | InputGaming = 94
    | InputKeyboard = 95
    | InputMouse = 96
    | InputTablet = 97
    | MediaFlash = 98
    | MediaOptical = 99
    | MediaTape = 100
    | MultimediaPlayer = 101
    | NetworkWired = 102
    | NetworkWireless = 103
    | Phone = 104
    | Printer = 105
    | Scanner = 106
    | VideoDisplay = 107
    | AppointmentMissed = 108
    | AppointmentSoon = 109
    | AudioVolumeHigh = 110
    | AudioVolumeLow = 111
    | AudioVolumeMedium = 112
    | AudioVolumeMuted = 113
    | BatteryCaution = 114
    | BatteryLow = 115
    | DialogError = 116
    | DialogInformation = 117
    | DialogPassword = 118
    | DialogQuestion = 119
    | DialogWarning = 120
    | FolderDragAccept = 121
    | FolderOpen = 122
    | FolderVisiting = 123
    | ImageLoading = 124
    | ImageMissing = 125
    | MailAttachment = 126
    | MailUnread = 127
    | MailRead = 128
    | MailReplied = 129
    | MediaPlaylistRepeat = 130
    | MediaPlaylistShuffle = 131
    | NetworkOffline = 132
    | PrinterPrinting = 133
    | SecurityHigh = 134
    | SecurityLow = 135
    | SoftwareUpdateAvailable = 136
    | SoftwareUpdateUrgent = 137
    | SyncError = 138
    | SyncSynchronizing = 139
    | UserAvailable = 140
    | UserOffline = 141
    | WeatherClear = 142
    | WeatherClearNight = 143
    | WeatherFewClouds = 144
    | WeatherFewCloudsNight = 145
    | WeatherFog = 146
    | WeatherShowers = 147
    | WeatherSnow = 148
    | WeatherStorm = 149
    
let internal toQtThemeIcon (icon: ThemeIcon) =
    enum<Icon.ThemeIcon> (int icon)

type CheckState =
    | Unchecked
    | PartiallyChecked
    | Checked
with
    member this.QtValue =
        match this with
        | Unchecked -> Enums.CheckState.Unchecked
        | PartiallyChecked -> Enums.CheckState.PartiallyChecked
        | Checked -> Enums.CheckState.Checked
        
type SortOrder =
    | AscendingOrder
    | DescendingOrder
with
    member internal this.QtValue =
        match this with
        | AscendingOrder -> Enums.SortOrder.AscendingOrder
        | DescendingOrder -> Enums.SortOrder.DescendingOrder
    
type ItemDataRole =
    | DisplayRole
    | DecorationRole
    | EditRole
    | ToolTipRole
    | StatusTipRole
    | WhatsThisRole
    | FontRole
    | TextAlignmentRole
    | BackgroundRole
    | ForegroundRole
    | CheckStateRole
    | AccessibleTextRole
    | AccessibleDescriptionRole
    | SizeHintRole
    | InitialSortOrderRole
    | DisplayPropertyRole
    | DecorationPropertyRole
    | ToolTipPropertyRole
    | StatusTipPropertyRole
    | WhatsThisPropertyRole
    | UserRole of value: int   // over 0x0100
with
    member internal this.QtValue =
        match this with
        | DisplayRole -> Enums.ItemDataRole.DisplayRole
        | DecorationRole -> Enums.ItemDataRole.DecorationRole
        | EditRole -> Enums.ItemDataRole.EditRole
        | ToolTipRole -> Enums.ItemDataRole.ToolTipRole
        | StatusTipRole -> Enums.ItemDataRole.StatusTipRole
        | WhatsThisRole -> Enums.ItemDataRole.WhatsThisRole
        | FontRole -> Enums.ItemDataRole.FontRole
        | TextAlignmentRole -> Enums.ItemDataRole.TextAlignmentRole
        | BackgroundRole -> Enums.ItemDataRole.BackgroundRole
        | ForegroundRole -> Enums.ItemDataRole.ForegroundRole
        | CheckStateRole -> Enums.ItemDataRole.CheckStateRole
        | AccessibleTextRole -> Enums.ItemDataRole.AccessibleTextRole
        | AccessibleDescriptionRole -> Enums.ItemDataRole.AccessibleDescriptionRole
        | SizeHintRole -> Enums.ItemDataRole.SizeHintRole
        | InitialSortOrderRole -> Enums.ItemDataRole.InitialSortOrderRole
        | DisplayPropertyRole -> Enums.ItemDataRole.DisplayPropertyRole
        | DecorationPropertyRole -> Enums.ItemDataRole.DecorationPropertyRole
        | ToolTipPropertyRole -> Enums.ItemDataRole.ToolTipPropertyRole
        | StatusTipPropertyRole -> Enums.ItemDataRole.StatusTipPropertyRole
        | WhatsThisPropertyRole -> Enums.ItemDataRole.WhatsThisPropertyRole
        | UserRole value -> enum<Enums.ItemDataRole> value
    static member internal From (role: Enums.ItemDataRole) =
        match role with
        | Enums.ItemDataRole.DisplayRole -> DisplayRole
        | Enums.ItemDataRole.DecorationRole -> DecorationRole
        | Enums.ItemDataRole.EditRole -> EditRole
        | Enums.ItemDataRole.ToolTipRole -> ToolTipRole
        | Enums.ItemDataRole.StatusTipRole -> StatusTipRole
        | Enums.ItemDataRole.WhatsThisRole -> WhatsThisRole
        | Enums.ItemDataRole.FontRole -> FontRole
        | Enums.ItemDataRole.TextAlignmentRole -> TextAlignmentRole
        | Enums.ItemDataRole.BackgroundRole -> BackgroundRole
        | Enums.ItemDataRole.ForegroundRole -> ForegroundRole
        | Enums.ItemDataRole.CheckStateRole -> CheckStateRole
        | Enums.ItemDataRole.AccessibleTextRole -> AccessibleTextRole
        | Enums.ItemDataRole.AccessibleDescriptionRole -> AccessibleDescriptionRole
        | Enums.ItemDataRole.SizeHintRole -> SizeHintRole
        | Enums.ItemDataRole.InitialSortOrderRole -> InitialSortOrderRole
        | Enums.ItemDataRole.DisplayPropertyRole -> DisplayPropertyRole
        | Enums.ItemDataRole.DecorationPropertyRole -> DecorationPropertyRole
        | Enums.ItemDataRole.ToolTipPropertyRole -> ToolTipPropertyRole
        | Enums.ItemDataRole.StatusTipPropertyRole -> StatusTipPropertyRole
        | Enums.ItemDataRole.WhatsThisPropertyRole -> WhatsThisPropertyRole
        | _ ->
            let value = int role
            if value >= 0x100 then
                UserRole value
            else
                failwithf "DataRole.From: unknown input value [%d]" value
                
type ItemFlag =
    | ItemIsSelectable
    | ItemIsEditable
    | ItemIsDragEnabled
    | ItemIsDropEnabled
    | ItemIsUserCheckable
    | ItemIsEnabled
    | ItemIsAutoTristate
    | ItemNeverHasChildren
    | ItemIsUserTristate
with
    member internal this.QtFlag =
        match this with
        | ItemIsSelectable -> AbstractListModel.ItemFlags.ItemIsSelectable
        | ItemIsEditable -> AbstractListModel.ItemFlags.ItemIsEditable
        | ItemIsDragEnabled -> AbstractListModel.ItemFlags.ItemIsDragEnabled
        | ItemIsDropEnabled -> AbstractListModel.ItemFlags.ItemIsDropEnabled
        | ItemIsUserCheckable -> AbstractListModel.ItemFlags.ItemIsUserCheckable
        | ItemIsEnabled -> AbstractListModel.ItemFlags.ItemIsEnabled
        | ItemIsAutoTristate -> AbstractListModel.ItemFlags.ItemIsAutoTristate
        | ItemNeverHasChildren -> AbstractListModel.ItemFlags.ItemNeverHasChildren
        | ItemIsUserTristate -> AbstractListModel.ItemFlags.ItemIsUserTristate
    static member internal QtSetFrom (flags: ItemFlag seq) =
        (enum<AbstractListModel.ItemFlags> 0, flags)
        ||> Seq.fold (fun acc flag -> acc ||| flag.QtFlag)
    static member internal SetFrom (inputFlags: AbstractListModel.ItemFlags) =
        let pairs =
            [ AbstractListModel.ItemFlags.ItemIsSelectable, ItemIsSelectable
              AbstractListModel.ItemFlags.ItemIsEditable, ItemIsEditable
              AbstractListModel.ItemFlags.ItemIsDragEnabled, ItemIsDragEnabled
              AbstractListModel.ItemFlags.ItemIsDropEnabled, ItemIsDropEnabled
              AbstractListModel.ItemFlags.ItemIsUserCheckable, ItemIsUserCheckable
              AbstractListModel.ItemFlags.ItemIsEnabled, ItemIsEnabled
              AbstractListModel.ItemFlags.ItemIsAutoTristate, ItemIsAutoTristate
              AbstractListModel.ItemFlags.ItemNeverHasChildren, ItemNeverHasChildren
              AbstractListModel.ItemFlags.ItemIsUserTristate, ItemIsUserTristate ]
        (Set.empty<ItemFlag>, pairs)
        ||> List.fold (fun acc (qtFlag, fsFlag) ->
            if inputFlags.HasFlag qtFlag then
                acc.Add(fsFlag)
            else
                acc)
                
// // for utility widgets (synthetic layout widgets etc)
// NEW: just use Widget.CreateNoHandler()
// type internal NullWidgetHandler() =
//     interface Widget.SignalHandler with
//         member this.Destroyed obj =
//             ()
//         member this.ObjectNameChanged name =
//             ()
//         member this.CustomContextMenuRequested pos =
//             ()
//         member this.WindowIconChanged icon =
//             ()
//         member this.WindowTitleChanged title =
//             ()
//         member this.Dispose() =
//             ()
    
// for anything where we don't want users to be dealing with Org.Whatever.MinimalQtForFSharp namespace (generated C# code)
// generally these are for signals and callbacks of various kinds where the user might need to query some values

// but ideally the node/view API will use deferred stuff (eg Icon.Deferred) instead of proxies,
// because we don't want users to be responsible for lifetimes (disposal) on these things
// anything the user would have created on the stack, ideally shouldn't use a proxy

type QObjectProxy internal(handle: Object.Handle) =
    let x = 10

type WidgetProxy internal(handle: Widget.Handle) =
    member val internal Handle = handle
    member this.Rect = Rect.From(handle.Rect())
    member this.Width = handle.Width()
    member this.Height = handle.Height()
        
type AbstractButtonProxy internal(handle: AbstractButton.Handle) =
    let x = 10

type ActionProxy internal(action: Action.Handle) =
    // not sure what methods/props will be useful yet
    let x = 10

type IconProxy internal(icon: Icon.Handle) =
    // just put this here due to a signal needing it
    // user-created icons (for node construction) are further below
    let x = 10
    
type DockWidgetProxy internal(widget: DockWidget.Handle) =
    let x = 10
    
type MimeDataProxy internal(qMimeData: Widget.MimeData) =
    member val qMimeData = qMimeData
    member this.HasFormat(mimeType: string) =
        qMimeData.HasFormat(mimeType)
    member this.Text =
        qMimeData.Text()
    member this.Urls =
        qMimeData.Urls()
        
type StyleOptionViewItemProxy internal(thing: StyleOptionViewItem.Handle) =
    let x = 10
    
type ComboBoxProxy internal(model: ComboBox.Handle) =
    member this.CurrentIndex
        with get() =
            model.CurrentIndex()
        and set value =
            model.SetCurrentIndex(value)

type IColor =
    interface
        abstract QtValue: Color.Deferred
    end
    
type ColorConstant =
    | Black
    | White
    | DarkGray
    | Gray
    | LightGray
    | Red
    | Green
    | Blue
    | Cyan
    | Magenta
    | Yellow
    | DarkRed
    | DarkGreen
    | DarkBlue
    | DarkCyan
    | DarkMagenta
    | DarkYellow
    | Transparent
with
    member internal this.QtValue =
        match this with
        | Black -> Color.Constant.Black
        | White -> Color.Constant.White
        | DarkGray -> Color.Constant.DarkGray
        | Gray -> Color.Constant.Gray
        | LightGray -> Color.Constant.LightGray
        | Red -> Color.Constant.Red
        | Green -> Color.Constant.Green
        | Blue -> Color.Constant.Blue
        | Cyan -> Color.Constant.Cyan
        | Magenta -> Color.Constant.Magenta
        | Yellow -> Color.Constant.Yellow
        | DarkRed -> Color.Constant.DarkRed
        | DarkGreen -> Color.Constant.DarkGreen
        | DarkBlue -> Color.Constant.DarkBlue
        | DarkCyan -> Color.Constant.DarkCyan
        | DarkMagenta -> Color.Constant.DarkMagenta
        | DarkYellow -> Color.Constant.DarkYellow
        | Transparent -> Color.Constant.Transparent
    
module Color =
    [<RequireQualifiedAccess>]
    type internal Value =
        | Constant of cc: ColorConstant
        | RGB of r: int * g: int * b: int
        | RGBA of r: int * g: int * b: int * a: int
        | FloatRGB of r: float * g: float * b: float
        | FloatRGBA of r: float * g: float * b: float * a: float
        | Hex of hex: string
    with
        member this.QtValue =
            match this with
            | Constant cc -> Color.Deferred.FromConstant(cc.QtValue) :> Org.Whatever.MinimalQtForFSharp.Color.Deferred
            | RGB(r, g, b) -> Color.Deferred.FromRGB(r, g, b)
            | RGBA(r, g, b, a) -> Color.Deferred.FromRGBA(r, g, b, a)
            | FloatRGB(r, g, b) -> Color.Deferred.FromFloatRGB(float32 r, float32 g, float32 b)
            | FloatRGBA(r, g, b, a) -> Color.Deferred.FromFloatRGBA(float32 r, float32 g, float32 b, float32 a)
            | Hex hex ->
                match Util.tryParseHexStringUInt32 hex (Some "#") with
                | Some value ->
                    let red = (value >>> 16) &&& 0xFFu
                    let green = (value >>> 8) &&& 0xFFu
                    let blue = value &&& 0xFFu
                    Color.Deferred.FromRGB(int red, int green, int blue)
                | None ->
                    printfn "MiscTypes.Value.QtValue: failed to parse hex string [%s]" hex
                    Color.Deferred.FromConstant(Black.QtValue)
                    
    type Unowned internal(handle: Org.Whatever.MinimalQtForFSharp.Color.Handle) =
        member val private Value = handle
        override this.Equals other =
            match other with
            | :? Unowned as other2 ->
                this.Value = other2.Value
            | _ ->
                false
        override this.GetHashCode() =
            this.Value.GetHashCode()
            
        interface IColor with
            member this.QtValue = Color.Deferred.FromHandle(handle)
            
        // any conventional getters/methods would go here
    
    let internal realize (value: Value) =
        Color.Create(value.QtValue)
        
    type Owned internal(handle: Org.Whatever.MinimalQtForFSharp.Color.Owned) =
        inherit Unowned(handle)
        let mutable disposed = false
        interface IDisposable with
            member this.Dispose() =
                if not disposed then
                    handle.Dispose()
                    disposed <- true
        override this.Finalize() =
            (this :> IDisposable).Dispose()
        new(cc: ColorConstant) =
            new Owned(Value.Constant cc |> realize)
        new(r: int, g: int, b: int) =
            new Owned(Value.RGB(r, g, b) |> realize)
        new(r: int, g: int, b: int, a: int) =
            new Owned(Value.RGBA(r, g, b, a) |> realize)
        new(r: float, g: float, b: float) =
            new Owned(Value.FloatRGB(r, g, b) |> realize)
        new(r: float, g: float, b: float, a: float) =
            new Owned(Value.FloatRGBA(r, g, b, a) |> realize)
        new(hex: string) =
            new Owned(Value.Hex(hex) |> realize)
    
type Color private(value: Color.Value, cached: bool) =
    static let mutable existing: Map<Color.Value, Org.Whatever.MinimalQtForFSharp.Color.Owned> = Map.empty
    member val private Value = value
    override this.Equals other =
        match other with
        | :? Color as other2 ->
            this.Value = other2.Value
        | _ ->
            false
    override this.GetHashCode() =
        value.GetHashCode()
        
    interface IColor with
        member this.QtValue =
            if cached then
                let handle =
                    match existing.TryFind value with
                    | Some handle ->
                        handle
                    | None ->
                        let handle =
                            Color.Create(value.QtValue)
                        existing <- existing.Add(value, handle)
                        handle
                Color.Deferred.FromHandle(handle)
            else
                // direct deferred value
                // might be useful if you intend on creating a gazillion colors that you'd rather not cache
                value.QtValue
    private new(cc: ColorConstant, ?cache: bool) =
        Color(Color.Value.Constant cc, defaultArg cache true)
    new(r: int, g: int, b: int, ?cache: bool) =
        Color(Color.Value.RGB(r, g, b), defaultArg cache true)
    new(r: int, g: int, b: int, a: int, ?cache: bool) =
        Color(Color.Value.RGBA(r, g, b, a), defaultArg cache true)
    new(r: float, g: float, b: float, ?cache: bool) =
        Color(Color.Value.FloatRGB(r, g, b), defaultArg cache true)
    new(r: float, g: float, b: float, a: float, ?cache: bool) =
        Color(Color.Value.FloatRGBA(r, g, b, a), defaultArg cache true)
    new(hex: string, ?cache: bool) =
        Color(Color.Value.Hex(hex), defaultArg cache true)
    static member Black = Color(Black)
    static member White = Color(White)
    static member DarkGray = Color(DarkGray)
    static member Gray = Color(Gray)
    static member LightGray = Color(LightGray)
    static member Red = Color(Red)
    static member Green = Color(Green)
    static member Blue = Color(Blue)
    static member Cyan = Color(Cyan)
    static member Magenta = Color(Magenta)
    static member Yellow = Color(Yellow)
    static member DarkRed = Color(DarkRed)
    static member DarkGreen = Color(DarkGreen)
    static member DarkBlue = Color(DarkBlue)
    static member DarkCyan = Color(DarkCyan)
    static member DarkMagenta = Color(DarkMagenta)
    static member DarkYellow = Color(DarkYellow)
    static member Transparent = Color(Transparent)
        
        
type VariantProxy private(qtVariant: Org.Whatever.MinimalQtForFSharp.Variant.Handle, owned: bool) =
    let mutable disposed = false
    member val internal Handle = qtVariant
    internal new(ownedVariant: Org.Whatever.MinimalQtForFSharp.Variant.Owned) =
        new VariantProxy(ownedVariant, true)
    internal new(unowned: Org.Whatever.MinimalQtForFSharp.Variant.Handle) =
        new VariantProxy(unowned, false)

    interface IDisposable with
        member this.Dispose() =
            if owned && not disposed then
                (qtVariant :?> Org.Whatever.MinimalQtForFSharp.Variant.Owned).Dispose()
                disposed <- true

    override this.Finalize() =
        (this :> IDisposable).Dispose()
    member this.ToBool() = qtVariant.ToBool()
    member this.ToInt() = qtVariant.ToInt()
    member this.ToSize() = qtVariant.ToSize()
    member this.ToStringValue() = qtVariant.ToString2()
    member this.ToCheckState() =
        match qtVariant.ToCheckState() with
        | Enums.CheckState.Unchecked -> Unchecked
        | Enums.CheckState.PartiallyChecked -> PartiallyChecked
        | Enums.CheckState.Checked -> Checked
        | _ -> failwith "VariantProxy.ToCheckState() - unknown incoming check state"
    member this.ToColor() =
        new Color.Owned(qtVariant.ToColor())
        
[<RequireQualifiedAccess>]
type private VariantValue =
    | Empty
    | Bool of value: bool
    | String of str: string
    | Int of value: int
    | Size of size: Size
    | CheckState of state: CheckState
    // | Icon of icon: Icon
    | Color of color: IColor
    | Alignment of align: Alignment
    // | Unknown
    
type Variant private(value: VariantValue) =
    member val QtValue =
        // the reason we don't just use a MinimalQtForFSharp.Variant.Deferred in the primary constructor (vs. VariantValue DU),
        // is to minimize native code execution until the last second
        // because for example evaluating a Color.Handle() can trigger something on the C++ side
        // "resource values" (like this, and Color) are meant to be safe to use in the view() function, with no C++ activity until absolutely necessary
        match value with
        | VariantValue.Empty ->
            Variant.Deferred.Empty() :> Org.Whatever.MinimalQtForFSharp.Variant.Deferred
        | VariantValue.Bool value ->
            Variant.Deferred.FromBool(value)
        | VariantValue.String str ->
            Variant.Deferred.FromString(str)
        | VariantValue.Int value ->
            Variant.Deferred.FromInt(value)
        | VariantValue.Size size ->
            Variant.Deferred.FromSize(size.QtValue)
        | VariantValue.CheckState state ->
            Variant.Deferred.FromCheckState(state.QtValue)
        | VariantValue.Color color ->
            Variant.Deferred.FromColor(color.QtValue)
        | VariantValue.Alignment align ->
            Variant.Deferred.FromAligment(align.QtValue)
        // | VariantValue.Unknown ->
        //     failwith "Variant.QtValue: 'Unknown' variants cannot be sent to C++ side"
    new() =
        Variant(VariantValue.Empty)
    new(value: bool) =
        Variant(VariantValue.Bool(value))
    new(value: string) =
        Variant(VariantValue.String(value))
    new(value: int) =
        Variant(VariantValue.Int(value))
    new(value: Size) =
        Variant(VariantValue.Size(value))
    new(value: CheckState) =
        Variant(VariantValue.CheckState(value))
    new(value: IColor) =
        Variant(VariantValue.Color(value))
    new(value: Alignment) =
        Variant(VariantValue.Alignment(value))
        
type ModelIndex private(handle: Org.Whatever.MinimalQtForFSharp.ModelIndex.Handle, owned: bool) =
    let mutable disposed = false
    member val internal Handle = handle
    
    interface IDisposable with
        member this.Dispose() =
            if owned && not disposed then
                (handle :?> Org.Whatever.MinimalQtForFSharp.ModelIndex.Owned).Dispose()
                disposed <- true
    override this.Finalize() =
        (this :> IDisposable).Dispose()
        
    internal new(handle: Org.Whatever.MinimalQtForFSharp.ModelIndex.Handle) =
        new ModelIndex(handle, false)
        
    internal new(handle: Org.Whatever.MinimalQtForFSharp.ModelIndex.Owned) =
        // I'm assuming the more specific ctor is matched, in the case of an 'Owned' handle
        // (vs. Handle which it inherits from)
        new ModelIndex(handle, true)
        
    new() =
        let handle = Org.Whatever.MinimalQtForFSharp.ModelIndex.Create()
        new ModelIndex(handle, true)
        
    member this.IsValid =
        handle.IsValid()
        
    member this.Row =
        handle.Row()
        
    member this.Column =
        handle.Column()
        
    member this.Data() =
        new VariantProxy(handle.Data())
        
    member this.Data(role: ItemDataRole) =
        new VariantProxy(handle.Data(role.QtValue))
    
// persistent model index
        
type PersistentModelIndexProxy internal(index: PersistentModelIndex.Handle) =
    member val internal Index = index

// see notes on ModelIndex* stuff above, similar will apply here
// maybe we need a "stack types" module or something?
type SizePolicyDeferred private(deferred: Org.Whatever.MinimalQtForFSharp.SizePolicy.Deferred) =
    member val internal QtValue = deferred
    
type AbstractItemModelProxy internal(model: AbstractItemModel.Handle) =
    member this.Index(row: int, column: int) =
        let value = model.Index(row, column)
        new ModelIndex(value)
        
    member this.Index(row: int, column: int, parent: ModelIndex) =
        let value = model.Index(row, column, parent.Handle)
        new ModelIndex(value)
    
    member this.Data(index: ModelIndex) =
        let value = model.Data(index.Handle)
        new VariantProxy(value)
        
    member this.Data(index: ModelIndex, role: ItemDataRole) =
        let value = model.Data(index.Handle, role.QtValue)
        new VariantProxy(value)
        
    member this.SetData(index: ModelIndex, value: Variant) =
        model.SetData(index.Handle, value.QtValue)
    
// other =========================

let internal qtDateFromDateOnly (date: DateOnly) =
    Date.Deferred.FromYearMonthDay(date.Year, date.Month, date.Day)
    
let internal dateOnlyFromQtDate (qtDate: Org.Whatever.MinimalQtForFSharp.Date.Handle) =
    let x = qtDate.ToYearMonthDay()
    DateOnly(x.Year, x.Month, x.Day)

type Icon private(handle: Org.Whatever.MinimalQtForFSharp.Icon.Handle, owned: bool) =
    let mutable disposed = false
    member val internal Handle = handle
    
    interface IDisposable with
        member this.Dispose() =
            if owned && not disposed then
                (handle :?> Org.Whatever.MinimalQtForFSharp.Icon.Owned).Dispose()
                disposed <- true
    override this.Finalize() =
        (this :> IDisposable).Dispose()
        
    internal new(handle: Org.Whatever.MinimalQtForFSharp.Icon.Handle) =
        new Icon(handle, false)
        
    internal new(owned: Org.Whatever.MinimalQtForFSharp.Icon.Owned) =
        new Icon(owned, true)
        
    new() =
        let handle = Icon.Create()
        new Icon(handle)
        
    new(filename: string) =
        let handle = Icon.Create(filename)
        new Icon(handle)
        
    new(themeIcon: ThemeIcon) =
        let handle = Icon.Create(toQtThemeIcon themeIcon)
        new Icon(handle)
        
    // TODO: pixmap ctor, but our whole module ordering is a big problem right now
    // Pixmap lives in Painting.fs which is below this and depends on this file :(
        
type KeySequence private(deferred: Org.Whatever.MinimalQtForFSharp.KeySequence.Deferred) =
    member val internal QtValue = deferred
    new(str: string) =
        let deferred =
            KeySequence.Deferred.FromString(str)
        KeySequence(deferred)
    new(stdKey: StandardKey) =
        let deferred =
            KeySequence.Deferred.FromStandard(toQtStandardKey stdKey)
        KeySequence(deferred)
    new(key: Key, ?modifiers: Modifier seq) =
        let deferred =
            let mods =
                defaultArg modifiers []
                |> Set.ofSeq
            KeySequence.Deferred.FromKey(toQtKey key, Modifier.QtSetFrom mods)
        KeySequence(deferred)
        
type RegexOption =
    | CaseInsensitive
    | DotMatchesEverything
    | Multiline
    | ExtendedPatternSyntax
    | InvertedGreediness
    | DontCapture
    | UseUnicodeProperties
with
    static member QtSetFrom (options: RegexOption seq) =
        (enum<RegularExpression.PatternOptions> 0, options)
        ||> Seq.fold (fun acc option ->
            let flag =
                match option with
                | CaseInsensitive -> RegularExpression.PatternOptions.CaseInsensitiveOption
                | DotMatchesEverything -> RegularExpression.PatternOptions.DotMatchesEverythingOption
                | Multiline -> RegularExpression.PatternOptions.MultilineOption
                | ExtendedPatternSyntax -> RegularExpression.PatternOptions.ExtendedPatternSyntaxOption
                | InvertedGreediness -> RegularExpression.PatternOptions.InvertedGreedinessOption
                | DontCapture -> RegularExpression.PatternOptions.DontCaptureOption
                | UseUnicodeProperties -> RegularExpression.PatternOptions.UseUnicodePropertiesOption
            acc ||| flag)
    
type Regex private(deferred: RegularExpression.Deferred) =
    member val internal QtValue = deferred
    new() =
        Regex(RegularExpression.Deferred.Empty())
    new(pattern: string, ?options: RegexOption seq)=
        let deferred =
            let options' =
                defaultArg options Set.empty
                |> RegexOption.QtSetFrom
            RegularExpression.Deferred.Regex(pattern, options')
        Regex(deferred)

type ImageConversionFlags =
    | AutoColor
    | ColorOnly
    | MonoOnly
    | ThresholdAlphaDither
    | OrderedAlphaDither
    | DiffuseAlphaDither
    | NoAlpha
    | DiffuseDither
    | OrderedDither
    | ThresholdDither
    | AutoDither
    | PreferDither
    | AvoidDither
    | NoOpaqueDetection
    | NoFormatConversion
with
    static member internal SetFrom (qtFlagSet: Enums.ImageConversionFlags) =
        let pairs = [
            Enums.ImageConversionFlags.AutoColor, AutoColor
            Enums.ImageConversionFlags.ColorOnly, ColorOnly
            Enums.ImageConversionFlags.MonoOnly, MonoOnly
            Enums.ImageConversionFlags.ThresholdAlphaDither, ThresholdAlphaDither
            Enums.ImageConversionFlags.OrderedAlphaDither, OrderedAlphaDither
            Enums.ImageConversionFlags.DiffuseAlphaDither, DiffuseAlphaDither
            Enums.ImageConversionFlags.NoAlpha, NoAlpha
            Enums.ImageConversionFlags.DiffuseDither, DiffuseDither
            Enums.ImageConversionFlags.OrderedDither, OrderedDither
            Enums.ImageConversionFlags.ThresholdDither, ThresholdDither
            Enums.ImageConversionFlags.AutoDither, AutoDither
            Enums.ImageConversionFlags.PreferDither, PreferDither
            Enums.ImageConversionFlags.AvoidDither, AvoidDither
            Enums.ImageConversionFlags.NoOpaqueDetection, NoOpaqueDetection
            Enums.ImageConversionFlags.NoFormatConversion, NoFormatConversion
        ]
        (Set.empty<ImageConversionFlags>, pairs)
        ||> List.fold (fun acc (flag, icFlag) ->
            if qtFlagSet.HasFlag flag then
                acc.Add(icFlag)
            else
                acc)
    static member internal QtSetFrom (icFlags: ImageConversionFlags seq) =
        (enum<Enums.ImageConversionFlags> 0, icFlags)
        ||> Seq.fold (fun acc icFlag ->
            let flag =
                match icFlag with
                | AutoColor -> Enums.ImageConversionFlags.AutoColor
                | ColorOnly -> Enums.ImageConversionFlags.ColorOnly
                | MonoOnly -> Enums.ImageConversionFlags.MonoOnly
                | ThresholdAlphaDither -> Enums.ImageConversionFlags.ThresholdAlphaDither
                | OrderedAlphaDither -> Enums.ImageConversionFlags.OrderedAlphaDither
                | DiffuseAlphaDither -> Enums.ImageConversionFlags.DiffuseAlphaDither
                | NoAlpha -> Enums.ImageConversionFlags.NoAlpha
                | DiffuseDither -> Enums.ImageConversionFlags.DiffuseDither
                | OrderedDither -> Enums.ImageConversionFlags.OrderedDither
                | ThresholdDither -> Enums.ImageConversionFlags.ThresholdDither
                | AutoDither -> Enums.ImageConversionFlags.AutoDither
                | PreferDither -> Enums.ImageConversionFlags.PreferDither
                | AvoidDither -> Enums.ImageConversionFlags.AvoidDither
                | NoOpaqueDetection -> Enums.ImageConversionFlags.NoOpaqueDetection
                | NoFormatConversion -> Enums.ImageConversionFlags.NoFormatConversion
            acc ||| flag)

type AspectRatioMode =
    | IgnoreAspectRatio
    | KeepAspectRatio
    | KeepAspectRatioByExpanding
with
    member this.QtValue =
        match this with
        | IgnoreAspectRatio -> Enums.AspectRatioMode.IgnoreAspectRatio
        | KeepAspectRatio -> Enums.AspectRatioMode.KeepAspectRatio
        | KeepAspectRatioByExpanding -> Enums.AspectRatioMode.KeepAspectRatioByExpanding
        
type TransformationMode =
    | FastTransformation
    | SmoothTransformation
with
    member this.QtValue =
        match this with
        | FastTransformation -> Enums.TransformationMode.FastTransformation
        | SmoothTransformation -> Enums.TransformationMode.SmoothTransformation
