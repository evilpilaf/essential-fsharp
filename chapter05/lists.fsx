let items =
    [ for x in 1 .. 5 do
          x ]

let extendedItems = 6 :: items

let readList items =
    match items with
    | [] -> "Empty List"
    | [ head ] -> $"Head: {head}"
    | head :: tail -> sprintf "Head: %A and Tail: %A" head tail

let emptyList = readList []
let multipleList = readList extendedItems
let singleItemList = readList [ 1 ]


let myList = [ 1 .. 9 ]

let getEvens items =
    items |> List.filter (fun x -> x % 2 = 0)

let evens = getEvens myList // [2;4;6;8]

// Why did this fail without type annotations?
let sum (items: int list) = items |> List.sum

let mySum = sum myList

let triple items = items |> List.map (fun x -> x * 3)

let myTriples = triple myList

let print items =
    items
    |> List.iter (fun x -> (printfn $"My value is {x}"))

// Why ignore?
print myList |> ignore

let items2 =
    [ (1, 0.25M)
      (5, 0.25M)
      (1, 2.25M)
      (1, 125M)
      (7, 10.9M) ]

let sum2 items =
    items
    |> List.map (fun (q, p) -> decimal q * p)
    |> List.sum


let getTotal items =
    items
    |> List.fold (fun acc (q, p) -> acc + decimal q * p) 0M

let total = getTotal items2
