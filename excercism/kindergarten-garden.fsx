//Instructions
//Given a diagram, determine which plants each child in the kindergarten class is responsible for.
//
//The kindergarten class is learning about growing plants. The teacher thought it would be a good idea to give them actual seeds, plant them in actual dirt, and grow actual plants.
//
//They've chosen to grow grass, clover, radishes, and violets.
//
//To this end, the children have put little cups along the window sills, and planted one type of plant in each cup, choosing randomly from the available types of seeds.
//
//[window][window][window]
//........................ # each dot represents a cup
//........................
//There are 12 children in the class:
//
//Alice, Bob, Charlie, David,
//Eve, Fred, Ginny, Harriet,
//Ileana, Joseph, Kincaid, and Larry.
//Each child gets 4 cups, two on each row. Their teacher assigns cups to the children alphabetically by their names.
//
//The following diagram represents Alice's plants:
//
//[window][window][window]
//VR......................
//RG......................
//In the first row, nearest the windows, she has a violet and a radish. In the second row she has a radish and some grass.
//
//Your program will be given the plants from left-to-right starting with the row nearest the windows. From this, it should be able to determine which plants belong to each student.
//
//For example, if it's told that the garden looks like so:
//
//[window][window][window]
//VRCGVVRVCGGCCGVRGCVCGCGV
//VRCCCGCRRGVCGCRVVCVGCGCV
//Then if asked for Alice's plants, it should provide:
//
//Violets, radishes, violets, radishes
//While asking for Bob's plants would yield:
//
//Clover, grass, clover, clover


module KindergartenGarden

open System

type Plant =
    | Grass = 'G'
    | Clover = 'C'
    | Radishes = 'R'
    | Violets = 'V'
    
let students = [
    "Alice"  
    "Bob"
    "Charlie"
    "David"
    "Eve"
    "Fred"
    "Ginny"
    "Harriet"
    "Ileana"
    "Joseph"
    "Kincaid"
    "Larry"]

let plantFromCode code =
    match code with
    | 'G' -> Plant.Grass
    | 'C' -> Plant.Clover
    | 'R' -> Plant.Radishes
    | 'V' -> Plant.Violets
    | _ -> failwith $"{code} is an unknown plant"
    
let plants (diagram: string) student =
    let studentIdx =
        students
        |> List.findIndexBack (fun x -> x.Equals(student, StringComparison.OrdinalIgnoreCase))
        
    let windowSills = diagram.Split([|'\n'|])    

    windowSills
    |> Array.map (fun windowSill ->
                  let plants = windowSill.ToCharArray ()
                  [| plants.[studentIdx] |> plantFromCode; plants.[studentIdx + 1]|> plantFromCode|] )
    |> Array.fold (fun x -> )
    

    
let student = "Alice"
let diagram = "RC\nGG"

plants diagram student