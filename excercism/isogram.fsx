// Instructions
// Determine if a word or phrase is an isogram.

// An isogram (also known as a "nonpattern word") is a word or phrase without a repeating letter, however spaces and hyphens are allowed to appear multiple times.

// Examples of isograms:

// lumberjacks
// background
// downstream
// six-year-old
// The word isograms, however, is not an isogram, because the s repeats.


module Isogram

open System

let isIsogram (str: string) =
    str.ToLower().ToCharArray()
    |> Array.filter Char.IsLetter
    |> Array.countBy id
    |> Array.forall (fun (_, count) -> count = 1)


let lumberjacks = isIsogram "lumberjacks"
let background = isIsogram "background"
let isogram = not (isIsogram "Isograms")
