module FSharpQt.Widgets.RadioButton

open FSharpQt.BuilderNode
open System
open Org.Whatever.MinimalQtForFSharp

open FSharpQt.Attrs

type internal Signal = unit

// no attrs

type Props<'msg>() =
    inherit AbstractButton.Props<'msg>()
    
    member internal this.SignalMask = enum<RadioButton.SignalMask> (int this._signalMask)
    
    member internal this.SignalMapList =
        // prepend to parent signal map funcs
        NullSignalMapFunc() :> ISignalMapFunc :: base.SignalMapList
        
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit AbstractButton.ModelCore<'msg>(dispatch)
    let mutable radioButton: RadioButton.Handle = null
    let mutable currentMask = enum<RadioButton.SignalMask> 0
    // no signals, no guards

    member this.RadioButton
        with get() =
            radioButton
        and set value =
            this.AbstractButton <- value
            radioButton <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? NullSignalMapFunc ->
                // no signals in RadioButton
                ()
            | _ ->
                failwithf "RadioButton.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "RadioButton.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            radioButton.SetSignalMask(value)
            currentMask <- value
            
    interface RadioButton.SignalHandler with
        // object =========================
        member this.Destroyed(obj: Object.Handle) =
            (this :> Object.SignalHandler).Destroyed(obj)
        member this.ObjectNameChanged(name: string) =
            (this :> Object.SignalHandler).ObjectNameChanged(name)
        // widget =========================
        member this.CustomContextMenuRequested pos =
            (this :> Widget.SignalHandler).CustomContextMenuRequested pos
        member this.WindowIconChanged icon =
            (this :> Widget.SignalHandler).WindowIconChanged icon
        member this.WindowTitleChanged title =
            (this :> Widget.SignalHandler).WindowTitleChanged title
        // abstractbutton =================
        member this.Clicked checkState =
            (this :> AbstractButton.SignalHandler).Clicked checkState
        member this.Pressed() =
            (this :> AbstractButton.SignalHandler).Pressed()
        member this.Released() =
            (this :> AbstractButton.SignalHandler).Released()
        member this.Toggled checkState =
            (this :> AbstractButton.SignalHandler).Toggled checkState
        // none of our own
    
    interface IDisposable with
        member this.Dispose() =
            radioButton.Dispose()

type private Model<'msg>(dispatch: 'msg -> unit) as this =
    inherit ModelCore<'msg>(dispatch)
    do
        this.RadioButton <- RadioButton.Create(this)

let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (initialMask: RadioButton.SignalMask) =
    let model = new Model<'msg>(dispatch)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- initialMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: RadioButton.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()

type RadioButton<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    interface IWidgetNode<'msg> with
        override this.Dependencies = []

        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask
            
        override this.AttachDeps () =
            ()

        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> RadioButton<'msg>)
            let nextAttrs = diffAttrs left'.Attrs this.Attrs |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask

        override this.Dispose() =
            (this.model :> IDisposable).Dispose()

        override this.Widget =
            this.model.RadioButton
            
        override this.ContentKey =
            this.model.RadioButton
            
        override this.Attachments =
            this.Attachments

        override this.Binding = None
