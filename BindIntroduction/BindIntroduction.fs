namespace BindIntroduction

module DivideByExplicit =
    
    let divideBy bottom top =
        if bottom = 0
        then None
        else Some(top / bottom)
    
    let divideByWorkflow x y w z =
        let a = x |> divideBy y
        match a with
        | None -> None
        | Some a' ->
            let b = a' |> divideBy w
            match b with
            | None -> None
            | Some b' ->
                let c = b' |> divideBy z
                match c with
                | None -> None
                | Some c' ->
                    Some c'

module DivideByWithBindFunction =

    let divideBy bottom top =
        if bottom = 0
        then None
        else Some(top / bottom)

    let bind (m, f) =
        Option.bind f m

    let return' x = Some x

    let divideByWorkflow x y w z =
        bind (x |> divideBy y, fun a ->
        bind (a |> divideBy w, fun b ->
        bind (b |> divideBy z, fun c ->
            return' c)))

module DivideByWithCompExpr =

    let divideBy bottom top =
        if bottom = 0
        then None
        else Some(top / bottom)

    type MaybeBuilder() =
        member this.Bind(m, f) = Option.bind f m
        member this.Return(x) = Some x

    let maybe = new MaybeBuilder()

    let divideByWorkflow x y w z =
        maybe
            {
            let! a =  x |> divideBy y
            let! b = a |> divideBy w
            let! c = b |> divideBy z
            return c
            }

module DivideByWithBindOperator =

    let divideBy bottom top =
        if bottom = 0
        then None
        else Some(top / bottom)

    let (>>=) m f = Option.bind f m

    let divideByWorkflow x y w z =
        x |> divideBy y
        >>= divideBy w
        >>= divideBy z

        