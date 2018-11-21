
// Boring boilerplate logging

let log p = printfn "expression is %A" p

let loggedWorkflow =
    let x = 42
    log x
    let y = 43
    log y
    let z = x + y
    log z
    z

// let's get rid of the boilerplate

type LoggingBuilder() =
    let log p = printfn "expression is %A" p

    member this.Bind(x, f) =
        log x
        f x

    member this.Return(x) =
        x

let logger = new LoggingBuilder()

// now we can hide the logging within a computation expression
let improvedLoggedWorkflow = 
    logger 
        {
        let! x = 42
        let! y = 43
        let! z = x + y
        return z
        }

// we can also simplify optional (or null) checking
type MaybeBuilder() =
    
    member this.Bind(x, f) =
        match x with
        | None -> None
        | Some a -> f a

    member this.Return(x) =
        Some x

let maybe = new MaybeBuilder()

let divideBy bottom top =   
    if bottom = 0
    then None
    else Some(top/bottom)

let divideByWorkflow init x y z =
    maybe
        {
            let! a = init |> divideBy x
            let! b = a |> divideBy y
            let! c = b |> divideBy z
            return c
        }

// What if we want to try a series of fallbacks until
// one succeeds
type OrElseBuilder() =
    member this.ReturnFrom(x) = x
    member this.Combine(a,b) =
        match a with
        | Some _ -> a
        | None -> b
    member this.Delay(f) = f()

let orElse = new OrElseBuilder()

let map1 = [ ("1","One"); ("2","Two") ] |> Map.ofList
let map2 = [ ("A","Alice"); ("B","Bob") ] |> Map.ofList
let map3 = [ ("CA","California"); ("NY","New York") ] |> Map.ofList

let multiLookup key = orElse {
    return! map1.TryFind key
    return! map2.TryFind key
    return! map3.TryFind key
    }

// And of course! Our favorite workflow. Async
// The use case is so common that they've already built the
// helper into the standard library
open System.Net
let req1 = HttpWebRequest.Create("http://fsharp.org")
let req2 = HttpWebRequest.Create("http://google.com")
let req3 = HttpWebRequest.Create("http://bing.com")

async {
    use! resp1 = req1.AsyncGetResponse()  
    printfn "Downloaded %O" resp1.ResponseUri

    use! resp2 = req2.AsyncGetResponse()  
    printfn "Downloaded %O" resp2.ResponseUri

    use! resp3 = req3.AsyncGetResponse()  
    printfn "Downloaded %O" resp3.ResponseUri

    } |> Async.RunSynchronously