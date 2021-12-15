namespace ComputationExpression

[<AutoOpen>]
module Option =

    type OptionBuilder() =
        // Supports let!
        member _.Bind(x, f) = Option.bind f x
        // Supports return
        member _.Return(x) = Some x
        // Supports return!
        member _.ReturnFrom(x) = x

    let option = OptionBuilder()

module OptionDemo = 

    let multiply x y =
        x * y

    let divide x y =
        if y = 0 then None
        else Some (x / y)

    let calculate x y =
        divide x y
        |> function
            | Some v ->  multiply v x |> Some
            | None -> None
        |> function
            | Some t -> divide t y
            | None -> None

    let calculate' x y =
        divide x y
        |> Option.map (fun s -> multiply s x)
        |> Option.bind (fun x -> divide x y)

    let calculate'' x y =
        option {
            let! first = divide x y
            let second = multiply first x
            return! divide second y
        }