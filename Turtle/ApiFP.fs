namespace Turtle.FP
open Turtle.FP.Turtle
open Turtle.Common
open Turtle.FP.Agent

module Api =

    type ErrorMessage =
        | InvalidDistance of string
        | InvalidAngle of string
        | InvalidColor of string
        | InvalidCommand of string


    type TurtleApi() =

        let log message = (printf "%s" message)

        let (moveLog, turnLog, penUpLog, penDownLog, setColorLog) =
            (move log, turn log, penUp log, penDown log, setColor log)

        let mutable state = initialTurtleState

        let updateState newState =
            state <- newState

        let validateDistance distanceStr =
            try
                Ok (float distanceStr)
            with
            | _ ->
                Error (InvalidDistance distanceStr)
        
        let validateAngle angleStr =
            try
                let degrees = (float angleStr) * 1.0<Degrees>
                Ok degrees
            with
            | _ -> Error (InvalidAngle angleStr)

        let validateColor colorStr =
            match colorStr with
            | "Black" -> Ok Black
            | "Red" -> Ok Red
            | "Blue" -> Ok Blue
            | _ -> Error (InvalidColor colorStr)
        
        let trimString (s: string) = s.Trim()

        let lift2r f a b =
            match a, b with
            | Ok a, Ok b -> Ok (f a b)
            | Error a, _ -> Error a
            | _, Error b -> Error b

        member this.Exec (commandStr: string) =
            let tokens = 
                commandStr.Split(' ')
                |> List.ofArray
                |> List.map trimString
            
            let stateR = Result.Ok state

            let turtleAgent = TurtleAgent()

            let result =
                match tokens with
                | ["Move"; distanceStr] -> result {
                    let! distance = validateDistance distanceStr
                    let command = Move distance
                    turtleAgent.Post command
                    }
                | ["Turn"; angleStr] -> result {
                    let! angle = validateAngle angleStr
                    let command = Turn angle
                    turtleAgent.Post command
                    }
                | ["Pen"; "Up"] -> result {
                    let command = PenUp
                    turtleAgent.Post command
                    }
                | ["Pen"; "Down"] -> result {
                    let command = PenDown
                    turtleAgent.Post command
                    }
                | ["SetColor"; colorStr] -> result {
                    let! color = validateColor colorStr
                    let command = SetColor color
                    turtleAgent.Post command
                    }
                | _ ->
                    Error (InvalidCommand commandStr)

            Result.map updateState newStateR

        