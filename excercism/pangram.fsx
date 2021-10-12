module Pangram

open System

let hasLetter (letter: char) (input: string) : bool =
    input.Contains(letter, StringComparison.OrdinalIgnoreCase)

let rec hasAll (letters: char list) (input: string) : bool =
    match letters with
    | [] -> true
    | [ head ] -> hasLetter head input
    | head :: tail -> hasLetter head input && hasAll tail input


let isPangram (input: string) : bool = input |> hasAll [ 'a' .. 'z' ]
