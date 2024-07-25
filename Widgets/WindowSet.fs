module FSharpQt.Widgets.WindowSet

open FSharpQt.Attrs
open FSharpQt.BuilderNode

type WindowSet<'msg>() =
    inherit PropsRoot<'msg>()
    
    member val Windows: (DepsKey * IWindowNode<'msg>) list = [] with get, set
        
    interface ITopLevelNode<'msg> with
        override this.Dependencies =
            this.Windows
            |> List.map (fun (key, window) -> key, window :> IBuilderNode<'msg>)
            
        override this.Create dispatch buildContext =
            // no model, nothing to do
            ()
            
        override this.AttachDeps () =
            ()
            
        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            // no model, nothing to do
            ()
            
        override this.Dispose() =
            // etc
            ()
            
        override this.ContentKey =
            // there is no internal content, and dependencies are evaluated by their own content keys, so ... ?
            null
            
        override this.Attachments =
            this.Attachments

        override this.Binding = None
