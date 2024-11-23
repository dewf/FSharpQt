module FSharpQt.Resources
open System
open FSharpQt.MiscTypes
open FSharpQt.Painting

type ResourceKey =
    | ColorKey of name: string
    | ImageKey of name: string
    | PixmapKey of name: string
    
[<RequireQualifiedAccess>]
type Resource =
    | Color of color: Color.Owned
    | Image of image: Image
    | Pixmap of pixmap: Pixmap
with
    interface IDisposable with
        member this.Dispose() =
            match this with
            | Color color -> (color :> IDisposable).Dispose()
            | Image image -> (image :> IDisposable).Dispose()
            | Pixmap pixmap -> (pixmap :> IDisposable).Dispose()

type ViewResources = {
    Items: Map<ResourceKey, Resource>
} with
    static member Init = { Items = Map.empty }
    
    interface IDisposable with
        member this.Dispose() =
            printfn "ViewResources tearing down!"
            for pair in this.Items do
                printfn "ViewResources disposing %A" pair.Key
                (pair.Value :> IDisposable).Dispose()
    
    member this.Add(name: string, color: Color) =
        match this.Items.TryFind (ColorKey name) with
        | Some (Resource.Color existing) ->
            // release existing
            (existing :> IDisposable).Dispose()
        | _ ->
            // not found (or is wrong type, but obviously that shouldn't happen)
            ()
        let realized =
            match color with
            | :? Color.Owned as owned -> owned
            | _ -> color.Realize()
        { this with Items = this.Items.Add(ColorKey name, Resource.Color realized) }
        
    member this.Add(name: string, image: Image) =
        match this.Items.TryFind (ImageKey name) with
        | Some (Resource.Image existing) ->
            (existing :> IDisposable).Dispose()
        | _ ->
            ()
        { this with Items = this.Items.Add(ImageKey name, Resource.Image image) }
        
    member this.Add(name: string, pixmap: Pixmap) =
        match this.Items.TryFind (PixmapKey name) with
        | Some (Resource.Pixmap existing) ->
            (existing :> IDisposable).Dispose()
        | _ ->
            ()
        { this with Items = this.Items.Add(PixmapKey name, Resource.Pixmap pixmap) }
        
    member this.DeleteColor(name: string) =
        match this.Items.TryFind (ColorKey name) with
        | Some (Resource.Color existing) ->
            (existing :> IDisposable).Dispose()
            { this with Items = this.Items.Remove (ColorKey name) }
        | _ ->
            printfn "warning: ViewResources.DeleteColor - failed to find color '%s'" name
            this

    member this.DeleteImage(name: string) =
        match this.Items.TryFind (ImageKey name) with
        | Some (Resource.Image existing) ->
            (existing :> IDisposable).Dispose()
            { this with Items = this.Items.Remove (ImageKey name) }
        | _ ->
            printfn "warning: ViewResources.DeleteImage - failed to find image '%s'" name
            this

    member this.DeletePixmap(name: string) =
        match this.Items.TryFind (PixmapKey name) with
        | Some (Resource.Pixmap existing) ->
            (existing :> IDisposable).Dispose()
            { this with Items = this.Items.Remove (PixmapKey name) }
        | _ ->
            printfn "warning: ViewResources.DeletePixmap - failed to find pixmap '%s'" name
            this
    
    member this.Color (name: string) =
        match this.Items.TryFind (ColorKey name) with
        | Some (Resource.Color color) ->
            color
        | _ ->
            failwithf "ViewResources.Color - couldn't find color of name '%s'" name
            
    member this.Image (name: string) =
        match this.Items.TryFind (ImageKey name) with
        | Some (Resource.Image image) ->
            image
        | _ ->
            failwithf "ViewResources.Image - couldn't find image of name '%s'" name
            
    member this.Pixmap (name: string) =
        match this.Items.TryFind (PixmapKey name) with
        | Some (Resource.Pixmap pixmap) ->
            pixmap
        | _ ->
            failwithf "ViewResources.Pixmap - couldn't find pixmap of name '%s'" name
