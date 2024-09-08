module FSharpQt.Attrs

open FSharpQt.BuilderNode

type IAttrTarget =
    interface
    end
    
type IAttr =
    interface
        abstract member Key: string
        abstract member AttrEquals: IAttr -> bool
        abstract member ApplyTo: IAttrTarget * IAttr option -> unit    // 2nd argument: previous value, if any
    end
    
type AttrDiffResult =
    | Created of attr: IAttr
    | Changed of prev: IAttr * next: IAttr
    | Deleted of prev: IAttr
    
let diffAttrs (left: IAttr list) (right: IAttr list) =
    let leftList = left |> List.map (fun attr -> attr.Key, attr)
    let rightList = right |> List.map (fun attr -> attr.Key, attr)
    let leftMap = leftList |> Map.ofList
    let rightMap = rightList |> Map.ofList

    let allKeys =
        (leftList @ rightList)
        |> List.map fst
        |> List.distinct
        // |> List.sort        // I think we used to do this for numeric key ordering? but we don't use those anymore

    allKeys
    |> List.choose (fun key ->
        match leftMap.TryFind key, rightMap.TryFind key with
        | Some left, Some right ->
            if left.AttrEquals right then
                None
            else
                Changed (left, right) |> Some
        | Some left, None ->
            Deleted left |> Some
        | None, Some right ->
            Created right |> Some
        | _ ->
            failwith "can't happen")

let createdOrChanged (changes: AttrDiffResult list) =
    changes
    |> List.choose (function
        | Created attr ->
            Some (None, attr)
        | Changed (prev, attr) ->
            Some (Some prev, attr)
        | _ -> None)
    
// ======= move these to another module? ===========================================

// this interface doesn't really do anything, just tags our objects as relevant to this purpose
// nicer than just 'Object'
type internal ISignalMapFunc =
    interface
    end
    
[<AbstractClass>]
type internal SignalMapFuncBase<'signal,'msg>(func: 'signal -> 'msg option) =
    member val Func = func
    interface ISignalMapFunc // empty

// for cases where we have no signals (eg PushButton)
type internal NullSignalMapFunc() =
    interface ISignalMapFunc // empty
        
type PropsRoot<'msg>() =
    let mutable _maybeBoundName: string option = None
    
    // internal attribute-from-properties storage that will be shared by subclasses (eg [Root] -> Widget -> AbstractButton -> PushButton)
    // needs to be reversed before use to maintain the order that was originally assigned
    // we do it this way (all subclasses sharing this single list) precisely to preserve the consumer-supplied order
    let mutable _attrs: IAttr list = []
    
    member this.Attrs = _attrs |> List.rev
    member internal this.PushAttr(attr: IAttr) =
        _attrs <- attr :: _attrs
        
    member val internal _signalMask = 0L with get, set
    member internal this.AddSignal(flag: int64) =
        this._signalMask <- this._signalMask ||| flag
        
    member internal this.SignalMapList: ISignalMapFunc list =
        // not really necessary, but keeps this "regular" in case any root classes (eg QObject)
        //   still call base.SignalMapList
        []
    
    member internal this.MaybeBoundName = _maybeBoundName
    member this.Name with set value =
        _maybeBoundName <- Some value
        
    member val Attachments: Attachment<'msg> list = [] with get, set
        
        
[<AbstractClass>]
type ModelCoreRoot() =
    interface IAttrTarget
    member this.ApplyAttrs(attrs: (IAttr option * IAttr) list) =
        for maybePrev, attr in attrs do
            attr.ApplyTo(this, maybePrev)
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        // nothing to do, just maintaining regularity
        // see note on .SignalMapList above
        ()
