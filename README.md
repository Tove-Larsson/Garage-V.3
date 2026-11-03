# Garage 3.0

Konsolapplikation som simulerar ett garage. Fordon kan parkeras, hämtas ut, listas och sökas.

## Köra programmet
1. Klona repot och öppna `Garage-ovning5.slnx` i Visual Studio, tryck F5.
   Eller kör `dotnet run` i projektmappen.
2. Välj i huvudmenyn mellan tomt garage, garage med förparkerade fordon eller att ladda tidigare garage och fordon från en fil.

## Funktioner
- Skapa ett garage med valfri storlek, tomt eller med förparkerade fordon eller ett tidigare garage
- Parkera och hämta ut fordon
- Se antal lediga och totala parkeringsplatser i garagemenyn
- Lista alla fordon samt antal per fordonstyp
- Söka på regnummer (skiftlägesokänsligt)
- Söka på färg, antal hjul och fordonstyp, enskilt eller i kombination
- Tydlig feedback om varför en parkering inte gick, till exempel fullt garage eller upptaget regnummer
- **Filhantering:** Spara ner garaget till JSON-format och ladda in ett tidigare sparat garage (inklusive bevarade subklasser via polymorfism)

## Regler
- Regnumret måste vara unikt och ha formatet tre bokstäver och tre siffror (ABC123) eller tre bokstäver, två siffror och en bokstav (ABC12D). Reglerna ligger samlade i `LicensePlate`.
- Ett garage kan ha mellan 1 och 2000 parkeringsplatser (egen avgränsning). Kapaciteten anges när garaget skapas.
- Garaget är generiskt (`Garage<T>`), begränsat till typer som implementerar `IVehicle`, och går att iterera över med `foreach` eftersom det implementerar `IEnumerable<T>`.

## Kända begränsningar
- Alla fordon tar en parkeringsplats, oavsett typ.
- Numeriska värden som antal hjul och säten kontrolleras först när fordonet skapas. Ett ogiltigt värde avbryter därför hela parkeringen istället för att fråga om just det fältet.

## Projektstruktur
- `Vehicles/`: interfacet `IVehicle`, basklassen `Vehicle`, subklasserna `Car`, `Motorcycle`, `Bus`, `Airplane` och `Boat`, hjälpklassen `LicensePlate` med reglerna för regnummer (normalisering och format) samt enums för färg, fordonstyp och bränsle
- `Garages/`: det generiska garaget `Garage<T>`, som hanterar fordonen och sökningarna, `GarageHandler` med interfacet `IHandler`, som är mellanledet mellan gränssnittet och garaget, samt `ParkingResult`, som talar om varför en parkering lyckades eller inte
- `Garages/Persistence/`: filhantering för JSON (`IGarageFileHandler`, `GarageFileHandler` och `GarageSaveData`).
- `UI/`: menyn (`Menu`) med interfacet `IUI`, samt hjälpklasser för inmatning och skapande av fordon (`VehicleInput`, `VehicleCreator`, `InputHelper`)
- `Garage_ovning5.Tests/`: enhetstester skrivna med xUnit och AAA-mönstret för att testa publika metoder och gränsvärden i `Garage<T>`
- `Program.cs`: startpunkt som skapar handler och meny och kopplar ihop dem