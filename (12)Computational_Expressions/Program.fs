open ComputationExpression.AsyncDemo
open ComputationExpression.AsyncResultDemoTests
open ComputationExpression.OptionDemo

open System.IO

[<EntryPoint>]
let main argv =
    calculate 8 0 |> printfn "calculate 8 0 = %A"
    calculate' 8 0 |> printfn "calculate' 8 0 = %A"
    calculate'' 8 0 |> printfn "calculate'' 8 0 = %A"
    calculate 8 2 |> printfn "calculate 8 2 = %A"
    calculate' 8 2 |> printfn "calculate' 8 2 = %A"
    calculate'' 8 2 |> printfn "calculate'' 8 2 = %A"
    Path.Combine (__SOURCE_DIRECTORY__, "resources", "customers.csv")
    |> getFileInformation
    |> Async.RunSynchronously
    |> printfn "%A"

    printfn "Success: %b" success
    printfn "BadPassword: %b" badPassword
    printfn "InvalidUser: %b" invalidUser
    printfn "IsSuspended: %b" isSuspended
    printfn "IsBanned: %b" isBanned
    printfn "HasBadLuck: %b" hasBadLuck
    0