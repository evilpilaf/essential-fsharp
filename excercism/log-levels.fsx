module LogLevels

let message (logLine: string) =
    let start = (logLine.IndexOf ':') + 1
    logLine.[start..].Trim()

let logLevel (logLine: string) : string =
    let last = (logLine.IndexOf ']') - 1
    logLine.[1..last].ToLower()

let reformat (logLine: string) : string =
    let msg = message logLine
    let lvl = logLevel logLine
    $"{msg} ({lvl})"


reformat "[ERROR]: Stack overflow"
