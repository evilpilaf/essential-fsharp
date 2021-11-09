namespace MyProject

type Customer =
    { CustomerId: string
      IsRegistered: bool
      IsEligible: bool }

module Db =
    let tryGetCustomer customerId =
        try
            [ { CustomerId = "John"
                IsRegistered = true
                IsEligible = true }
              { CustomerId = "Mary"
                IsRegistered = true
                IsEligible = true }
              { CustomerId = "Richard"
                IsRegistered = true
                IsEligible = false }
              { CustomerId = "Sarah"
                IsRegistered = false
                IsEligible = false } ]

            |> List.tryFind (fun c -> c.CustomerId = customerId)
            |> Ok
        with
        | ex -> Error ex

    let saveCustomer (customer: Customer) =
        try
            Ok()
        with
        | ex -> Error ex

module Domain =

    let trySaveCustomer customer =
        match customer with
        | Some c -> Db.saveCustomer c
        | None -> Ok()

    let createCustomer customerId =
        { CustomerId = customerId
          IsRegistered = true
          IsEligible = false }

    let tryCreateCustomer customerId (customer: Customer option) =
        try
            match customer with
            | Some _ -> raise (exn $"Customer '{customerId}' already exists")
            | None -> Ok(createCustomer customerId)
        with
        | ex -> Error ex

    let convertToEligible customer =
        if not customer.IsEligible then
            { customer with IsEligible = true }
        else
            customer

    let upgradeCustomer customerId =
        customerId
        |> Db.tryGetCustomer
        |> Result.map (Option.map convertToEligible)
        |> Result.bind trySaveCustomer

    let registerCustomer customerId =
        customerId
        |> Db.tryGetCustomer
        |> Result.bind (tryCreateCustomer customerId)
        |> Result.bind Db.saveCustomer
