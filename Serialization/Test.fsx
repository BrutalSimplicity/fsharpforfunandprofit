#r "refs/Newtonsoft.Json.dll"
#load "Helpers.fs"
#load "Domain.fs"
#load "Dto.fs"
#load "Json.fs"

open System
open Serialization
open Serialization.Domain

let person = 
    {First = String50 "Alex"; Last = String50 "Adams"; Birthdate = Birthdate (DateTime(1980,1,1))}

Person.jsonFromDomain person

let jsonPerson = """{ "First": "Alex", "Last": "Adams", "Birthdate": "1980-01-01T00:00:00" }"""

// call the deserialization pipeline
Person.jsonToDomain jsonPerson
let jsonPersonWithErrors = """{ "First": "", "Last": "Adams", "Birthdate": "1776-01-01T00:00:00" }"""

Person.jsonToDomain jsonPersonWithErrors