namespace Turtle
open System

[<AutoOpenAttribute>]
module Common =
    type [<Measure>] Degrees

    type Angle = float<Degrees>

    type PenState = Up | Down

    type PenColor = Black | Red | Blue

    type Position = {x: float; y: float}

    type Distance = float

    let round2 (flt: float) = round flt

    let calcNewPosition (distance: Distance) (angle: Angle) currentPos =
        let angleInRads = angle * (Math.PI / 180.0) * 1.0<1/Degrees>
        let x0 = currentPos.x
        let y0 = currentPos.y

        let x1 = x0 + (distance * cos angleInRads)
        let y1 = y0 + (distance * sin angleInRads)

        {x = round2 x1; y = round2 y1}

    let initialPosition, initialColor, initialPenState =
        {x = 0.0; y = 0.0}, Black, Down
        
    let dummyDrawLine log oldPos newPos color =
        log (sprintf "...Draw line from (%0.1f,%0.1f) to (%0.1f,%0.1f) using %A" oldPos.x oldPos.y newPos.x newPos.y color)

    type ResultBuilder() =
        
        member this.Bind(x, f) =
            match x with
            | Error err -> Error err
            | Ok a -> f a

        member this.Result(x) =
            Ok x

    let result = ResultBuilder()


