namespace ComputationExpression

module AsyncDemo =

    open System.IO
    open FsToolkit.ErrorHandling.TaskResult

    type FileResult = {
        Name: string
        Length: int
    }

    let getFileInformation path =
        async {
            let! bytes = File.ReadAllBytesAsync(path) |> Async.AwaitTask
            let fileName = Path.GetFileName(path)
            return {Name = fileName; Length = bytes.Length }
        }

    let getFileInformation' path =
        taskResult {
            do! File.Exists(path) |> TaskResult.requireTrue FileDoesNotExist
            let! bytes = File.ReadAllBytesAsync(path) |> TaskResult.ofTask
            let fileName = Path.GetFileName(path)
            return {Name = fileName; Length = bytes.Length }
        }