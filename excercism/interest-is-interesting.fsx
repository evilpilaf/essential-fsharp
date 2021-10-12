module InterestIsInteresting

let (|Negative|Low|Mid|High|) (balance: decimal) =
    match (balance) with
    | b when b < 0m -> Negative
    | b when b < 1000m -> Low
    | b when b < 5000m -> Mid
    | b when b >= 5000m -> High
    | _ -> High

let interestRate (balance: decimal) : single =
    match (balance) with
    | Negative -> -3.213f
    | Low -> 0.5f
    | Mid -> 1.621f
    | High -> 2.475f

let annualBalanceUpdate (balance: decimal) : decimal =
    let interest = interestRate balance |> decimal
    balance * (interest / 100.0M) + balance

let amountToDonate (balance: decimal) (taxFreePercentage: float) : int =
    let donation =
        (float balance * taxFreePercentage / 100.0)

    match balance with
    | x when x > 0.0m -> 2.0 * donation |> int
    | _ -> 0
