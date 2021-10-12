type Customer =
    { Id: int
      IsVip: bool
      Credit: decimal }

let getPurchases customer =
    if customer.Id % 2 = 0 then
        (customer, 120M)
    else
        (customer, 80M)

let tryPromoteToVip purchases =
    let customer, amount = purchases

    if amount > 100M then
        { customer with IsVip = true }
    else
        customer

let increaseCreditIfVip customer =
    if customer.IsVip then
        { customer with
              Credit = customer.Credit + 100M }
    else
        { customer with
              Credit = customer.Credit + 50M }

let upgradeCustomerComposed =
    getPurchases
    >> tryPromoteToVip
    >> increaseCreditIfVip

let upgradeCustomerNested customer =
    increaseCreditIfVip (tryPromoteToVip (getPurchases customer))

let upgradeCustomerProcedural customer =
    let customerWithPurchases = getPurchases customer
    let promotedCustomer = tryPromoteToVip customerWithPurchases
    let increasedCreditCustomer = increaseCreditIfVip promotedCustomer
    increasedCreditCustomer

let upgradeCustomerPiped customer =
    customer
    |> getPurchases
    |> tryPromoteToVip
    |> increaseCreditIfVip

let customerVIP = { Id = 1; IsVip = true; Credit = 0.0M }

let customerSTD =
    { Id = 2
      IsVip = false
      Credit = 100.0M }

let assertVIP =
    upgradeCustomerComposed customerVIP = { Id = 1
                                            IsVip = true
                                            Credit = 100.0M }

let assertStdToVIP =
    upgradeCustomerComposed customerSTD = { Id = 2
                                            IsVip = true
                                            Credit = 200.0M }

let assertSTD =
    upgradeCustomerPiped
        { customerSTD with
              Id = 3
              Credit = 50.0M } = { Id = 3
                                   IsVip = false
                                   Credit = 100.0M }
