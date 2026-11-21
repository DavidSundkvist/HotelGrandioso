using HotelGrandioso.Services;
// Ger Program.cs tillgång till klasserna i Services.
using HotelGrandioso.Data;
using System.Security;

// Skapa en anslutning till databasen
using var db = new HotelContext();
HotelSeeder.Seed(db);

BookingService bookingService = new BookingService(db);

Console.WriteLine("=== Hotel Grandioso ===");
Console.WriteLine("Databasen är ansluten!");
bool körProgrammet = true;
while (körProgrammet)
{
    Console.WriteLine("=== HUVUDMENY ===");
Console.WriteLine("1. Boka rum");
Console.WriteLine("2. Avboka rum");
Console.WriteLine("3. Visa bokningar");
Console.WriteLine("4. Visa lediga rum");
Console.WriteLine("5. Avsluta");

Console.Write("Välj ett alternativ: ");
string val = Console.ReadLine() ?? "";
    // nästa switch och loop while 

    switch (val)
    {
        case "1":
            Console.WriteLine("=== BOKA RUM ===");

            Console.Write("Ange rumsnummer: ");
            string rumInput = Console.ReadLine() ?? "";

            if (!int.TryParse(rumInput, out int rumsnummer))
            {
                Console.WriteLine("Ogiltigt rumsnummer.");
                break;
            }

            var valtRum = db.Rooms.FirstOrDefault(r => r.RoomNumber == rumsnummer);

            if (valtRum == null)
            {
                Console.WriteLine("Rummet finns inte.");
                break;
            }

            Console.WriteLine($"Du valde rum {valtRum.RoomNumber} - {valtRum.PricePerNight} kr/natt.");
            Console.Write("Ange incheckningsdatum (åååå-MM-dd): ");
            string checkInInput = Console.ReadLine() ?? "";
            if (!DateTime.TryParse(checkInInput, out DateTime checkIn))
            {
                Console.WriteLine("Ogiltigt datum.");
                break;
            }
            Console.Write("Ange utcheckningsdatum (ÅÅÅÅ-MM-DD): ");
            string checkOutInput = Console.ReadLine() ?? "";

            if (!DateTime.TryParse(checkOutInput, out DateTime checkOut))
            {
                Console.WriteLine("Ogiltigt utcheckningsdatum.");
                break;
            }
            if (checkOut <= checkIn)
            {
                Console.WriteLine("Utcheckningsdatum måste vara efter incheckningsdatum.");
                break;
            }
            break;
            // dubbla break ser fullt ut men det är för att bryta ur switch loop, fixa senare probobly


        case "2":
            Console.WriteLine("Du valde att avboka rum.");
            break;

        case "3":
            Console.WriteLine("Du valde att visa bokningar.");
            break;

        case "4":
            Console.WriteLine("=== HOTELLETS RUM ===");

            foreach (var rum in db.Rooms)
            {
                Console.WriteLine($"Rum {rum.RoomNumber} - {rum.PricePerNight} kr/natt");
            }

            break;

        case "5":
            Console.WriteLine("Programmet avslutas.");
            körProgrammet = false;
            break;

        default:
            Console.WriteLine("Ogiltigt val.");
            break;
    }

}