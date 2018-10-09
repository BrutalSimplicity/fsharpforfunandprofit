namespace Serialization

open System
open Domain

module Dto =
    type Person = {
        First: string
        Last: string
        Birthdate: DateTime
    }

    let bindOption f xOpt =
        match xOpt with
        | Some x -> f x |> Some
        | None -> None

    module Person =

        let fromDomain(person: Domain.Person): Person =
            let first = person.First |> String50.value
            let last = person.Last |> String50.value
            let birthdate = person.Birthdate |> Birthdate.value

            {First = first; Last = last; Birthdate = birthdate}
