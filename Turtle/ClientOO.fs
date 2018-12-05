namespace Turtle.OO
open Turtle.OO.Service
open Turtle.OO.Api

module Client =
    let log message =
        printfn "%s" message

    let drawTriangle() =
        let turtleDependency = normalSize()
        let api = TurtleApi(turtleDependency)

        api.Exec "Move 100"
        api.Exec "Turn 120"
        api.Exec "Move 100"
        api.Exec "Turn 120"
        api.Exec "Move 100"
        api.Exec "Turn 120"

    let drawPolygon n = 
        let angle = 180.0 - (360.0/float n) 
        let turtleDependency = normalSize()
        let api = TurtleApi(turtleDependency)

        // define a function that draws one side
        let drawOneSide() = 
            api.Exec "Move 100.0"
            api.Exec (sprintf "Turn %f" angle)

        // repeat for all sides
        for i in [1..n] do
            drawOneSide()