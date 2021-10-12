open System

type Customer =
    { Id: int
      IsVip: bool
      Credit: decimal }

let getPurchases customer =
    try
        if customer.Id % 2 = 0 then
            (customer, 120M)
        else
            (customer, 80M)
        |> Ok
    with
    | ex -> Error ex

let tryPromoteToVip purchases =
    let customer, amount = purchases

    if amount > 100M then
        { customer with IsVip = true }
    else
        customer

let increaseCreditIfVip customer =
    try
        let credit = if customer.IsVip then 100.0M else 50.0M

        Ok
            { customer with
                  Credit = customer.Credit + credit }
    with
    | ex -> Error ex

let map oneTrackFunction resultInput =
    match resultInput with
    | Ok s -> Ok(oneTrackFunction s)
    | Error f -> Error f

let bind switchFunction resultInput =
    match resultInput with
    | Ok s -> switchFunction s
    | Error f -> Error f

let upgradeCustomer customer =
    customer
    |> getPurchases
    |> Result.map tryPromoteToVip
    |> Result.bind increaseCreditIfVip


let customerVIP = { Id = 1; IsVip = true; Credit = 0.0M }

let customerSTD =
    { Id = 2
      IsVip = false
      Credit = 100.0M }

let assertVIP =
    upgradeCustomer customerVIP = Ok
                                      { Id = 1
                                        IsVip = true
                                        Credit = 100.0M }

let assertSTDtoVIP =
    upgradeCustomer customerSTD = Ok
                                      { Id = 2
                                        IsVip = true
                                        Credit = 200.0M }

let assertSTD =
    upgradeCustomer
        { customerSTD with
              Id = 3
              Credit = 50.0M } = Ok
                                     { Id = 3
                                       IsVip = false
                                       Credit = 100.0M }
