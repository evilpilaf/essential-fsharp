module CarsAssemble

let productionRatePerHour (speed: int) : float =
    let successRate =
        if speed = 0 then 0.0
        elif speed > 0 && speed <= 4 then 1.0
        elif speed >= 5 && speed <= 8 then 0.9
        elif speed = 9 then 0.8
        else 0.77

    float speed * float 221 * successRate

let workingItemsPerMinute (speed: int) : int =
    (productionRatePerHour speed) / 60.0 |> int

// module Leap

let leapYear (year: int) : bool =
    if year % 4 = 0 then
        if year % 100 = 0 then
            year % 400 = 0
        else
            false
    else
        false
