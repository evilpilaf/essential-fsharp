// type Option<'T> =
//     | Some of 'T
//     | None

let x = Some 10

// let (Some y) = x

let z (zz: int option) =
    match zz with
    | Some x -> x
    | None -> 0

open System

type OrderId = Guid

type CustomerId = CustomerIdx of Guid

let customerId = Guid.NewGuid() |> CustomerIdx

let (CustomerIdx c) = customerId

let (oid: OrderId) = Guid.NewGuid()

let (g: Guid) = oid

type Coordinate = decimal * decimal

let (myPos: Coordinate) = 5M, 50M

let (lat, long) = myPos

let parse (input: string) =
    let isDate, date = DateTime.TryParse input
    if (isDate) then Some date else None

let date = parse "2021-09-02"

let date = parse ""
