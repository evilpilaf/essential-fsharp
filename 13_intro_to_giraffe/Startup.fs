namespace _13_intro_to_giraffe

open System
open Giraffe
open Giraffe.ViewEngine
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open GiraffeExample.TodoStore

module Handlers =
    open GiraffeExample
    let sayHelloNameHandler name : HttpHandler =
        fun (next:HttpFunc) (ctx:HttpContext) ->
            task {
                let msg = $"Hello {name}, how are you"
                return! json {| Response = msg |} next ctx
            }

    let indexView =
        html [] [
            head [] [
                title [] [ str "Giraffe Example" ]
            ]
            body [] [
                h1 [] [ str "I |> F#" ]
                p [ _class "some-css-class"; _id "someId" ] [
                    str "Hello World from Giraffe Example"
                ]
            ]
        ]

    let apiRoutes =
        choose [
            subRoute "/todo" Todos.todoRoutes
            GET >=> choose [
                route "" >=> json {| Response = "Hello json World!" |}
                routef "/%s" sayHelloNameHandler
            ]
        ]

    let webApp =
        choose [
            GET >=> route "/" >=> htmlView indexView
            subRoute "/api" apiRoutes
            RequestErrors.NOT_FOUND "Not found :("
        ]



type Startup() =

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    member _.ConfigureServices(services: IServiceCollection) =
        services.AddGiraffe()
                .AddSingleton<TodoStore>(TodoStore())
        |> ignore

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member _.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        if env.IsDevelopment() then
            app.UseDeveloperExceptionPage() |> ignore

        app.UseGiraffe Handlers.webApp
