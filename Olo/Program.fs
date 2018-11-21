open FSharp.Data

type Pizza = JsonProvider<"pizzas.json", SampleIsList = true>

type ToppingFrequencyPair = { Toppings: string[]; Frequency: int }

let getToppingFrequencyPair grouping =
    { Toppings = fst grouping; Frequency = snd grouping |> Array.length }

[<EntryPoint>]
let main argv =
    let bestToppings = 
        Pizza.GetSamples()
        |> Array.groupBy (fun x -> x.Toppings |> Array.sort)
        |> Array.map getToppingFrequencyPair
        |> Array.sortByDescending (fun x -> x.Frequency)
        |> Array.take 20

    bestToppings |> Array.map (fun x -> printfn "%A" x) |> ignore
    0 // return an integer exit code
