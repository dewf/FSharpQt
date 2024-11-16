module FSharpQt.NativeInitModule
open Org.Whatever.MinimalQtForFSharp

// ====================================================================================
// any type that needs the native stuff ready to go (for top-level `let` bindings),
// needs to call this in its static initializer (static do)
// eg Color, Font, Pixmap, etc ...
let mutable private loaded = false
let ensureNativeLibraryLoaded() =
    if not loaded then
        Library.Init()
        loaded <- true
