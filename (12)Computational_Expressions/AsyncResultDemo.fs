namespace ComputationExpression

module AsyncResultDemo =

    open System
    open FsToolkit.ErrorHandling

    type AuthError = 
        | UserBannedOrSuspended

    type TokenError =
        | BadThingHappened of string

    type LoginError =
        | InvalidUser
        | InvalidPwd
        | Unauthorized of AuthError
        | TokenErr of TokenError

    type AuthToken = AuthToken of Guid

    type UserStatus = 
        | Active
        | Suspended
        | Banned

    type User = {
        Name: string
        Password: string
        Status: UserStatus
    }

    [<Literal>]
    let ValidPassword = "password"
    [<Literal>]
    let ValidUser = "isvalid"
    [<Literal>]
    let SuspendedUser = "issuspended"
    [<Literal>]
    let BannedUser = "isbanned"
    [<Literal>]
    let BadLuckUser = "hasbadluck"
    [<Literal>]
    let AuthErrorMessage = "Earth's core stopped spinning"

    let tryGetUser (username: string) : Async<User option> =
        async {
            let user = { Name = username; Password = ValidPassword; Status = Active }
            return
                match username with
                | ValidUser -> Some user
                | SuspendedUser -> Some { user with Status = Banned }
                | BannedUser -> Some { user with Status = Suspended }
                | BadLuckUser -> Some user
                | _ -> None
        }

    let isPwdValid (password: string) (user: User) : bool =
        password = user.Password

    let authorize (user: User) : Async<Result<unit, AuthError>> =
        async {
            return
                match user.Status with
                | Active -> Ok ()
                | _ -> UserBannedOrSuspended |> Error
        }

    let createAuthToken (user: User) : Result<AuthToken, TokenError> =
        try
            if user.Name = BadLuckUser then failwith AuthErrorMessage
            else Guid.NewGuid() |> AuthToken |> Ok
        with
        | ex -> ex.Message |> BadThingHappened |> Error

    let login (username: string) (password: string) : Async<Result<AuthToken, LoginError>> =
        asyncResult {
            let! user = username |> tryGetUser |> AsyncResult.requireSome InvalidUser
            do! user |> isPwdValid password |> Result.requireTrue InvalidPwd
            do! user |> authorize |> AsyncResult.mapError Unauthorized
            return! user |> createAuthToken |> Result.mapError TokenErr
        }