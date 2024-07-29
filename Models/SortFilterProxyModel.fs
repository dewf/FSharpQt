module FSharpQt.Models.SortFilterProxyModel

open FSharpQt.BuilderNode
open System
open Org.Whatever.MinimalQtForFSharp
open FSharpQt.MiscTypes

open FSharpQt.Attrs

type Signal =
    | AutoAcceptChildRowsChanged of autoAcceptChildRows: bool
    | FilterCaseSensitivityChanged of filterCaseSensitivity: CaseSensitivity
    | FilterRoleChanged of filterRole: ItemDataRole
    | RecursiveFilteringEnabledChanged of recursiveFilteringEnabled: bool
    | SortCaseSensitivityChanged of sortCaseSensitivity: CaseSensitivity
    | SortLocaleAwareChanged of sortLocaleAware: bool
    | SortRoleChanged of sortRole: ItemDataRole
    
type internal Attr =
    | AutoAcceptChildRows of state: bool
    | DynamicSortFilter of state: bool
    | FilterCaseSensitivity of sensitivity: CaseSensitivity
    | FilterKeyColumn of column: int option
    | FilterRegularExpression of regex: Regex
    | FilterRole of role: ItemDataRole
    | SortLocaleAware of state: bool
    | RecursiveFilteringEnabled of state: bool
    | SortCaseSensitivity of sensitivity: CaseSensitivity
    | SortRole of role: ItemDataRole
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
            | AutoAcceptChildRows _ -> "sortfilterproxymodel:autoacceptchildrows"
            | DynamicSortFilter _ -> "sortfilterproxymodel:dynamicsortfilter"
            | FilterCaseSensitivity _ -> "sortfilterproxymodel:filtercasesensitivity"
            | FilterKeyColumn _ -> "sortfilterproxymodel:filterkeycolumn"
            | FilterRegularExpression _ -> "sortfilterproxymodel:filterregularexpression"
            | FilterRole _ -> "sortfilterproxymodel:filterrole"
            | SortLocaleAware _ -> "sortfilterproxymodel:sortlocaleaware"
            | RecursiveFilteringEnabled _ -> "sortfilterproxymodel:recursivefilteringenabled"
            | SortCaseSensitivity _ -> "sortfilterproxymodel:sortcasesensitivity"
            | SortRole _ -> "sortfilterproxymodel:sortrole"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplySortFilterProxyModelAttr(this)
            | _ ->
                printfn "warning: SortFilterProxyModel.Attr couldn't ApplyTo() unknown target type [%A]" target
                
and internal AttrTarget =
    interface
        inherit AbstractProxyModel.AttrTarget
        abstract member ApplySortFilterProxyModelAttr: Attr -> unit
    end
  
type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)

type Props<'msg>() =
    inherit AbstractProxyModel.Props<'msg>()
    
    let mutable onAutoAcceptChildRowsChanged: (bool -> 'msg) option = None
    let mutable onFilterCaseSensitivityChanged: (CaseSensitivity -> 'msg) option = None
    let mutable onFilterRoleChanged: (ItemDataRole -> 'msg) option = None
    let mutable onRecursiveFilteringEnabledChanged: (bool -> 'msg) option = None
    let mutable onSortCaseSensitivityChanged: (CaseSensitivity -> 'msg) option = None
    let mutable onSortLocaleAwareChanged: (bool -> 'msg) option = None
    let mutable onSortRoleChanged: (ItemDataRole -> 'msg) option = None
    
    member internal this.SignalMask = enum<SortFilterProxyModel.SignalMask> (int this._signalMask)
    
    member this.AutoAcceptChildRowsChanged with set value =
        onAutoAcceptChildRowsChanged <- Some value
        this.AddSignal(int SortFilterProxyModel.SignalMask.AutoAcceptChildRowsChanged)

    member this.FilterCaseSensitivityChanged with set value =
        onFilterCaseSensitivityChanged <- Some value
        this.AddSignal(int SortFilterProxyModel.SignalMask.FilterCaseSensitivityChanged)

    member this.FilterRoleChanged with set value =
        onFilterRoleChanged <- Some value
        this.AddSignal(int SortFilterProxyModel.SignalMask.FilterRoleChanged)

    member this.RecursiveFilteringEnabledChanged with set value =
        onRecursiveFilteringEnabledChanged <- Some value
        this.AddSignal(int SortFilterProxyModel.SignalMask.RecursiveFilteringEnabledChanged)

    member this.SortCaseSensitivityChanged with set value =
        onSortCaseSensitivityChanged <- Some value
        this.AddSignal(int SortFilterProxyModel.SignalMask.SortCaseSensitivityChanged)

    member this.SortLocaleAwareChanged with set value =
        onSortLocaleAwareChanged <- Some value
        this.AddSignal(int SortFilterProxyModel.SignalMask.SortLocaleAwareChanged)

    member this.SortRoleChanged with set value =
        onSortRoleChanged <- Some value
        this.AddSignal(int SortFilterProxyModel.SignalMask.SortRoleChanged)
        
    member internal this.SignalMapList =
        let thisFunc = function
            | AutoAcceptChildRowsChanged autoAcceptChildRows ->
                onAutoAcceptChildRowsChanged
                |> Option.map (fun f -> f autoAcceptChildRows)
            | FilterCaseSensitivityChanged filterCaseSensitivity ->
                onFilterCaseSensitivityChanged
                |> Option.map (fun f -> f filterCaseSensitivity)
            | FilterRoleChanged filterRole ->
                onFilterRoleChanged
                |> Option.map (fun f -> f filterRole)
            | RecursiveFilteringEnabledChanged recursiveFilteringEnabled ->
                onRecursiveFilteringEnabledChanged
                |> Option.map (fun f -> f recursiveFilteringEnabled)
            | SortCaseSensitivityChanged sortCaseSensitivity ->
                onSortCaseSensitivityChanged
                |> Option.map (fun f -> f sortCaseSensitivity)
            | SortLocaleAwareChanged sortLocaleAware ->
                onSortLocaleAwareChanged
                |> Option.map (fun f -> f sortLocaleAware)
            | SortRoleChanged sortRole ->
                onSortRoleChanged
                |> Option.map (fun f -> f sortRole)
        // prepend to parent signal map funcs
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList
        
    member this.AutoAcceptChildRows with set value =
        this.PushAttr(AutoAcceptChildRows value)

    member this.DynamicSortFilter with set value =
        this.PushAttr(DynamicSortFilter value)

    member this.FilterCaseSensitivity with set value =
        this.PushAttr(FilterCaseSensitivity value)

    member this.FilterKeyColumn with set value =
        this.PushAttr(FilterKeyColumn value)

    member this.FilterRegularExpression with set value =
        this.PushAttr(FilterRegularExpression value)

    member this.FilterRole with set value =
        this.PushAttr(FilterRole value)

    member this.SortLocaleAware with set value =
        this.PushAttr(SortLocaleAware value)

    member this.RecursiveFilteringEnabled with set value =
        this.PushAttr(RecursiveFilteringEnabled value)

    member this.SortCaseSensitivity with set value =
        this.PushAttr(SortCaseSensitivity value)

    member this.SortRole with set value =
        this.PushAttr(SortRole value)
        
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit AbstractProxyModel.ModelCore<'msg>(dispatch)
    let mutable sfProxyModel: SortFilterProxyModel.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<SortFilterProxyModel.SignalMask> 0
    
    // binding guards
    let mutable lastAutoAcceptChildRows = false
    let mutable lastFilterCaseSensitivity = CaseSensitive
    let mutable lastFilterRole = DisplayRole
    let mutable lastRecursiveFilteringEnabled = false
    let mutable lastSortCaseSensitivity = CaseSensitive
    let mutable lastLocaleAware = false
    let mutable lastSortRole = DisplayRole
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.SortFilterProxyModel
        with get() = sfProxyModel
        and set value =
            // assign to base
            this.AbstractProxyModel <- value
            sfProxyModel <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "SortFilterProxyModel.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "SortFilterProxyModel.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            sfProxyModel.SetSignalMask(value)
            currentMask <- value
            
    interface AttrTarget with
        member this.ApplySortFilterProxyModelAttr attr =
            match attr with
            | AutoAcceptChildRows state ->
                if state <> lastAutoAcceptChildRows then
                    lastAutoAcceptChildRows <- state
                    sfProxyModel.SetAutoAcceptChildRows(state)
            | DynamicSortFilter state ->
                sfProxyModel.SetDynamicSortFilter(state)
            | FilterCaseSensitivity sensitivity ->
                if sensitivity <> lastFilterCaseSensitivity then
                    lastFilterCaseSensitivity <- sensitivity
                    sfProxyModel.SetFilterCaseSensitivity(sensitivity.QtValue)
            | FilterKeyColumn column ->
                sfProxyModel.SetFilterKeyColumn(column |> Option.defaultValue -1)
            | FilterRegularExpression regex ->
                sfProxyModel.SetFilterRegularExpression(regex.QtValue)
            | FilterRole role ->
                if role <> lastFilterRole then
                    lastFilterRole <- role
                    sfProxyModel.SetFilterRole(role.QtValue)
            | SortLocaleAware state ->
                if state <> lastLocaleAware then
                    lastLocaleAware <- state
                    sfProxyModel.SetSortLocaleAware(state)
            | RecursiveFilteringEnabled state ->
                if state <> lastRecursiveFilteringEnabled then
                    lastRecursiveFilteringEnabled <- state
                    sfProxyModel.SetRecursiveFilteringEnabled(state)
            | SortCaseSensitivity sensitivity ->
                if sensitivity <> lastSortCaseSensitivity then
                    lastSortCaseSensitivity <- sensitivity
                    sfProxyModel.SetSortCaseSensitivity(sensitivity.QtValue)
            | SortRole role ->
                if role <> lastSortRole then
                    lastSortRole <- role
                    sfProxyModel.SetSortRole(role.QtValue)
                    
    interface SortFilterProxyModel.SignalHandler with
        // Object =========================
        member this.Destroyed(obj: Object.Handle) =
            (this :> Object.SignalHandler).Destroyed(obj)
        member this.ObjectNameChanged(name: string) =
            (this :> Object.SignalHandler).ObjectNameChanged(name)
        // AbstractItemModel ==============
        member this.ColumnsAboutToBeInserted (parent, first, last) =
            (this :> AbstractItemModel.SignalHandler).ColumnsAboutToBeInserted(parent, first, last)
        member this.ColumnsAboutToBeMoved (srcParent, srcStart, srcEnd, destParent, destColumn) =
            (this :> AbstractItemModel.SignalHandler).ColumnsAboutToBeMoved(srcParent, srcStart, srcEnd, destParent, destColumn)
        member this.ColumnsAboutToBeRemoved (parent, first, last) =
            (this :> AbstractItemModel.SignalHandler).ColumnsAboutToBeRemoved(parent, first, last)
        member this.ColumnsInserted (parent, first, last) =
            (this :> AbstractItemModel.SignalHandler).ColumnsInserted(parent, first, last)
        member this.ColumnsMoved (srcParent, srcStart, srcEnd, destParent, destColumn) =
            (this :> AbstractItemModel.SignalHandler).ColumnsMoved(srcParent, srcStart, srcEnd, destParent, destColumn)
        member this.ColumnsRemoved (parent, first, last) =
            (this :> AbstractItemModel.SignalHandler).ColumnsRemoved(parent, first, last)
        member this.DataChanged (topLeft, bottomRight, roles) =
            (this :> AbstractItemModel.SignalHandler).DataChanged(topLeft, bottomRight, roles)
        member this.HeaderDataChanged (orientation, first, last) =
            (this :> AbstractItemModel.SignalHandler).HeaderDataChanged(orientation, first, last)
        member this.LayoutAboutToBeChanged (parents, hint) =
            (this :> AbstractItemModel.SignalHandler).LayoutAboutToBeChanged(parents, hint)
        member this.LayoutChanged (parents, hint) =
            (this :> AbstractItemModel.SignalHandler).LayoutChanged(parents, hint)
        member this.ModelAboutToBeReset() =
            (this :> AbstractItemModel.SignalHandler).ModelAboutToBeReset()
        member this.ModelReset() =
            (this :> AbstractItemModel.SignalHandler).ModelReset()
        member this.RowsAboutToBeInserted (parent, start, ``end``) =
            (this :> AbstractItemModel.SignalHandler).RowsAboutToBeInserted(parent, start, ``end``)
        member this.RowsAboutToBeMoved (srcParent, srcStart, srcEnd, destParent, destRow) =
            (this :> AbstractItemModel.SignalHandler).RowsAboutToBeMoved(srcParent, srcStart, srcEnd, destParent, destRow)
        member this.RowsAboutToBeRemoved (parent, first, last) =
            (this :> AbstractItemModel.SignalHandler).RowsAboutToBeRemoved(parent, first, last)
        member this.RowsInserted (parent, first, last) =
            (this :> AbstractItemModel.SignalHandler).RowsInserted(parent, first, last)
        member this.RowsMoved (srcParent, srcStart, srcEnd, destParent, destRow) =
            (this :> AbstractItemModel.SignalHandler).RowsMoved(srcParent, srcStart, srcEnd, destParent, destRow)
        member this.RowsRemoved (parent, first, last) =
            (this :> AbstractItemModel.SignalHandler).RowsRemoved(parent, first, last)
        // AbstractProxyModel
        member this.SourceModelChanged() =
            (this :> AbstractProxyModel.SignalHandler).SourceModelChanged()
        // SortFilterProxyModel
        member this.AutoAcceptChildRowsChanged autoAccept =
            signalDispatch (AutoAcceptChildRowsChanged autoAccept)
        member this.FilterCaseSensitivityChanged sensitivity =
            signalDispatch (CaseSensitivity.From sensitivity |> FilterCaseSensitivityChanged)
        member this.FilterRoleChanged role =
            signalDispatch (ItemDataRole.From role |> FilterRoleChanged)
        member this.RecursiveFilteringEnabledChanged enabled =
            signalDispatch (RecursiveFilteringEnabledChanged enabled)
        member this.SortCaseSensitivityChanged sensitivity =
            signalDispatch (CaseSensitivity.From sensitivity |> SortCaseSensitivityChanged)
        member this.SortLocaleAwareChanged state =
            signalDispatch (SortLocaleAwareChanged state)
        member this.SortRoleChanged role =
            signalDispatch (ItemDataRole.From role |> SortRoleChanged)

    interface IDisposable with
        member this.Dispose() =
            sfProxyModel.Dispose()
            
type private Model<'msg>(dispatch: 'msg -> unit) as this =
    inherit ModelCore<'msg>(dispatch)
    let sfProxyModel = SortFilterProxyModel.Create(this)
    do
        this.SortFilterProxyModel <- sfProxyModel
    
    member this.AddSourceModel (model: AbstractItemModel.Handle) =
        sfProxyModel.SetSourceModel(model)
        
    member this.RemoveSourceModel () =
        // well if it gets deleted (as a dependency), won't that automagically remove it from us?
        ()

let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: SortFilterProxyModel.SignalMask) =
    let model = new Model<'msg>(dispatch)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: SortFilterProxyModel.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()
    
type SortFilterProxyModelBinding internal(handle: SortFilterProxyModel.Handle) =
    inherit AbstractProxyModel.AbstractProxyModelBinding(handle)
    
let bindNode (name: string) (map: Map<string, IViewBinding>) =
    match map.TryFind name with
    | Some (:? SortFilterProxyModelBinding as sortFilterProxyModel) ->
        sortFilterProxyModel
    | _ ->
        failwith "SortFilterProxyModel.bindNode fail"

type SortFilterProxyModel<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    let mutable maybeSourceModel: IModelNode<'msg> option = None
    member this.SourceModel with set value = maybeSourceModel <- Some value
    
    member this.MigrateDeps (changeMap: Map<DepsKey, DepsChange>) =
        match changeMap.TryFind (StrKey "source") with
        | Some change ->
            match change with
            | Unchanged ->
                ()
            | Added ->
                this.model.AddSourceModel(maybeSourceModel.Value.QtModel)
            | Removed ->
                this.model.RemoveSourceModel()
            | Swapped ->
                this.model.RemoveSourceModel()
                this.model.AddSourceModel(maybeSourceModel.Value.QtModel)
        | None ->
            // neither side had one
            ()
    
    interface IModelNode<'msg> with
        override this.Dependencies =
            maybeSourceModel
            |> Option.map (fun node -> StrKey "source", node :> IBuilderNode<'msg>)
            |> Option.toList

        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask
            
        override this.AttachDeps () =
            maybeSourceModel
            |> Option.iter (fun node ->
                this.model.AddSourceModel(node.QtModel))

        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> SortFilterProxyModel<'msg>)
            let nextAttrs = diffAttrs left'.Attrs this.Attrs |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask
            this.MigrateDeps(depsChanges |> Map.ofList)

        override this.Dispose() =
            (this.model :> IDisposable).Dispose()

        override this.QtModel =
            this.model.SortFilterProxyModel
            
        override this.ContentKey =
            this.model.SortFilterProxyModel
            
        override this.Attachments =
            this.Attachments
            
        override this.Binding =
            this.MaybeBoundName
            |> Option.map (fun name ->
                name, SortFilterProxyModelBinding(this.model.SortFilterProxyModel))

       
