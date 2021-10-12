namespace OrderTests

open MyProject.Orders
open MyProject.Orders.Domain
open Xunit
open FsUnit

module ``Add item to order`` =
    [<Fact>]
    let ``product does not exist in empty order`` () =
        let myEmptyOrder = { Id = 1; Items = [] }

        let expected =
            { Id = 1
              Items = [ { ProductId = 1; Quantity = 3 } ] }

        let actual =
            myEmptyOrder
            |> addItem { ProductId = 1; Quantity = 3 }

        actual |> should equal expected

    [<Fact>]
    let ``product does not exist in order`` () =
        let myOrder =
            { Id = 1
              Items = [ { ProductId = 1; Quantity = 1 } ] }

        let expected =
            { Id = 1
              Items =
                  [ { ProductId = 1; Quantity = 1 }
                    { ProductId = 2; Quantity = 5 } ] }

        let actual =
            myOrder |> addItem { ProductId = 2; Quantity = 5 }

        actual |> should equal expected

    [<Fact>]
    let ``product exists in order`` () =
        let myOrder =
            { Id = 1
              Items = [ { ProductId = 1; Quantity = 1 } ] }

        let expected =
            { Id = 1
              Items = [ { ProductId = 1; Quantity = 4 } ] }

        let actual =
            myOrder |> addItem { ProductId = 1; Quantity = 3 }

        actual |> should equal expected

module ``add multiple items to an order`` =

    [<Fact>]
    let ``new products added to empty order`` () =
        let myEmptyOrder = { Id = 1; Items = [] }

        let expected =
            { Id = 1
              Items =
                  [ { ProductId = 1; Quantity = 1 }
                    { ProductId = 2; Quantity = 5 } ] }

        let actual =
            myEmptyOrder
            |> addItems [ { ProductId = 1; Quantity = 1 }
                          { ProductId = 2; Quantity = 5 } ]

        actual |> should equal expected

    [<Fact>]
    let ``new products and updated existing to order`` () =
        let myOrder =
            { Id = 1
              Items = [ { ProductId = 1; Quantity = 1 } ] }

        let expected =
            { Id = 1
              Items =
                  [ { ProductId = 1; Quantity = 2 }
                    { ProductId = 2; Quantity = 5 } ] }

        let actual =
            myOrder
            |> addItems [ { ProductId = 1; Quantity = 1 }
                          { ProductId = 2; Quantity = 5 } ]

        actual |> should equal expected

module ``Reduce item quantity`` =

    [<Fact>]
    let reduceSomeExistingItemAssert () =
        let myOrder =
            { Id = 1
              Items = [ { ProductId = 1; Quantity = 5 } ] }

        let expected =
            { Id = 1
              Items = [ { ProductId = 1; Quantity = 2 } ] }

        let actual = myOrder |> reduceItem 1 3
        actual |> should equal expected

    [<Fact>]
    let reduceAllExistingItemAssert () =
        let myOrder =
            { Id = 2
              Items = [ { ProductId = 1; Quantity = 5 } ] }

        let expected = { Id = 2; Items = [] }
        let actual = myOrder |> reduceItem 1 5
        actual |> should equal expected

    [<Fact>]
    let reduceNonexistantItemAssert () =
        let myOrder =
            { Id = 3
              Items = [ { ProductId = 1; Quantity = 1 } ] }

        let expected =
            { Id = 3
              Items = [ { ProductId = 1; Quantity = 1 } ] }

        let actual = myOrder |> reduceItem 2 5
        actual |> should equal expected

    [<Fact>]
    let reduceNonexistantItemEmptyOrderAssert () =
        let myEmptyOrder = { Id = 4; Items = [] }
        let expected = { Id = 4; Items = [] }
        let actual = myEmptyOrder |> reduceItem 2 5
        actual |> should equal expected

module ``Empty an order of all items`` =

    [<Fact>]
    let ``order with existing items`` () =
        let myOrder =
            { Id = 1
              Items = [ { ProductId = 1; Quantity = 1 } ] }

        let expected = { Id = 1; Items = [] }
        let actual = myOrder |> clearItems
        actual |> should equal expected

    [<Fact>]
    let ``empty order unchanged`` () =
        let myEmptyOrder = { Id = 2; Items = [] }
        let expected = { Id = 2; Items = [] }
        let actual = myEmptyOrder |> clearItems
        actual |> should equal expected
