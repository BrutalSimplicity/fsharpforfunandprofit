// Learn more about F# at http://fsharp.org
#r "refs/Newtonsoft.Json.dll"
#r "/usr/local/share/dotnet/sdk/NuGetFallbackFolder/microsoft.netcore.app/2.1.0/ref/netcoreapp2.1/netstandard.dll"
open System
open Newtonsoft.Json
open System.IO

type Toppings =
    Items of string list

type Pizza =
    Toppings of Toppings

let deserialize<'a> str =
    JsonConvert.DeserializeObject<'a>(str)

let main argv =
    let pizzas = deserialize<Pizza[]> (File.ReadAllText "pizza.json")
    printfn "%A" pizzas
    0 // return an integer exit code
