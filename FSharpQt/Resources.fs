module FSharpQt.Resources
open System
open FSharpQt.MiscTypes
open FSharpQt.Painting

open FSharpQt.Util

type ResourceKey<'id> =
    // partition keys by type
    // no big rationale for allowing this (eg both a color and an image with the same name/key),
    // just seemed easier than error messages saying "item @ key was wrong type" etc 
    | ColorKey of name: 'id
    | ImageKey of name: 'id
    | PixmapKey of name: 'id
    
[<RequireQualifiedAccess>]
type Resource =
    | Color of color: Color.Owned
    | Image of image: Image
    | Pixmap of pixmap: Pixmap
with
    interface IDisposable with
        member this.Dispose() =
            match this with
            | Color color -> dispose color
            | Image image -> dispose image
            | Pixmap pixmap -> dispose pixmap
            
type ResourceManager<'id when 'id: comparison>() =
    let mutable items: Map<ResourceKey<'id>, Resource> = Map.empty
    
    interface IDisposable with
        member this.Dispose() =
            printfn "ViewResources tearing down!"
            for pair in items do
                printfn "ViewResources disposing %A" pair.Key
                dispose pair.Value
    
    member this.Set(name: 'id, color: Color.Owned) =
        match items.TryFind (ColorKey name) with
        | Some (Resource.Color existing) ->
            // release existing
            dispose existing
        | _ ->
            // not found (or is wrong type, but obviously that shouldn't happen)
            ()
        items <- items.Add(ColorKey name, Resource.Color color)
        
    member this.Set(name: 'id, image: Image) =
        match items.TryFind (ImageKey name) with
        | Some (Resource.Image existing) ->
            dispose existing
        | _ ->
            ()
        items <- items.Add(ImageKey name, Resource.Image image)
        
    member this.Set(name: 'id, pixmap: Pixmap) =
        match items.TryFind (PixmapKey name) with
        | Some (Resource.Pixmap existing) ->
            dispose existing
        | _ ->
            ()
        items <- items.Add(PixmapKey name, Resource.Pixmap pixmap)
        
    member this.DeleteColor(name: 'id) =
        match items.TryFind (ColorKey name) with
        | Some (Resource.Color existing) ->
            dispose existing
            items <- items.Remove (ColorKey name)
        | _ ->
            printfn "warning: ViewResources.DeleteColor - failed to find color '%A'" name

    member this.DeleteImage(name: 'id) =
        match items.TryFind (ImageKey name) with
        | Some (Resource.Image existing) ->
            dispose existing
            items <- items.Remove(ImageKey name)
        | _ ->
            printfn "warning: ViewResources.DeleteImage - failed to find image '%A'" name

    member this.DeletePixmap(name: 'id) =
        match items.TryFind (PixmapKey name) with
        | Some (Resource.Pixmap existing) ->
            dispose existing
            items <- items.Remove (PixmapKey name)
        | _ ->
            printfn "warning: ViewResources.DeletePixmap - failed to find pixmap '%A'" name
    
    member this.Color (name: 'id) =
        match items.TryFind (ColorKey name) with
        | Some (Resource.Color color) ->
            color
        | _ ->
            failwithf "ViewResources.Color - couldn't find color of name '%A'" name
            
    member this.Image (name: 'id) =
        match items.TryFind (ImageKey name) with
        | Some (Resource.Image image) ->
            image
        | _ ->
            failwithf "ViewResources.Image - couldn't find image of name '%A'" name
            
    member this.Pixmap (name: 'id) =
        match items.TryFind (PixmapKey name) with
        | Some (Resource.Pixmap pixmap) ->
            pixmap
        | _ ->
            failwithf "ViewResources.Pixmap - couldn't find pixmap of name '%A'" name
