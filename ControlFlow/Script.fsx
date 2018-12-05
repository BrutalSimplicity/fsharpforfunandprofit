
// bad
let f x =
    if x = 1
    then "a"
    else "b"

// still sucks
let f' x =
    match x = 1 with
    | true -> "a"
    | false -> "b"

// better
let f'' x =
    match x with
    | 1 -> "a"
    | _ -> "b"

// Part of the reason why direct matching is better is that the equality 
// test throws away useful information that you often need to retrieve again.

let f1 list =
    if List.isEmpty list
    then printfn "is empty"
    else printfn "first element is %s" list.Head

let f1' list =
    match list with
    | [] -> printfn "is empty"
    | x::_ -> printfn "first element is %s" x

// If the boolean test is complicated, it can still be done with match by
// using extra “when” clauses (called “guards”). 

// bad
let f2 list = 
    if List.isEmpty list
        then printfn "is empty" 
        elif (List.head list) > 0
            then printfn "first element is > 0" 
            else printfn "first element is <= 0" 

// much better
let f2' list = 
    match list with
    | [] -> printfn "is empty" 
    | x::_ when x > 0 -> printfn "first element is > 0" 
    | x::_ -> printfn "first element is <= 0" 


// but there are still some cases where it's ok. one-liners mostly

let posNeg x = if x > 0 then "+" elif x < 0 then "-" else "0"
[-5..5] |> List.map posNeg

// bad
for i = 1 to 10 do
   printf "%i" i

// much better
[1..10] |> List.iter (printf "%i")

// bad
let sum list = 
    let mutable total = 0    // uh-oh -- mutable value 
    for e in list do
        total <- total + e   // update the mutable value
    total                    // return the total

// much better
let sum' list = List.reduce (+) list

// bad
let printRandomNumbersUntilMatched matchValue maxValue =
  let mutable continueLooping = true  // another mutable value
  let randomNumberGenerator = new System.Random()
  while continueLooping do
    // Generate a random number between 1 and maxValue.
    let rand = randomNumberGenerator.Next(maxValue)
    printf "%d " rand
    if rand = matchValue then 
       printfn "\nFound a %d!" matchValue
       continueLooping <- false

// much better
let printRandomNumbersUntilMatched' matchValue maxValue =
  let randomNumberGenerator = new System.Random()
  let sequenceGenerator _ = randomNumberGenerator.Next(maxValue)
  let isNotMatch = (<>) matchValue

  //create and process the sequence of rands
  Seq.initInfinite sequenceGenerator 
    |> Seq.takeWhile isNotMatch
    |> Seq.iter (printf "%d ")

  // done
  printfn "\nFound a %d!" matchValue

// one-liners are ok
let myList = [for x in 0..100 do if x*x < 100 then yield x ]
