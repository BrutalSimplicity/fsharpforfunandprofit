
type DbResult<'a> =
    | Success of 'a
    | Error of string

let getCustomerId name =
    if (name = "") 
    then Error "getCustomerId failed"
    else Success "Cust42"

let getLastOrderForCustomer custId =
    if (custId = "") 
    then Error "getLastOrderForCustomer failed"
    else Success "Order123"

let getLastProductForOrder orderId =
    if (orderId  = "") 
    then Error "getLastProductForOrder failed"
    else Success "Product456"

type DbResultBuilder() =
    member this.Bind(m, f) =
        match m with
        | Error _ -> m
        | Success a ->
            printfn "\tSuccessful: %s" a
            f a

     member this.Return(x) =
        Success x

let dbresult = new DbResultBuilder()

let product' = 
    dbresult {
        let! custId = getCustomerId "Alice"
        let! orderId = getLastOrderForCustomer custId
        let! productId = getLastProductForOrder orderId 
        printfn "Product is %s" productId
        return productId
        }
printfn "%A" product'

type ListWorkflowBuilder() =
    member this.Bind(list, f) =
        list |> List.collect f

    member this.Return(x) =
        [x]

    member this.For(list, f) =
        this.Bind(list, f)

let listWorkflow = new ListWorkflowBuilder()

let added = 
    listWorkflow {
        let! i = [1;2;3]
        let! j = [10;11;12]
        return i+j
        }
printfn "added=%A" added

let multiplied = 
    listWorkflow {
        for i in [1;2;3] do
        for j in [10;11;12] do
        return i*j
        }
printfn "multiplied=%A" multiplied

