namespace Serialization

open System
open System.Security.Permissions
open System.Runtime.CompilerServices

module Domain = // our domain-driven types

    /// constrained to be not null and at most 50 chars
    type String50 = internal String50 of string 

    module String50 =       // functions for String50
        let create fieldname str =
            if String.IsNullOrEmpty(str) then
                Error (fieldname + " must be non-empty")
            else if str.Length > 50 then
                Error (fieldname + " must be less than 50 chars")
            else
                Ok (String50 str)

        let value (String50 str) = str

    /// constrained to be bigger than 1/1/1900 and less than today's date
    type Birthdate = internal Birthdate of DateTime 

    module Birthdate =           // functions for Birthdate
        let create fieldname aDateTime =
            if aDateTime < DateTime(1910, 1, 1) then
                Error (fieldname + " must be older than 1/1/1910")
            else if aDateTime > DateTime.Today then
                Error (fieldname + " cannot be set to the future")
            else
                Ok(Birthdate aDateTime)

        let value (Birthdate birthdate) = birthdate

    /// Domain type
    type Person = {
        First: String50
        Last: String50
        Birthdate : Birthdate
    }

    let createPerson first last birthdate =
        {First = String50 first; Last = String50 last; Birthdate = Birthdate birthdate}
