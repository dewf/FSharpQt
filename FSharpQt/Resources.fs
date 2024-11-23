module FSharpQt.Resources
open System
open FSharpQt.MiscTypes
open FSharpQt.Painting

open FSharpQt.Util

type ResourceKey =
    | ColorKey of name: string
    | ImageKey of name: string
    | PixmapKey of name: string
    
[<RequireQualifiedAccess>]
type Resource =
    | Color of color: Color
    | Image of image: Image
    | Pixmap of pixmap: Pixmap
with
    interface IDisposable with
        member this.Dispose() =
            match this with
            | Color color -> dispose color
            | Image image -> dispose image
            | Pixmap pixmap -> dispose pixmap
            
type ResourceManager() =
    let mutable items: Map<ResourceKey, Resource> = Map.empty
    
    interface IDisposable with
        member this.Dispose() =
            printfn "ViewResources tearing down!"
            for pair in items do
                printfn "ViewResources disposing %A" pair.Key
                dispose pair.Value
    
    member this.Set(name: string, color: Color) =
        match items.TryFind (ColorKey name) with
        | Some (Resource.Color existing) ->
            // release existing
            dispose existing
        | _ ->
            // not found (or is wrong type, but obviously that shouldn't happen)
            ()
        items <- items.Add(ColorKey name, Resource.Color color)
        
    member this.Set(name: string, image: Image) =
        match items.TryFind (ImageKey name) with
        | Some (Resource.Image existing) ->
            dispose existing
        | _ ->
            ()
        items <- items.Add(ImageKey name, Resource.Image image)
        
    member this.Set(name: string, pixmap: Pixmap) =
        match items.TryFind (PixmapKey name) with
        | Some (Resource.Pixmap existing) ->
            dispose existing
        | _ ->
            ()
        items <- items.Add(PixmapKey name, Resource.Pixmap pixmap)
        
    member this.DeleteColor(name: string) =
        match items.TryFind (ColorKey name) with
        | Some (Resource.Color existing) ->
            dispose existing
            items <- items.Remove (ColorKey name)
        | _ ->
            printfn "warning: ViewResources.DeleteColor - failed to find color '%s'" name

    member this.DeleteImage(name: string) =
        match items.TryFind (ImageKey name) with
        | Some (Resource.Image existing) ->
            dispose existing
            items <- items.Remove(ImageKey name)
        | _ ->
            printfn "warning: ViewResources.DeleteImage - failed to find image '%s'" name

    member this.DeletePixmap(name: string) =
        match items.TryFind (PixmapKey name) with
        | Some (Resource.Pixmap existing) ->
            dispose existing
            items <- items.Remove (PixmapKey name)
        | _ ->
            printfn "warning: ViewResources.DeletePixmap - failed to find pixmap '%s'" name
    
    member this.Color (name: string) =
        match items.TryFind (ColorKey name) with
        | Some (Resource.Color color) ->
            color
        | _ ->
            failwithf "ViewResources.Color - couldn't find color of name '%s'" name
            
    member this.Image (name: string) =
        match items.TryFind (ImageKey name) with
        | Some (Resource.Image image) ->
            image
        | _ ->
            failwithf "ViewResources.Image - couldn't find image of name '%s'" name
            
    member this.Pixmap (name: string) =
        match items.TryFind (PixmapKey name) with
        | Some (Resource.Pixmap pixmap) ->
            pixmap
        | _ ->
            failwithf "ViewResources.Pixmap - couldn't find pixmap of name '%s'" name
