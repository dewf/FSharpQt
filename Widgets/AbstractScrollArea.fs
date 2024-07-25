module FSharpQt.Widgets.AbstractScrollArea

open System
open FSharpQt.Attrs
open Org.Whatever.MinimalQtForFSharp

type private Signal = unit

type SizeAdjustPolicy =
    | AdjustIgnored
    | AdjustToContentsOnFirstShow
    | AdjustToContents
with
    member internal this.QtValue =
        match this with
        | AdjustIgnored -> AbstractScrollArea.SizeAdjustPolicy.AdjustIgnored
        | AdjustToContentsOnFirstShow -> AbstractScrollArea.SizeAdjustPolicy.AdjustToContentsOnFirstShow
        | AdjustToContents -> AbstractScrollArea.SizeAdjustPolicy.AdjustToContents

type ScrollBarPolicy =
    | ScrollBarAsNeeded
    | ScrollBarAlwaysOff
    | ScrollBarAlwaysOn
with
    member internal this.QtValue =
        match this with
        | ScrollBarAsNeeded -> AbstractScrollArea.ScrollBarPolicy.ScrollBarAsNeeded
        | ScrollBarAlwaysOff -> AbstractScrollArea.ScrollBarPolicy.ScrollBarAlwaysOff
        | ScrollBarAlwaysOn -> AbstractScrollArea.ScrollBarPolicy.ScrollBarAlwaysOn

type internal Attr =
    | HorizontalScrollBarPolicy of policy: ScrollBarPolicy
    | SizeAdjustPolicy of policy: SizeAdjustPolicy
    | VerticalScrollBarPolicy of policy: ScrollBarPolicy
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
            | HorizontalScrollBarPolicy _ -> "abstractscrollarea:horizontalscrollbarpolicy"
            | SizeAdjustPolicy _ -> "abstractscrollarea:sizeadjustpolicy"
            | VerticalScrollBarPolicy _ -> "abstractscrollarea:verticalscrollbarpolicy"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyAbstractScrollAreaAttr(this)
            | _ ->
                printfn "warning: Frame.Attr couldn't ApplyTo() unknown target type [%A]" target
    
and internal AttrTarget =
    interface
        inherit Frame.AttrTarget
        abstract member ApplyAbstractScrollAreaAttr: Attr -> unit
    end

type Props<'msg>() =
    inherit Frame.Props<'msg>()
    
    member internal this.SignalMask = enum<AbstractScrollArea.SignalMask> (int this._signalMask)
    
    member internal this.SignalMapList =
        NullSignalMapFunc() :> ISignalMapFunc :: base.SignalMapList
        
    member this.HorizontalScrollBarPolicy with set value =
        this.PushAttr(HorizontalScrollBarPolicy value)
        
    member this.SizeAdjustPolicy with set value =
        this.PushAttr(SizeAdjustPolicy value)
        
    member this.VerticalScrollBarPolicy with set value =
        this.PushAttr(VerticalScrollBarPolicy value)
    
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit Frame.ModelCore<'msg>(dispatch)
    let mutable abstractScrollArea: AbstractScrollArea.Handle = null

    member this.AbstractScrollArea
        with get() = abstractScrollArea
        and set value =
            this.Frame <- value
            abstractScrollArea <- value

    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? NullSignalMapFunc ->
                // nothing to assign
                ()
            | _ ->
                failwith "Frame.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "Frame.ModelCore: signal map assignment didn't have a head element"
            
    // abstract, no mask setter

    interface AttrTarget with
        member this.ApplyAbstractScrollAreaAttr attr =
            match attr with
            | HorizontalScrollBarPolicy policy ->
                abstractScrollArea.SetHorizontalScrollBarPolicy(policy.QtValue)
            | SizeAdjustPolicy policy ->
                abstractScrollArea.SetSizeAdjustPolicy(policy.QtValue)
            | VerticalScrollBarPolicy policy ->
                abstractScrollArea.SetVerticalScrollBarPolicy(policy.QtValue)
                
    interface AbstractScrollArea.SignalHandler with
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

    interface IDisposable with
        member this.Dispose() =
            abstractScrollArea.Dispose()
