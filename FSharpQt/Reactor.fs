module FSharpQt.Reactor

open System
open FSharpQt.BuilderNode
open Org.Whatever.MinimalQtForFSharp

open FSharpQt.Attrs

[<RequireQualifiedAccess>]
type Cmd<'msg,'signal> =
    | None
    | Msg of 'msg
    | Signal of 'signal
    | ViewExec of func: (Map<string, IViewBinding> -> 'msg option)
    | Batch of commands: Cmd<'msg,'signal> list
    | Async of block: Async<'msg>
    | Sub of subFunc: (('msg -> unit) -> unit)
    
let asyncPerform (block: Async<'a>) (mapper: 'a -> 'msg) =
    async {
        let! result = block
        return mapper result
    }
    
type ViewExecBuilder(bindings: Map<string, IViewBinding>) =
    member this.Bind(m, f) =
        let thing = m bindings
        f thing
    member this.Return(m) =
        Some m
    member this.Zero() =
        None
    member this.Using(m, f) =
        use thing = m
        f thing
        
let viewexec = ViewExecBuilder
    
type ComponentStateTarget<'state> =
    interface
        inherit IAttrTarget
        abstract member State: 'state
        abstract member Update: 'state -> unit
    end
    
[<AbstractClass>]
type ComponentAttrBase<'T,'State when 'T: equality>(value: 'T) =
    member this.Value = value
    abstract member Key: string
    abstract member Update: 'State -> 'State
    interface IAttr with
        override this.AttrEquals other =
            match other with
            | :? ComponentAttrBase<'T,'State> as otherAttr ->
                value = otherAttr.Value
            | _ ->
                false
        override this.Key =
            this.Key
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? ComponentStateTarget<'State> as stateTarget ->
                stateTarget.State
                |> this.Update
                |> stateTarget.Update
            | _ ->
                failwith "ComponentAttrBase can only .ApplyTo ComponentStateTarget<'State>"

type ComponentAttr<'T,'State when 'T: equality>(value: 'T, keyFunc: 'T -> string, updateFunc: 'State -> 'T -> 'State) =
    member this.Value = value
    interface IAttr with
        override this.AttrEquals other =
            match other with
            | :? ComponentAttr<'T,'State> as otherAttr ->
                value = otherAttr.Value
            | _ ->
                false
        override this.Key =
            keyFunc value
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? ComponentStateTarget<'State> as stateTarget ->
                let nextState =
                    updateFunc stateTarget.State value
                stateTarget.Update(nextState)
            | _ ->
                failwith "ComponentAttr can only .ApplyTo ComponentStateTarget<'State>"
                
type private StateMutator<'state>(init: 'state) =
    let mutable current = init
    interface ComponentStateTarget<'state> with
        member this.State = current
        member this.Update next =
            current <- next
    
type Reactor<'state, 'msg, 'signal, 'root when 'root :> IBuilderNode<'msg>>(
                    init: unit -> 'state * Cmd<'msg,'signal>,
                    update: 'state -> 'msg -> 'state * Cmd<'msg,'signal>,
                    view: 'state -> 'root,
                    processSignal: 'signal -> unit,
                    buildContext: BuilderContext<'msg>) =
    let initState, initCmd = init()
    let mutable state = initState
    let mutable root = view state
    let mutable disableDispatch = false
    let mutable bindings = Map.empty<string, IViewBinding>
    let mutable disposed = false
    
    let updateBindings() =
        let rec recInner (soFar: Map<string, IViewBinding>) (node: IBuilderNode<'msg>) =
            let deps =
                node.Dependencies
                |> List.map snd
            let attachments =
                node.Attachments
                |> List.map (_.Node)
            let soFar =
                (soFar, attachments @ deps)
                ||> List.fold recInner
            match node.Binding with
            | Some (name, thing) ->
                // should we log or even crash when a duplicate exists?
                soFar.Add(name, thing)
            | None ->
                soFar
        bindings <- recInner Map.empty root
    
    let rec dispatch (msg: 'msg) =
        if disableDispatch then
            // currently diffing, something tried to dispatch due to an attribute change (generally a Qt widget emitting a signal due to a method call / property change)
            // basically this acts as a global callback disabler, preventing them while we're handling one already
            ()
        else
            let prevRoot = root
            let nextState, cmd =
                update state msg
            state <- nextState
            root <- view state
            // prevent diff-triggered dispatching with a guard:
            disableDispatch <- true
            let events = DiffEventsList()
            diff dispatch (Some (prevRoot :> IBuilderNode<'msg>)) (Some (root :> IBuilderNode<'msg>)) buildContext events
            disableDispatch <- false
            //
            updateBindings()
            // process command(s) after tree diff
            processCmd cmd
            // process diff events after dispatch working again (at the moment, just showing visible top level windows)
            // not sure if it matters precisely where this is
            events.ProcessEvents()
    and
        processCmd (cmd: Cmd<'msg,'signal>) =
            match cmd with
            | Cmd.None ->
                ()
            | Cmd.Msg msg ->
                // should this be deferred via .ExecuteOnMainThread? would the recursion be a problem for any reason?
                dispatch msg
            | Cmd.Signal signal ->
                processSignal signal
            | Cmd.ViewExec func ->
                match func bindings with
                | Some msg ->
                    dispatch msg
                | None ->
                    ()
            | Cmd.Batch commands ->
                commands
                |> List.iter processCmd
            | Cmd.Async block ->
                async {
                    let! msg = block
                    Application.ExecuteOnMainThread(fun _ -> dispatch msg)
                } |> Async.Start
            | Cmd.Sub subFunc ->
                let safeDispatch msg =
                    // don't do anything except on the UI thread!
                    let inner _ =
                        if not disposed then
                            dispatch msg
                        else
                            printfn "Cmd.Sub - attempted to dispatch [%A] on a disposed reactor" msg
                    Application.ExecuteOnMainThread(inner)
                subFunc safeDispatch
    do
        // prevent diff-triggered dispatching with a guard (some widgets obnoxiously emit signals when programmatically setting properties)
        disableDispatch <- true
        let events = DiffEventsList()
        build dispatch root buildContext events
        disableDispatch <- false
        //
        updateBindings()
        processCmd initCmd
        // process diff events after dispatch working again (at the moment, just showing visible top level windows)
        // not sure if it matters precisely where this is
        events.ProcessEvents()
        
    member this.Root =
        root
        
    member this.ApplyAttrs (attrs: (IAttr option * IAttr) list) =
        let prevRoot = root
        let mutator =
            StateMutator(state)
        for maybePrev, attr in attrs do
            // the attr list has already been filtered to created-or-changed, so we don't have to worry about checking prev vs. next equivalence
            attr.ApplyTo(mutator, maybePrev)
        state <- (mutator :> ComponentStateTarget<'state>).State
        root <- view state
        // prevent dispatching while diffing
        disableDispatch <- true
        let events = DiffEventsList()
        diff dispatch (Some prevRoot) (Some root) buildContext events
        disableDispatch <- false
        //
        updateBindings()
        // no commands allowed in attr update (for now)
        // ...
        // and what about DiffEvents, should those be processed here or not?
        if not events.Events.IsEmpty then
            printfn "!! warning: Reactor.ApplyAttrs - we had some unprocess DiffEvents. not sure how/if to handle these yet"
    
    interface IDisposable with
        member this.Dispose() =
            // set ASAP to stop subscription dispatches after disposal:
            disposed <- true
            // outside code has no concept of our inner tree, so we're responsible for disposing all of it
            disposeTree root
            // dispose the state, if it's capable
            Util.tryDispose state

[<AbstractClass>]    
type ReactorNodeBase<'outerMsg,'state,'msg,'signal,'root when 'root :> IBuilderNode<'msg>>(
                init: unit -> 'state * Cmd<'msg, 'signal>,
                update: 'state -> 'msg -> 'state * Cmd<'msg, 'signal>,
                view: 'state -> 'root
                ) =
    [<DefaultValue>] val mutable reactor: Reactor<'state,'msg,'signal,'root>
    
    let mutable _attrs: IAttr list = []
    member private this.Attrs = _attrs |> List.rev
    member this.PushAttr(attr: IAttr) =
        _attrs <- attr :: _attrs
    
    abstract member SignalMap: 'signal -> 'outerMsg option
    default this.SignalMap _ = None
    
    interface IBuilderNode<'outerMsg> with
        override this.Dependencies = []
        override this.Create dispatch buildContext =
            let processSignal signal =
                match this.SignalMap signal with
                | Some outerMsg ->
                    dispatch outerMsg
                | None ->
                    ()
            let buildContext =
                // we need to rebind this, because 'outerMsg and 'msg are different
                { ContainingWindow = buildContext.ContainingWindow }
            this.reactor <- new Reactor<'state,'msg,'signal,'root>(init, update, view, processSignal, buildContext)
            let initAttrs =
                this.Attrs
                |> List.map (fun attr -> (None, attr)) // 'None' for no previous values
            this.reactor.ApplyAttrs(initAttrs)
        override this.AttachDeps () =
            ()
        override this.MigrateFrom (left: IBuilderNode<'outerMsg>) (_: (DepsKey * DepsChange) list) =
            let left' = (left :?> ReactorNodeBase<'outerMsg,'state,'msg,'signal,'root>)
            let nextAttrs = diffAttrs left'.Attrs this.Attrs |> createdOrChanged
            this.reactor <- left'.reactor
            this.reactor.ApplyAttrs(nextAttrs)
        override this.Dispose() =
            (this.reactor :> IDisposable).Dispose()
        override this.ContentKey =
            this.reactor.Root.ContentKey
        override this.Attachments = []
        override this.Binding = None
            
    
[<AbstractClass>]
type WidgetReactorNode<'outerMsg,'state,'msg,'signal>(
                init: unit -> 'state * Cmd<'msg, 'signal>,
                update: 'state -> 'msg -> 'state * Cmd<'msg, 'signal>,
                view: 'state -> IWidgetNode<'msg>
                ) =
    inherit ReactorNodeBase<'outerMsg,'state,'msg,'signal,IWidgetNode<'msg>>(init, update, view)
    interface IWidgetNode<'outerMsg> with
        override this.Widget =
            this.reactor.Root.Widget

[<AbstractClass>]
type LayoutReactorNode<'outerMsg,'state,'msg,'signal>(
                init: unit -> 'state * Cmd<'msg, 'signal>,
                update: 'state -> 'msg -> 'state * Cmd<'msg, 'signal>,
                view: 'state -> ILayoutNode<'msg>
                ) =
    inherit ReactorNodeBase<'outerMsg,'state,'msg,'signal,ILayoutNode<'msg>>(init, update, view)
    interface ILayoutNode<'outerMsg> with
        override this.Layout =
            this.reactor.Root.Layout
        
[<AbstractClass>]
type WindowReactorNode<'outerMsg,'state,'msg,'signal>(
                init: unit -> 'state * Cmd<'msg, 'signal>,
                update: 'state -> 'msg -> 'state * Cmd<'msg, 'signal>,
                view: 'state -> IWindowNode<'msg>
                ) =
    inherit ReactorNodeBase<'outerMsg,'state,'msg,'signal,IWindowNode<'msg>>(init, update, view)
    interface IWindowNode<'outerMsg> with
        override this.WindowWidget =
            this.reactor.Root.WindowWidget
        override this.ShowIfVisible () =
            this.reactor.Root.ShowIfVisible()

// root-level AppReactor stuff ============================================================

type AppSignal =
    | QuitApplication
    
type AppStyle =
    | Win11
    | Vista
    | Windows
    | Fusion
    
type AppReactor<'msg,'state>(init: unit -> 'state * Cmd<'msg,AppSignal>, update: 'state -> 'msg -> 'state * Cmd<'msg,AppSignal>, view: 'state -> IBuilderNode<'msg>) =
    let mutable appStyle: AppStyle option = None
    let mutable quitOnLastWindowClosed: bool option = None
    static let mutable libraryInitialized = false
    do
        if not libraryInitialized then
            Library.Init()
            libraryInitialized <- true
    member this.SetQuitOnLastWindowClosed (state: bool) =
        quitOnLastWindowClosed <- Some state
        
    member this.SetStyle (style: AppStyle) =
        appStyle <- Some style
        
    member this.Run(argv: string array) =
        use app =
            Application.Create(argv)

        match quitOnLastWindowClosed with
        | Some value ->
            app.SetQuitOnLastWindowClosed(value)
        | None ->
            ()
            
        match appStyle with
        | Some value ->
            let str =
                match value with
                | Win11 -> "windows11"
                | Vista -> "windowsvista"
                | Windows -> "Windows"
                | Fusion -> "Fusion"
            Application.SetStyle(str)
        | None ->
            ()
            
        let processSignal signal =
            match signal with
            | QuitApplication ->
                Application.Quit()
                
        let context =
            { ContainingWindow = None }

        use reactor =
            new Reactor<'state,'msg,AppSignal,IBuilderNode<'msg>>(init, update, view, processSignal, context)
        Application.Exec()
        
    interface IDisposable with
        member this.Dispose() =
            Library.DumpTables()
            Library.Shutdown()
            
let createApplication (init: unit -> 'state * Cmd<'msg,AppSignal>) (update: 'state -> 'msg -> 'state * Cmd<'msg,AppSignal>) (view: 'state -> IBuilderNode<'msg>) =
    new AppReactor<'msg,'state>(init, update, view)


// attribute stuff for components =============================================================
// generic list-based attr diffing
// we've since moved to IAttr-based attrs instead,
// but this exists to make it easy to bootstrap a custom component without writing so much attribute code
// just use a single EasyAttrs property on your custom node class, eg:
//
// member this.Attrs with set value =
//     this.PushAttr(EasyAttrs(value, "fakethingattrs", attrKey, attrUpdate))

// (this.Attrs overwrites/shadows the internal one from PropsRoot(), but the compiler isn't complaining so it's probably OK)

type private EasyAttrChange<'a> =
    | Created of 'a
    | Deleted of 'a
    | Changed of 'a * 'a
    
let inline private easyDiffAttrs (keyFunc: 'a -> int) (a1: 'a list) (a2: 'a list)  =
    let leftList = a1 |> List.map (fun a -> keyFunc a, a)
    let rightList = a2 |> List.map (fun a -> keyFunc a, a)
    let leftMap = leftList |> Map.ofList
    let rightMap = rightList |> Map.ofList

    let allKeys =
        (leftList @ rightList)
        |> List.map fst
        |> List.distinct
        |> List.sort

    allKeys
    |> List.choose (fun key ->
        let leftVal, rightVal = (Map.tryFind key leftMap, Map.tryFind key rightMap)
        match leftVal, rightVal with
        | Some left, Some right ->
            if left = right then None else Changed (left, right) |> Some
        | Some left, None ->
            Deleted left |> Some
        | None, Some right ->
            Created right |> Some
        | _ -> failwith "shouldn't happen")

let private easyCreatedOrChanged (changes: EasyAttrChange<'a> list) =
    changes
    |> List.choose (function | Created attr | Changed (_, attr) -> Some attr | _ -> None)
    
type EasyAttrs<'attr,'state when 'attr:equality>(values: 'attr list, uniqueKey: string, keyFunc: 'attr -> int, attrUpdate: 'state -> 'attr -> 'state) =
    member val Values = values
    interface IAttr with
        override this.AttrEquals other =
            match other with
            | :? EasyAttrs<'attr,'state> as otherAttr ->
                values = otherAttr.Values
            | _ ->
                false
        override this.Key =
            "easyattrs:" + uniqueKey
        override this.ApplyTo(target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? ComponentStateTarget<'state> as componentTarget ->
                let changedAttrs =
                    match maybePrev with
                    | Some prevIAttr ->
                        match prevIAttr with
                        | :? EasyAttrs<'attr,'state> as prev ->
                            (easyDiffAttrs keyFunc) prev.Values values
                            |> easyCreatedOrChanged
                        | _ ->
                            failwith "EasyAttrs previous value type match error - did you use a sufficiently unique key?"
                    | None ->
                        // no previous values, use them all
                        values
                let nextState =
                    (componentTarget.State, changedAttrs)
                    ||> List.fold attrUpdate
                componentTarget.Update(nextState)
            | _ ->
                failwith "nope"
