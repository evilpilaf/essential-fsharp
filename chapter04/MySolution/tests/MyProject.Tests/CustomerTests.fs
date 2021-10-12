namespace MyProjectTests

open Xunit
open FsUnit
open MyProject
open MyProject.Domain

[<AutoOpen>]
module TestHelpers =

    let failTest msg = Assert.True(false, msg)

    let passTest = Assert.True(true)

    let isCustomerAlreadyExistsException message customerId =
        let expected =
            $"Customer '{customerId}' already exists"

        match message with
        | s when s = expected -> passTest
        | _ -> failTest (sprintf "%A not expected" message)

module ``Convert customer to eligible`` =

    let sourceCustomer =
        { CustomerId = "John"
          IsRegistered = true
          IsEligible = true }

    [<Fact>]
    let ``should succeed if not currently eligible`` () =
        let customer =
            { sourceCustomer with
                  IsEligible = false }

        let upgraded = convertToEligible customer
        upgraded |> should equal sourceCustomer

    [<Fact>]
    let ``should return eligible customer unchanged`` () =
        let upgraded = convertToEligible sourceCustomer
        upgraded |> should equal sourceCustomer

module ``Create customer`` =

    let name = "John"

    [<Fact>]
    let ``should succeed if customer does not exist`` () =
        let existing = None
        let result = tryCreateCustomer name existing

        match result with
        | Ok customer ->
            customer
            |> should
                equal
                { CustomerId = name
                  IsRegistered = true
                  IsEligible = false }
        | Error ex -> ex.ToString() |> failTest

    [<Fact>]
    let ``should fail if customer does exist`` () =
        let existing =
            Some
                { CustomerId = name
                  IsRegistered = true
                  IsEligible = false }

        let result = tryCreateCustomer name existing

        match result with
        | Error ex -> isCustomerAlreadyExistsException ex.Message name
        | Ok customer -> failTest $"{customer} was not expected"
