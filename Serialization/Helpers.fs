namespace Serialization

module Helpers =
    type ResultBuilder() =
        
        member this.Bind(x, f) =
            match x with
            | Error e -> Error e
            | Ok a -> f a

        member this.Return(x) =
            Ok x

        member this.ReturnFrom(x) =
            x

    let result = new ResultBuilder()
