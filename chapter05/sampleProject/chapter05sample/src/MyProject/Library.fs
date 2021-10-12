namespace MyProject.Orders

type Item = { ProductId: int; Quantity: int }

type Order = { Id: int; Items: Item list }

module Domain =
    let recalculate items =
        items
        |> List.groupBy (fun i -> i.ProductId)
        |> List.map
            (fun (id, items) ->
                { ProductId = id
                  Quantity = items |> List.sumBy (fun i -> i.Quantity) })

    let addItem newItem order =
        let items =
            newItem :: order.Items
            |> recalculate
            |> List.sortBy (fun i -> i.ProductId)

        { order with Items = items }

    let addItems newItems order =
        let items =
            newItems @ order.Items
            |> recalculate
            |> List.sortBy (fun i -> i.ProductId)

        { order with Items = items }

    let removeProduct productId order =
        let items =
            order.Items
            |> List.filter (fun x -> x.ProductId <> productId)
            |> List.sortBy (fun i -> i.ProductId)

        { order with Items = items }

    let reduceItem productId quantity order =
        let items =
            { ProductId = productId
              Quantity = -quantity }
            :: order.Items
            |> recalculate
            |> List.filter (fun x -> x.Quantity > 0)
            |> List.sortBy (fun i -> i.ProductId)

        { order with Items = items }

    let clearItems order = { order with Items = [] }
