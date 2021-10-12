// type Option<'T> =
//     | Some of 'T
//     | None

open System

let tryParseDateTime (input: string) =
    let (success, value) = DateTime.TryParse input
    if success then Some value else None

let tryParseDateTime' (input: string) =
    match DateTime.TryParse input with
    | true, result -> Some result
    | _ -> None

let isDate = tryParseDateTime "2019-08-01"
let isDate' = tryParseDateTime' "2019-08-01"

let isNotDate = tryParseDateTime "Hello"
let isNotDate' = tryParseDateTime' "Hello"


type PersonName =
    { FirstName: string
      MiddleName: string option
      LastName: string }

let firstPerson =
    { FirstName = "Ivette"
      MiddleName = Some "del Socorro"
      LastName = "Diaz del Castillo" }

let otherPerson =
    { FirstName = "Mauricio"
      MiddleName = None
      LastName = "Dominguez Diaz del Castillo" }
