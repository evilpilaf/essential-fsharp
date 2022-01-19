namespace GiraffeExample

open System
open System.Collections.Generic
open Microsoft.AspNetCore.Http
open Giraffe
open FSharp.Control.Tasks
open GiraffeExample.TodoStore

module Todos =

    let viewTodosHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let store = ctx.GetService<TodoStore>()
                let todos = store.GetAll()
                return! json todos next ctx
            }

    let createTodoHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
             task {
                let! newTodo = ctx.BindJsonAsync<NewTodo>()
                let store = ctx.GetService<TodoStore>()
                let todo = {
                    Id = Guid.NewGuid()
                    Description = newTodo.Description
                    Created = DateTime.UtcNow
                    IsCompleted = false }
                let created = store.Create(todo)
                return! Successful.CREATED todo next ctx
            }

    let updateTodoHandler =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let! todo = ctx.BindJsonAsync<Todo>()
                let store = ctx.GetService<TodoStore>()
                let created = store.Update(todo)
                return! json created next ctx
            }

    let viewTodoHandler (id: Guid) =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let store = ctx.GetService<TodoStore>()
                let todo = store.Get(id)
                return! json todo next ctx
            }

    let deleteTodoHandler (id:Guid) =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let store = ctx.GetService<TodoStore>()
                let existing = store.Get(id)
                let deleted = store.Delete(KeyValuePair<TodoId, Todo>(id, existing))
                return! json deleted next ctx
            }


    let todoRoutes : HttpHandler =
        choose [
            route "" >=> choose [
                GET >=> viewTodosHandler
                PUT >=> createTodoHandler
                POST >=> updateTodoHandler
            ]
            GET >=> routef "/%O" viewTodoHandler
            DELETE >=> routef "/%O"  deleteTodoHandler
            // routef "/%O" choose [
            //     GET >=> viewTodoHandler
            //     DELETE >=> deleteTodoHandler
            // ]
            // GET >=> choose [
            //     route "" >=> viewTodosHandler
            // ]
            // PUT >=> route "" >=> createTodoHandler
            // POST >=> route "" >=> updateTodoHandler
        ]