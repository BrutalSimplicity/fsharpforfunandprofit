namespace Serialization
open System
open Helpers
open Dto

module Json =
    open Newtonsoft.Json

    let serialize obj =
        JsonConvert.SerializeObject obj

    let deserialize<'a> str =
        try
            JsonConvert.DeserializeObject<'a> str
            |> Result.Ok
        with
            | ex -> Result.Error ex

module Person =

    type DtoError =
    | ValidationError of string
    | DeserializationException of Exception

    let jsonFromDomain(person: Domain.Person) =
        person
        |> Dto.Person.fromDomain
        |> Json.serialize

    let jsonToDomain json : Result<Domain.Person, DtoError> =
        result {
            let! deserializedValue =
                json
                |> Json.deserialize
                |> Result.mapError DeserializationException

            let! domainValue =
                deserializedValue
                |> Dto.Person.toDomain
                |> Result.mapError ValidationError

            return domainValue
            }
