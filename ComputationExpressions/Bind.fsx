open System

// Conventional operator for `Bind`
let (>>=) m f =
    match m with
    | None -> None
    | Some a -> f a

let divideBy top bottom =
    if (bottom = 0)
    then None
    else Some(top/bottom)

let divideByWorkflow x y w z =
    x |> divideBy y >>= divideBy w >>= divideBy z

let strToInt str =
    match Int32.TryParse str with
    | (true, int) -> Some int
    | _ -> None

let strAdd str i =
    match strToInt str with
    | None -> None
    | Some x -> Some (x + i)
    
type MyWorkflowBuilder() =
    member this.Bind(x, f) = Option.bind f x

    member this.Return(x) = Some(x)

let yourWorkflow = new MyWorkflowBuilder()
        

let stringAddWorkflow x y z =
    yourWorkflow
        {
        let! a = strToInt x
        let! b = strToInt y
        let! c = strToInt z
        return a + b + c
        }

let good = stringAddWorkflow "10" "20" "30"
let bad = stringAddWorkflow "10" "twenty" "30"

let good' = strToInt "1" >>= strAdd "2" >>= strAdd "3"
let bad' = strToInt "1" >>= strAdd "xyz" >>= strAdd "3"