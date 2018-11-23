open System

let strToInt str =
    match Int32.TryParse(str) with
    | (true, int) -> Some(int)
    | _ -> None

type YourWorkflowBuilder() =
    member this.Bind(m, f) = Option.bind f m
    member this.Return(x) = Some x

let yourWorkflow = new YourWorkflowBuilder()

let stringAddWordflow x y z =
    yourWorkflow
        {
        let! a = strToInt x
        let! b = strToInt y
        let! c = strToInt z
        return a + b + c
        }

