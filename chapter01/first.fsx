type RegisteredCustomer = { Id: string; IsEligible: bool }

type UnregisteredCustomer = { Id: string }

type Customer =
    | Registered of RegisteredCustomer
    | Guest of UnregisteredCustomer

let calculateTotal customer spend =
    let discount =
        match customer with
        | Registered c when c.IsEligible && spend >= 100.0M -> spend * 0.1M
        | Registered _ -> 0.0M
        | Guest _ -> 0.0M

    spend - discount

let john =
    Registered { Id = "John"; IsEligible = true }

let mary =
    Registered { Id = "Mary"; IsEligible = true }

let richard =
    Registered { Id = "Richard"; IsEligible = false }

let sarah = Guest { Id = "Sarah" }

let assertJohn = calculateTotal john 100.0M = 90.0M
let assertMary = calculateTotal mary 99.0M = 99.0M
let assertRichard = calculateTotal richard 100.0M = 100.0M
let assertSarah = calculateTotal sarah 100.0M = 100.0M
