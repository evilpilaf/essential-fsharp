module BookingUpForBeauty

// The following line is needed to use the DateTime type
open System

let schedule (appointmentDateDescription: string) : DateTime =
    DateTime.Parse(appointmentDateDescription)

let hasPassed (appointmentDate: DateTime) : bool =
    DateTime.UtcNow > appointmentDate.ToUniversalTime()

let isAfternoonAppointment (appointmentDate: DateTime) : bool =
    appointmentDate.Hour >= 12
    && appointmentDate.Hour < 18

let description (appointmentDate: DateTime) : string =
    "You have an appointment on "
    + appointmentDate.ToString("M/d/yyyy h:mm:ss tt")
    + "."

let anniversaryDate () : DateTime =
    DateTime(DateTime.Now.Year, 9, 15, 0, 0, 0)


schedule "10/25/2019 13:45:00"

description (DateTime(2019, 3, 29, 15, 0, 0))
