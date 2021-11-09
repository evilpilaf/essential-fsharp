module Raindrops

let mapping =
    [ (3, "Pling")
      (5, "Plang")
      (7, "Plong") ]

let convert (number: int) : string =
    mapping
    |> List.map
        (fun (divisor, result) ->
            if number % divisor = 0 then
                result
            else
                "")
    |> List.reduce (+)
    |> fun input ->
        if input = "" then
            $"{number}"
        else
            input

let assert28 = convert 28 = "Plong"
let assert30 = convert 30 = "PlingPlang"
let assert34 = convert 34 = "34"
