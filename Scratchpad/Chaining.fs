namespace Scratchpad

module Chaining =
    let appendHeadToTail xs = xs |> List.append <| [List.head xs]
