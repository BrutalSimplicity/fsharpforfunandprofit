namespace Turtle.OO
open System
open Turtle

module Service =

    type ITurtle =
        abstract Move: Distance -> unit
        abstract Turn: Angle -> unit
        abstract PenUp: unit -> unit
        abstract PenDown: unit -> unit
        abstract SetColor: PenColor -> unit

    type Turtle(log) =
        let mutable currentPosition = initialPosition
        let mutable currentAngle = 0.0<Degrees>
        let mutable currentColor = initialColor
        let mutable currentPenState = initialPenState

        member this.Move(distance) =
            log (sprintf "Move %0.1f" distance)

            let newPosition = calcNewPosition distance currentAngle currentPosition

            if currentPenState = Down then 
                dummyDrawLine log currentPosition newPosition currentColor

            currentPosition <- newPosition

        member this.Turn(angle) =
            log (sprintf "Turn %0.1f" angle)
            let newAngle = (currentAngle + angle) % 360.0<Degrees>
            currentAngle <- newAngle

        member this.PenUp() =
            log "Pen up"
            currentPenState <- Up

        member this.PenDown() =
            log "Pen down"
            currentPenState <- Down

        member this.SetColor(color) =
            log (sprintf "SetColor %A" color)
            currentColor <- color


    let normalSize() =
        let turtle = Turtle(log)

        {new ITurtle with
            member this.Move dist = turtle.Move dist
            member this.Turn angle = turtle.Turn angle
            member this.PenDown() = turtle.PenDown()
            member this.PenUp() = turtle.PenUp()
            member this.SetColor color = turtle.SetColor color}

    let halfSize() =
        let turtle = normalSize()


        {new ITurtle with
            member this.Move dist = turtle.Move (dist/2.0)
            member this.Turn angle = turtle.Turn angle
            member this.PenDown() = turtle.PenDown()
            member this.PenUp() = turtle.PenUp()
            member this.SetColor color = turtle.SetColor color}

