namespace Turtle.OO
open Turtle.OO.Service
open Turtle.OO.Api
open Turtle

module Client =
    let log message =
        printfn "%s" message

    let drawTriangle() =
        let turtle = Turtle(log)
        turtle.Move 100.0
        turtle.Turn 120.0<Degrees>
        turtle.Move 100.0
        turtle.Turn 120.0<Degrees>
        turtle.Move 100.0
        turtle.Turn 120.0<Degrees>

    let drawPolygon n = 
        let angle = 180.0 - (360.0/float n) 
        let api = TurtleApi()

        // define a function that draws one side
        let drawOneSide() = 
            api.Exec "Move 100.0"
            api.Exec (sprintf "Turn %f" angle)

        // repeat for all sides
        for i in [1..n] do
            drawOneSide()