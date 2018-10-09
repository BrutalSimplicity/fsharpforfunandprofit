namespace Serialization.Json
open Serialization

module Json =
    open Newtonsoft.Json

    let serialize obj =
        JsonConvert.SerializeObject obj

    let deserialize<'a> str =
        try
            Result.Ok (JsonConvert.DeserializeObject<'a> str)
        with
            | ex -> ex |> Result.Error

module Person =

    let jsonFromDomain(person: Domain.Person) =
        person
        |> Dto.Person.fromDomain
        |> Json.serialize