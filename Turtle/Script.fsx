#load "LibraryOO.fs"
#load "ClientOO.fs"
open Turtle.OO

Client.drawTriangle()

let squares =
    seq {
        for i in 1..3 -> i * i
    }

let cubes =
    seq {
        for i in 1..3 -> i * i * i
    }

let squaresAndCubes =
    seq {
        yield! squares
        yield! cubes
    }

printfn "%A" squaresAndCubes // Prints - 1; 4; 9; 1; 8; 27

let (|Even|Odd|) input = if input % 2 = 0 then Even else Odd
let TestNumber input =
   match input with
   | Even -> printfn "%d is even" input
   | Odd -> printfn "%d is odd" input

let count ls =
    ls |> List.map List.length