module QueenAttack

let isInBoard v = [ 0 .. 7 ] |> List.contains v


let create (position: int * int) =
    isInBoard (fst position)
    && isInBoard (snd position)


let (|IsInSameRow|_|) ((r1, _), (r2, _)) = if r1 = r2 then Some() else None

let (|IsInSameColumn|_|) ((_, c1), (_, c2)) = if c1 = c2 then Some() else None

let (|InDiagonal|_|) ((r1, c1), (r2, c2)) =
    if (abs (r1 - r2) = abs (c1 - c2)) then
        Some()
    else
        None

let canAttack (queen1: int * int) (queen2: int * int) =
    match queen1, queen2 with
    | IsInSameRow -> true
    | IsInSameColumn -> true
    | InDiagonal -> true
    | _ -> false

let canAttack'' = function
    | IsInSameRow -> true
    | IsInSameColumn -> true
    | InDiagonal -> true
    | _ -> false

let canAttack' (queen1: int * int) (queen2: int * int) =
    (queen1, queen2)
    |> canAttack''
