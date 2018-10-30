#load "Chaining.fs"
open Scratchpad

Chaining.appendHeadToTail [1;2;3;4];;

type Person = {
    first: string
    last: string
}

let p = {first = "Alice"; last = "Jones"}