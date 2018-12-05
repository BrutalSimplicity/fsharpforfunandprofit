open FSharp.Data

type Pizza = JsonProvider<"pizzas.json">

type ToppingFrequencyPair = { Toppings: string[]; Frequency: int }

let getToppingFrequencyPair grouping =
    { Toppings = fst grouping; Frequency = snd grouping |> Array.length }

[<EntryPoint>]
let main argv =
    let bestToppings = 
        Pizza.Load "http://files.olo.com/pizzas.json"
        |> Array.groupBy (fun x -> x.Toppings |> Array.sort)
        |> Array.map getToppingFrequencyPair
        |> Array.sortByDescending (fun x -> x.Frequency)
        |> Array.take 20

    bestToppings |> Array.iter (printfn "%A")
    0 // return an integer exit code
