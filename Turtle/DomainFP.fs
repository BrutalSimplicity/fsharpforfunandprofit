namespace Turtle.FP
open Turtle.Common

module Turtle =

    type TurtleState = {
        position: Position
        angle: float<Degrees>
        color: PenColor
        penState: PenState
        }

    type TurtleFunctions = {
        move: Distance -> TurtleState -> TurtleState
        turn: Angle -> TurtleState -> TurtleState
        penUp: TurtleState -> TurtleState
        penDown: TurtleState -> TurtleState
        setColor: PenColor -> TurtleState -> TurtleState
        }

    let initialTurtleState = {
        position = initialPosition
        angle = 0.0<Degrees>
        color = initialColor
        penState = initialPenState
        }

    let move log distance state =
        log (sprintf "Move %0.1f" distance)
        let newPosition = calcNewPosition distance state.angle state.position

        if state.penState = Down then
            dummyDrawLine log state.position newPosition state.color
        
        {state with position = newPosition}

    let turn log angle state =
        log (sprintf "Turn %0.1f" angle)
        let newAngle = (state.angle + angle) % 360.0<Degrees>
        {state with angle = newAngle}

    let penUp log state =
        log "Pen up"
        {state with penState = Up}

    let penDown log state =
        log "Pen down"
        {state with penState = Down}

    let setColor log color state =
        log (sprintf "SetColor %A" color)
        {state with color = color}

    let normalSize() =
        {
            move = move log
            turn = turn log
            penDown = penDown log
            penUp = penUp log
            setColor = setColor log
        }
    
    let halfSize() =
        let normalSize = normalSize()
        {normalSize with
            move = fun dist -> normalSize.move (dist/2.0)}
