namespace Serialization

open System
open System.Security.Permissions

module Domain = // our domain-driven types

    /// constrained to be not null and at most 50 chars
    type String50 = private String50 of string 

    module String50 =       // functions for String50
        let create str =
            if String.IsNullOrEmpty(str) then
                None
            else if str.Length > 50 then
                None
            else
                Some(String50 str)

        let value (String50 str) = str

    /// constrained to be bigger than 1/1/1900 and less than today's date
    type Birthdate = private Birthdate of DateTime 

    module Birthdate =           // functions for Birthdate
        let create aDateTime =
            if aDateTime < DateTime(1990, 1, 1) then
                None
            else if aDateTime > DateTime.Today then
                None
            else
                Some(Birthdate aDateTime)

        let value (Birthdate birthdate) = birthdate

    /// Domain type
    type Person = {
        First: String50
        Last: String50
        Birthdate : Birthdate
    }
