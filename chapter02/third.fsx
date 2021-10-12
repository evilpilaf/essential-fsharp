type LogLevel =
    | Error
    | Warning
    | Info


let log (level: LogLevel) message =
    printfn "[%A]: %s" level message
    ()

let logError = log Error

log Error "Curried Function" |> ignore

logError "Partially aplied function" |> ignore
