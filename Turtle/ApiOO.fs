namespace Turtle.OO
open Turtle.OO.Service
open Turtle

exception TurtleApiException of string

module Api =
    let validateDistance distanceStr =
        try
            float distanceStr
        with
        | ex ->
            let msg = sprintf "Invalid distance '%s' [%s]" distanceStr ex.Message
            raise (TurtleApiException msg)

    let validateAngle angleStr =
        try
            (float angleStr) * 1.0<Degrees>
        with
        | ex ->
            let msg = sprintf "Invalid angle '%s [%s]" angleStr ex.Message
            raise (TurtleApiException msg)
    
    let validateColor colorStr =
        match colorStr with
            | "Black" -> Black
            | "Blue" -> Blue
            | "Red" -> Red
            | _ ->
                let msg = sprintf "Color '%s' is not recognized" colorStr
                raise (TurtleApiException msg)

    type TurtleApi(turtle: ITurtle) =

        let trimString (s: string) = s.Trim()

        member this.Exec (commandStr: string) =
            let tokens = 
                commandStr.Split(' ')
                |> List.ofArray
                |> List.map trimString

            match tokens with
            | [ "Move"; distanceStr ] ->
                let distance = validateDistance distanceStr
                turtle.Move distance
            | [ "Turn"; angleStr] ->
                let angle = validateAngle angleStr
                turtle.Turn angle
            | [ "Pen"; "Up" ] ->
                turtle.PenDown()
            | [ "Pen"; "Down" ] ->
                turtle.PenUp()
            | [ "SetColor"; colorStr ] ->
                let color = validateColor colorStr
                turtle.SetColor color
            | _ ->
                let msg = sprintf "Instruction '%s' is not recognized" commandStr
                raise (TurtleApiException msg)
        