open System

// these two are required for all FSharpQt root-level programs / components
open FSharpQt.BuilderNode
open FSharpQt.Reactor
// this is one is generally where enums etc from Qt:: namespace live, so it's effectively a requirement as well
// will probably rename it in the future
open FSharpQt.MiscTypes

// and here we import any widgets we're using
// with some exceptions they are the same as the normal Qt widget names, minus the "Q" prefix
open FSharpQt.Widgets
open MainWindow
open Label
open PushButton
// BoxLayout includes both HBoxLayout and VBoxLayout variants
open BoxLayout

// standard-fare Elm Architecture stuff:

type State = {
    Count: int
}

// Messages represent UI events. for those familiar with Qt, they would be analagous to user-defined "slots"
type Msg =
    | Increment

// kick off the program with this initial state + initial command
// commands will be illustrated in later examples
let init _ =
    { Count = 0 }, Cmd.None
    
// here we transform our State based on whatever just happened
let update state msg =
    match msg with
    | Increment ->
        { state with Count = state.Count + 1 }, Cmd.None

// similar to Elmish/React, the view describes a widget graph that is computed every time our state changes in response to a Msg (UI event)
// this widget graph is then diffed with the previous graph, and changes are applied behind the scenes by setting properties on the native Qt widgets
// widget state is entirely implicit, and unlike Elm/Elmish programs, you don't have to maintain any state for nested components, or route messages to them
// but that's getting a little ahead of ourselves. for now, just treat these like simple Elm(ish) programs - the concept is almost entirely the same
let view state =
    let label =
        let text =
            sprintf "Count: %d" state.Count
        Label(Text = text, Alignment = Center)
    let button =
        PushButton(Text = "Increment", OnClicked = Increment) // Msgs are attached to signals on the widgets, via On<signal>
    let vbox =
        VBoxLayout(Items = [
            // generally things with some notion of "items" wrap the items in a little class constructor
            // long story short it allows us to avoid specifying optional params which would be required in a record constructor,
            // and it helps us avoid having to do annoying casting, the ":> Whatever" that's unfortunately required in some places
            BoxItem(label)
            BoxItem(spacer = 10) // not necessary, just an example of optional args like "spacer" and "stretch"
            BoxItem(button)
        ])
    // the root element of the root/app-level F#/Qt application will generally be either a MainWindow or a WindowSet containing multiple windows
    // technically it could be any node type at all, but then you won't see anything
    // C++ Qt allows you to show any widget as a top-level (eg PushButton), and we might add that for completeness in the future, even though it's silly
    // but for now, just use MainWindow with either a central widget or central layout
    // we will cover multi-window apps in a later example
    MainWindow(WindowTitle = "Counter", CentralLayout = vbox)
    // this part is annoying: we generally have to cast the root-level elements of views/components
    // this is just a quirk of F# type stuff. fortunately we've been able to keep it to a bare minimum in other places
    :> IBuilderNode<Msg>

// just some boilerplate for kicking off an F#/Qt program
[<EntryPoint>]
[<STAThread>]
let main argv =
    use app =
        createApplication init update view
    // you can comment the style out if you want to let Qt default to whatever it deems appropriate for your platform
    // for styles to work Qt has to be able to find its plugins
    // on Windows you might need to copy the /plugins folder from your Qt distribution to your application .exe location
    // not sure if it works by default when Qt binaries are in your PATH
    app.SetStyle(Fusion)
    app.Run argv
