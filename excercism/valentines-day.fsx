module ValentinesDay

// TODO: please define the 'Approval' discriminated union type
type Approval =
    | Yes
    | No
    | Maybe

// TODO: please define the 'Cuisine' discriminated union type
type Cuisine =
    | Korean
    | Turkish

// TODO: please define the 'Genre' discriminated union type
type Genre =
    | Crime
    | Horror
    | Romance
    | Thriller

// TODO: please define the 'Activity' discriminated union type
type Activity =
    | BoardGame
    | Chill
    | Movie of Genre
    | Restaurant of Cuisine
    | Walk of int

let rateActivity (activity: Activity) : Approval =
    match activity with
    | BoardGame -> No
    | Chill -> No
    | Movie m ->
        match m with
        | Romance -> Yes
        | _ -> No
    | Restaurant r ->
        match r with
        | Korean -> Yes
        | Turkish -> Maybe
    | Walk d ->
        match d with
        | d when d < 3 -> Yes
        | d when d >= 3 && d < 5 -> Maybe
        | _ -> No
