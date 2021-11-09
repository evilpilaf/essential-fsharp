// Instructions
// Given an age in seconds, calculate how old someone would be on:

// Mercury: orbital period 0.2408467 Earth years
// Venus: orbital period 0.61519726 Earth years
// Earth: orbital period 1.0 Earth years, 365.25 Earth days, or 31557600 seconds
// Mars: orbital period 1.8808158 Earth years
// Jupiter: orbital period 11.862615 Earth years
// Saturn: orbital period 29.447498 Earth years
// Uranus: orbital period 84.016846 Earth years
// Neptune: orbital period 164.79132 Earth years
// So if you were told someone were 1,000,000,000 seconds old, you should be able to say that they're 31.69 Earth-years old.

// If you're wondering why Pluto didn't make the cut, go watch this youtube video.

// Try to focus on minimizing the amount of code duplication. If you find yourself doing a lot of copy/paste take a step back and think about how the code can be refactored
// Pattern matching is more idiomatic than using dictionaries to translate values

module SpaceAge

let earthOrbitalPeriodInSeconds = 31557600.0

type Planet =
    | Mercury
    | Venus
    | Earth
    | Mars
    | Jupiter
    | Saturn
    | Uranus
    | Neptune

let earthSecondsToYears seconds : float =
    seconds / earthOrbitalPeriodInSeconds |> float

let orbitalPeriod planet =
    match planet with
    | Mercury -> 0.2408467
    | Venus -> 0.61519726
    | Earth -> 1.0
    | Mars -> 1.8808158
    | Jupiter -> 11.862615
    | Saturn -> 29.447498
    | Uranus -> 84.016846
    | Neptune -> 164.79132


let age (planet: Planet) (seconds: int64) : float =
    let period = planet |> orbitalPeriod
    let secodsInEarthYears = float seconds |> earthSecondsToYears
    secodsInEarthYears / period


let age' (planet: Planet) (seconds: int64) : float =
    let secodsInEarthYears = float seconds |> earthSecondsToYears
    planet |> orbitalPeriod |> (/) secodsInEarthYears


let tst = age Earth 1_000_000_000L = 31.69
