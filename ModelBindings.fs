module FSharpQt.ModelBindings

open MiscTypes
open Org.Whatever.MinimalQtForFSharp

[<AbstractClass>]    
type ModelBindingBase<'handle> internal() =
    member val internal Handle = Unchecked.defaultof<'handle> with get, set
    // member internal this.Handle
    //     with set value =
    //         match maybeProxy with
    //         | Some _ ->
    //             printfn "warning: ModelBindingsBase re-assigning handle. why?"
    //         | None ->
    //             ()
    //         maybeProxy <- Some (this.MakeProxy value)
    // member this.UpdateContext =
    //     // always available, because tree built before first update
    //     maybeProxy.Value
    // // member this.ViewContextDelayed =
    // //     // available only if tree has been built once!
    // //     // 'Delayed' because it will always lag 1 iteration behind whatever the current attributes are!!!
    // //     // if you need it to be current/synchronized, update your state during the update() with whatever proxy method you're interested in ...
    // //     // we do need a better mechanism to track signal-less Qt properties with synthetic signals
    // //     // I would almost go as far as saying this should never be used, because it will lead to very confusing behavior
    // //     maybeProxy
    
// should these be declared in the modules, as <module>.ModelBinding ?

type AbstractProxyModelBinding() =
    inherit ModelBindingBase<AbstractProxyModel.Handle>()
    // when would this be used?
    // internal new(handle: AbstractProxyModel.Handle) =
    //     base.Handle <- handle
    //     AbstractProxyModelBinding()
    member this.MapToSource (proxyIndex: ModelIndexProxy) =
        let ret = this.Handle.MapToSource(ModelIndex.Deferred.FromHandle(proxyIndex.Index))
        new ModelIndexOwned(ret)
