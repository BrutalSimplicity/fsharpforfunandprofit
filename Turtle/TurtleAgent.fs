namespace Turtle.FP
open Turtle

module Agent =

    type TurtleCommand =
        | Move of Distance
        | Turn of Angle
        | PenUp
        | PenDown
        | SetColor of PenColor

    type TurtleAgent() =
        let log message =
            printfn "%s" message

        let (move, turn, penDown, penUp, setColor) =
            (move log, turn log, penDown log, penUp log, setColor log)

        let mailboxProc = MailboxProcessor.Start(fun inbox ->
            let rec loop turtleState = async {
                let! command = inbox.Receive()

                let newState =
                    match command with
                    | Move distance ->
                        move distance turtleState
                    | Turn angle ->
                        turn angle turtleState
                    | PenUp ->
                        penUp turtleState
                    | PenDown ->
                        penDown turtleState
                    | SetColor color ->
                        setColor color turtleState

                return! loop newState
            }
            loop initialTurtleState
        )

        member this.Post(command) =
            mailboxProc.Post command