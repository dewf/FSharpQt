module FSharpQt.Widgets.QObject

open System
open FSharpQt.BuilderNode
open Org.Whatever.MinimalQtForFSharp

open FSharpQt.MiscTypes
open FSharpQt.Attrs

type private Signal =
    | Destroyed of object: QObjectProxy
    | ObjectNameChanged of name: string

type internal Attr =
    | ObjectName of name: string
with
    interface IAttr with
        override this.AttrEquals other =
            match other with
            | :? Attr as attrOther ->
                this = attrOther
            | _ ->
                false
        override this.Key =
            match this with
            | ObjectName name -> "qobject:objectname"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyObjectAttr this
            | _ ->
                printfn "warning: QObject.Attr couldn't ApplyTo() unknown target type [%A]" target

and internal AttrTarget =
    interface
        inherit IAttrTarget
        abstract member ApplyObjectAttr: Attr -> unit
    end

type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)

type Props<'msg>() =
    inherit PropsRoot<'msg>()
    
    let mutable onDestroyed: (QObjectProxy -> 'msg) option = None
    let mutable onObjectNameChanged: (string -> 'msg) option = None
    
    member internal this.SignalMask = enum<Object.SignalMask> (int this._signalMask)
    
    member this.OnDestroyed with set value =
        onDestroyed <- Some value
        this.AddSignal(int Object.SignalMask.Destroyed)
    
    member this.OnObjectNameChanged with set value =
        onObjectNameChanged <- Some value
        this.AddSignal(int Object.SignalMask.ObjectNameChanged)

    member internal this.SignalMapList =
        let thisFunc = function
            | Destroyed obj ->
                onDestroyed
                |> Option.map (fun f -> f obj)
            | ObjectNameChanged name ->
                onObjectNameChanged
                |> Option.map (fun f -> f name)
        // technically base.SignalMapList should return [] in this instance,
        // but this is just for when we copy code so we don't have to think about it
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList

    member this.ObjectName with set value =
        this.PushAttr(ObjectName value)

type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit ModelCoreRoot()
    
    let mutable object: Object.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    // abstract (on the F# side anyway), so no need for a signal mask
    // let mutable currentMask = enum<Object.SignalMask> 0
    
    // binding guards
    let mutable lastObjectName = ""
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.Object
        with get() =
            object
        and set value =
            // no parent class to assign to
            object <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "QObject.ModelCore.SignalMaps: wrong func type"
            // assign remainder up heirarchy
            base.SignalMaps <- etc
        | _ ->
            failwith "QObject.ModelCore: signal map assignment didn't have a head element"
            
    // no SignalMask property, see note on currentMask at top
            
    interface AttrTarget with
        member this.ApplyObjectAttr attr =
            match attr with
            | ObjectName name ->
                if name <> lastObjectName then
                    lastObjectName <- name
                    object.SetObjectName(name)

    interface Object.SignalHandler with
        member this.Destroyed(obj: Object.Handle) =
            signalDispatch (QObjectProxy(obj) |> Destroyed)
        member this.ObjectNameChanged(name: string) =
            signalDispatch (ObjectNameChanged name)
            
    // interface IDisposable with
    //     member this.Dispose() =
    //         object.Dispose()

type QObjectBinding internal(handle: Object.Handle) =
    interface IViewBinding
