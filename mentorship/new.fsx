type Customer =
    { Id: int
      IsVip: bool
      Credit: decimal }

let getPurchases customer =
    if customer.Id % 2 = 0 then
        (customer, 120M)
    else
        (customer, 80M)

let tryPromoteToVip (blah: bool) purchases =
    let customer, amount = purchases

    if amount > 100M then
        { customer with IsVip = true }
    else
        customer

let logger = true

let increaseCreditIfVip customer =
    if customer.IsVip then
        { customer with
              Credit = customer.Credit + 100M }
    else
        { customer with
              Credit = customer.Credit + 50M }

let upgradeCustomerComposed l1 =
    getPurchases
    >> tryPromoteToVip l1
    >> increaseCreditIfVip

// let upgradeCustomerNested customer =
//     increaseCreditIfVip (tryPromoteToVip (getPurchases customer))

// let upgradeCustomerProcedural customer =
//     let customerWithPurchases = getPurchases customer
//     let promotedCustomer = tryPromoteToVip customerWithPurchases
//     let increasedCreditCustomer = increaseCreditIfVip promotedCustomer
//     increasedCreditCustomer

let upgradeCustomerPiped l1 customer =
    customer
    |> getPurchases
    |> tryPromoteToVip l1
    |> increaseCreditIfVip
    |> fun c ->
        match c with
        | { IsVip = true; Id = id } as x -> Some x
        | _ -> None
    |> function
        | Some x -> x.Credit
        | None -> -1.0M
// |> fun c -> match c with | x when x.IsVip -> true | _ -> false

let customerVIP = { Id = 1; IsVip = true; Credit = 0.0M }

let customerSTD =
    { Id = 2
      IsVip = false
      Credit = 100.0M }

let assertVIP =
    upgradeCustomerComposed logger customerVIP = { Id = 1
                                                   IsVip = true
                                                   Credit = 100.0M }


// let assertStdToVIP =
//     upgradeCustomerComposed customerSTD = { Id = 2
//                                             IsVip = true
//                                             Credit = 200.0M }

// let assertSTD =
//     upgradeCustomerPiped
//         { customerSTD with
//               Id = 3
//               Credit = 50.0M } = { Id = 3
//                                    IsVip = false
//                                    Credit = 100.0M }
