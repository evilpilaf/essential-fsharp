module Bob

open System
open System.Text.RegularExpressions

let isUppercase (input: string) : bool =
    Regex.Match(input, "(?=^[^a-z]*$).*[A-Z]").Success

let isQuestion (input: string) : bool = input.TrimEnd().EndsWith('?')

let isEmptyAddress (input: string) : bool = input |> String.IsNullOrWhiteSpace

let response (input: string) : string =
    match input with
    | _ when isEmptyAddress input -> "Fine. Be that way!"
    | _ when isQuestion input && isUppercase input -> "Calm down, I know what I'm doing!"
    | _ when isQuestion input -> "Sure."
    | _ when isUppercase input -> "Whoa, chill out!"
    | _ -> "Whatever."
