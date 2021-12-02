let lst = []

let lst2 = [ 1 .. 10 ]

let lst3 = [ 1; 2; 3 ]


let times3 = lst2 |> List.map (fun x -> x * 3)

let evens = lst2 |> List.filter (fun x -> x % 2 = 0)

let newLst =
    lst3 |> List.iter (fun x -> printfn $"{x}")

let reducedLst = lst2 |> List.reduce (+)

let foldedLst = lst2 |> List.fold (*) 1

let partitionedLst =
    lst2 |> List.partition (fun x -> x % 2 <> 0)

let getRepeatedElement (l: int list) =
    l
    |> List.groupBy id
    |> List.map (fun (id, l) -> id, List.length l)
    |> List.sortByDescending (fun (_, count) -> count)
    |> List.tryHead

[ 1
  2
  4
  6
  3
  8
  9
  1
  6
  3
  1
  3
  2
  4
  3
  2
  1
  5
  6
  2
  4
  6 ]
|> getRepeatedElemet

[] |> Map.ofList
