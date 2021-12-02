let rec fact n =
    match n with
    | 0 -> 1
    | 1 -> 1
    | n -> n * fact (n - 1)


fact 5


let fact n =
    let rec loop n acc =
        match n with
        | 0 -> acc
        | 1 -> acc
        | _ -> loop (n - 1) (acc * n)
    loop n 1
