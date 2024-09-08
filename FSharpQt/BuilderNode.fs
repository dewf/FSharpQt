module FSharpQt.BuilderNode

open Org.Whatever.MinimalQtForFSharp
open Extensions

type DepsKey =
    // used by actual widgets
    | IntKey of i: int
    | StrKey of str: string
    | StrIntKey of str: string * i: int
    | StrStrKey of str1: string * str2: string
    // only internally by diff/build
    | AttachKey of str: string
    
type DepsChange =
    | Unchanged
    | Added
    | Removed
    | Swapped
    
type ContentKey =
    System.Object
    
type IViewBinding =
    interface
    end

type Attachment<'msg>(id: string, node: IBuilderNode<'msg>) =
    member val internal Id = id
    member val internal Node = node
    
and BuilderContext<'msg> = {
    // context cannot contain nodes (eg parent node),
    // because when crossing a reactor node boundary, the 'msg type changes ('outerMsg to 'msg)
    // (a new builder context is created, copying the internal content to the new type)
    // reactor nodes might be able to create a wrapper node type but sounds like way too much work for little gain
    ContainingWindow: Widget.Handle option // window, dialog, whatever
}

and IBuilderNode<'msg> =
    interface
        abstract Dependencies: (DepsKey * IBuilderNode<'msg>) list
        abstract Create: ('msg -> unit) -> BuilderContext<'msg> -> unit
        abstract AttachDeps: unit -> unit
        abstract MigrateFrom: IBuilderNode<'msg> -> (DepsKey * DepsChange) list -> unit // will the dispatch ever change?
        abstract Dispose: unit -> unit
        abstract ContentKey: ContentKey
        
        // externally-supplied dependencies of any type, which the node itself has no knowledge of (except for providing the basic property storage)
        // will be added to self-reported dependencies during build/diff process
        abstract Attachments: Attachment<'msg> list
        
        abstract Binding: (string * IViewBinding) option // option for now, maybe a list at a later point if we ever need to expose 2+ things per node
    end
    
and INonVisualNode<'msg> =
    interface
        inherit IBuilderNode<'msg>
    end

// this will allow certain widgets (eg MainWindow) to accept either type
// also removes the burden of implementing IWidgetNode from every single ILayoutNode implementation (the former approach), which was annoying
and IWidgetOrLayoutNode<'msg> =
    interface
        inherit IBuilderNode<'msg>
    end

and IWidgetNode<'msg> =
    interface
        inherit IWidgetOrLayoutNode<'msg>
        abstract Widget: Widget.Handle
    end
    
and ILayoutNode<'msg> =
    interface
        inherit IWidgetOrLayoutNode<'msg>
        abstract member Layout: Layout.Handle
    end
    
and IMenuBarNode<'msg> =
    interface
        inherit IBuilderNode<'msg>
        abstract MenuBar: MenuBar.Handle
    end
    
and IMenuNode<'msg> =
    interface
        inherit IBuilderNode<'msg>
        abstract member Menu: Menu.Handle
    end
    
and IActionNode<'msg> =
    interface
        inherit IBuilderNode<'msg>
        abstract member Action: Action.Handle
    end
    
and IToolBarNode<'msg> =
    interface
        inherit IBuilderNode<'msg>
        abstract member ToolBar: ToolBar.Handle
    end
    
and IStatusBarNode<'msg> =
    interface
        inherit IBuilderNode<'msg>
        abstract member StatusBar: StatusBar.Handle
    end
    
and ITopLevelNode<'msg> =
    interface
        inherit IBuilderNode<'msg>
        // doesn't define any properties / contentkey itself
    end
    
and IWindowNode<'msg> =
    interface
        inherit ITopLevelNode<'msg>
        abstract member WindowWidget: Widget.Handle
        abstract member ShowIfVisible: unit -> unit
    end
    
and IDialogNode<'msg> =
    interface
        inherit ITopLevelNode<'msg>
        abstract member Dialog: Dialog.Handle
    end
    
and IModelNode<'msg> =
    interface
        inherit IBuilderNode<'msg>
        abstract member QtModel: AbstractItemModel.Handle
    end
    
and IAbstractItemDelegateNode<'msg> =
    interface
        inherit IBuilderNode<'msg>
        abstract member AbstractItemDelegate: AbstractItemDelegate.Handle
    end
    
let nodeDepsWithAttachments (node: IBuilderNode<'msg>) =
    let attachDeps =
        node.Attachments
        |> List.map (fun attach -> AttachKey attach.Id, attach.Node)
    node.Dependencies @ attachDeps
    
let dumpTree (node: IBuilderNode<'msg>) =
    let rec inner (indent: int) (node: IBuilderNode<'msg>) =
        let indentStr =
            Array.replicate indent "    "
            |> String.concat ""
        printfn "%s - [%A]" indentStr node.ContentKey
        for _, node in (nodeDepsWithAttachments node) do
            inner (indent + 1) node
    inner 0 node
        
let rec disposeTree(node: IBuilderNode<'msg>) =
    // disposing everything (bottom to top) is somewhat excessive since things like QMainWindow own (SOME OF) their dependents,
    // but:
    // 1. it works for now, Qt seems very well-designed in terms of gracefully removing deleted things from their parents/owners,
    //    which can work in our favor (not having to worry about dependency removal, eg removing a menubar from a window, widgets from a layout, etc)
    // 2. things like QMenu aren't owned by their QMenuBar, so until we have a more intelligent deletion mechanism in place,
    //    for example an IBuilderNode property .OwnedBy ... we might as well be exhaustive. it seems not to cause any problems!
    for _, node in (nodeDepsWithAttachments node) do
        disposeTree node
    node.Dispose()

let rec diff (dispatch: 'msg -> unit) (maybeLeft: IBuilderNode<'msg> option) (maybeRight: IBuilderNode<'msg> option) (context: BuilderContext<'msg>) =
    let createRight (dispatch: 'msg -> unit) (right: IBuilderNode<'msg>) =
        // initial create
        right.Create dispatch context
        
        // are we a window/dialog parent for things deeper down?
        let nextContext =
            match right with
            | :? IWindowNode<'msg> as windowNode ->
                { context with ContainingWindow = Some windowNode.WindowWidget }
            | :? IDialogNode<'msg> as dialogNode ->
                { context with ContainingWindow = Some dialogNode.Dialog }
            | _ ->
                context
                
        // realize dependencies + attachments
        let allDeps =
            nodeDepsWithAttachments right
        for _, node in allDeps do
            diff dispatch None (Some node) nextContext
                
        // attach realized dependencies
        // (the node will know nothing of its attachments, only self-declared .Dependencies)
        right.AttachDeps()
        
        // top-level window visibility is not set (other than an internal flag) during creation, only migration
        // because it doesn't have any dependencies attached until .AttachDeps, so internal layout will not happen correctly, and the window won't be sized to its content
        // later we also have to figure out the right way to account for nested layout changes, propagating them up to the window
        // (seemingly a series of layout.activate() / widget.adjustSize() for every boundary we cross)
        match right with
        | :? IWindowNode<'msg> as windowNode ->
            windowNode.ShowIfVisible()
        | _ ->
            ()

    match (maybeLeft, maybeRight) with
    | None, None ->
        failwith "both sides empty??"

    | None, Some right ->
        createRight dispatch right

    | Some left, None ->
        disposeTree left

    | Some left, Some right when left.GetType() = right.GetType() ->
        // neither side empty, but same type - diff and migrate
        // reconcile and order children via ID
        let leftMap = Map.ofList (nodeDepsWithAttachments left)
        let rightMap = Map.ofList (nodeDepsWithAttachments right)
        let uniqueIds = (Map.keys leftMap @ Map.keys rightMap) |> List.distinct |> List.sort
        let correlated =
            uniqueIds
            |> List.map (fun key ->
                let left = leftMap.TryFind key
                let right = rightMap.TryFind key
                key, left, right)
            
        // are we a window/dialog parent for things deeper down?
        let nextContext =
            // only left has a model right now (pre-migration), and created children could potentially query for the .Widget or whatever (eg Dialogs wanting to know parent windows)
            match left with
            | :? IWindowNode<'msg> as windowNode ->
                { context with ContainingWindow = Some windowNode.WindowWidget }
            | :? IDialogNode<'msg> as dialogNode ->
                { context with ContainingWindow = Some dialogNode.Dialog }
            | _ ->
                context
                
        for _, lch, rch in correlated do
            diff dispatch lch rch nextContext
            
        let depsChanges =
            // we exclude the attachments from the dependency changes sent to .Migrate
            // probably unnecessary, really, since they are generally queried by key and nobody will be looking for them
            let withoutAttachments =
                correlated
                |> List.filter (fun (key, _, _) ->
                    match key with
                    | AttachKey _ -> false
                    | _ -> true)
            // provide a more precise breakdown of the dependency changes
            // saves redundant comparison code in widgets with dependencies of different types (eg MainWindow)
            // see MainWindow.MigrateContent to see how this is typically used
            [for key, lch, rch in withoutAttachments ->
                let changeType =
                    match lch, rch with
                    | None, None ->
                        // how would the ID even exist if it wasn't in either side to begin with?
                        failwith "shouldn't happen (BuilderNode.diff - depsChanges)"
                    | None, Some _ ->
                        Added
                    | Some _, None ->
                        Removed
                    | Some left, Some right ->
                        if left.ContentKey = right.ContentKey then
                            Unchanged
                        else
                            Swapped
                key, changeType]

        // now merge the nodes themselves, with the children having been recursively reconciled above
        // attrs are handled internally
        right.MigrateFrom left depsChanges

    | Some left, Some right ->
        // different types - dispose left, create right
        // (combination of [right empty] and [left empty] cases above)
        // in theeeeory we could reparent existing children to a different parent type, preserving state
        // but honestly that's an extremely exotic use case, why bother?
        disposeTree left
        createRight dispatch right

let build (dispatch: 'msg -> unit) (root: IBuilderNode<'msg>) (initialContext: BuilderContext<'msg>) =
    diff dispatch None (Some root) initialContext
