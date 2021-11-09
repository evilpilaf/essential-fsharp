type Data = { Id: int; Rate: int }

type Category =
    | High
    | MediumHigh
    | MediumLow
    | Low

let categorize (data: Data) : Category =
    match data.Rate with
    | r when r >= 75 -> High
    | r when r >= 50 -> MediumHigh
    | r when r >= 25 -> MediumLow
    | _ -> Low

let randomData =
    let r = System.Random()
    fun () -> r.Next(0, 10), r.Next(0, 101)

let myRandomData =
    List.init 25 (fun _ -> randomData ())
    |> List.map (fun (i, r) -> { Id = i; Rate = r })

let greatest =
    myRandomData |> List.maxBy (fun x -> x.Rate)

let smallestPerId =
    myRandomData
    |> List.groupBy (fun x -> x.Id)
    |> List.map (fun (_, lst) -> lst |> List.minBy (fun x -> x.Rate))
    |> List.sortBy (fun x -> x.Id)


let categorizedData =
    myRandomData
    |> List.groupBy categorize
    |> List.map (fun (cat, d) -> (cat, d |> List.length))


let categorizedData' =
    myRandomData
    |> List.groupBy categorize
    |> List.map (fun x -> (fst x, snd x |> List.length))
