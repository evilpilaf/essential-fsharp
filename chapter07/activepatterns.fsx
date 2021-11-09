open System

let parse (input: string) =
    match DateTime.TryParse input with
    | (true, value) -> printfn "%A" value
    | (false, _) -> printfn "'%s' is not a valid date" input


let (|ValidDate|_|) (input: string) =
    let success, value = DateTime.TryParse input
    if success then Some value else None

let parse' input =
    match input with
    | ValidDate d -> printfn "%A" d
    | _ -> printfn "'%s' is not a valid date" input

let isDate = parse' "2019-12-20"
let isNotDate = parse' "Hello"


let fizzbuzz mapping n =
    mapping
    |> List.map (fun (divisor, result) -> if n % divisor = 0 then result else "")
    |> List.reduce (+)
    |> fun input -> if input = "" then string n else input


fizzbuzz
    [ (3, "Fizz")
      (5, "Buzz")
      (7, "Bazz") ]
    118


type Score = int * int

let (|CorrectScore|_|) (expected: Score, actual: Score) =
    if expected = actual then
        Some()
    else
        None

let (|Draw|HomeWin|AwayWin|) (score: Score) =
    match score with
    | (h, a) when h = a -> Draw
    | (h, a) when h > a -> HomeWin
    | _ -> AwayWin

let (|CorrectResult|_|) (expected: Score, actual: Score) =
    match (expected, actual) with
    | (Draw, Draw) -> Some()
    | (HomeWin, HomeWin) -> Some()
    | (AwayWin, AwayWin) -> Some()
    | _ -> None

let goalsScore (expected: Score) (actual: Score) =
    let (h, a) = expected
    let (h', a') = actual
    let home = [ h; h' ] |> List.min
    let away = [ a; a' ] |> List.min
    (home * 15) + (away * 20)

let resultScore (expected: Score) (actual: Score) =
    match (expected, actual) with
    | CorrectScore -> 400
    | CorrectResult -> 100
    | _ -> 0

let calculatePoints (expected: Score) (actual: Score) =
    [ resultScore; goalsScore ]
    |> List.sumBy (fun f -> f expected actual)

calculatePoints (0, 0) (0, 0) = 400
calculatePoints (3, 2) (3, 2) = 485
calculatePoints (5, 1) (4, 3) = 180
calculatePoints (2, 1) (0, 7) = 20
calculatePoints (2, 2) (3, 3) = 170
