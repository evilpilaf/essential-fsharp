// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.IO

type DataReader = string -> Result<string seq, exn>

type Customer =
    { CustomerId: string
      Email: string
      IsEligible: string
      IsRegistered: string
      DateRegistered: string
      Discount: string }

let parseLine (line: string) : Customer option =
    match line.Split('|') with
    | [| customerId; email; eligible; registered; dateRegistered; discount |] ->
        Some
            { CustomerId = customerId
              Email = email
              IsEligible = eligible
              IsRegistered = registered
              DateRegistered = dateRegistered
              Discount = discount }
    | _ -> None

let parse (data: string seq) =
    data
    |> Seq.skip 1 // Ignore the header
    |> Seq.map parseLine
    |> Seq.choose id

let readFile: DataReader =
    fun path ->
        try
            seq {
                use reader = new StreamReader(File.OpenRead(path))

                while not reader.EndOfStream do
                    yield reader.ReadLine()
            }
            |> Ok
        with
        | ex -> Error ex

let readFileLogger logger (reader: DataReader) : DataReader =
    logger "blah"
    reader

let output data =
    data |> Seq.iter (fun x -> printfn "%A" x)

let import (dataReader: DataReader) path =
    match path |> dataReader with
    | Ok data -> data |> parse |> output
    | Error ex -> printfn "Error: %A" ex.Message

let importWithFileReader = import readFile

[<EntryPoint>]
let main argv =
    Path.Combine(__SOURCE_DIRECTORY__, "resources", "customer.csv")
    |> importWithFileReader

    0 // return an integer exit code


// let fakeDataReader: DataReader =
//     fun _ ->
//         seq {
//             "CustomerId|Email|Eligible|Registered|DateRegistered|Discount"
//             "John|john@test.com|1|1|2015-01-23|0.1"
//             "Mary|mary@test.com|1|1|2018-12-12|0.1"
//             "Richard|richard@nottest.com|0|1|2016-03-23|0.0"
//             "Sarah||0|0||"
//         }
//         |> Ok

// import fakeDataReader "_"
