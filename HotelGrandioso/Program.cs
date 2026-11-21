using HotelGrandioso.UI;
using HotelGrandioso.Services;
// Ger Program.cs tillgång till klasserna i Services.
using HotelGrandioso.Data;



// Skapa en anslutning till databasen
using var db = new HotelContext();
HotelSeeder.Seed(db);

BookingService bookingService = new BookingService(db);
BookingMenu bookingMenu = new BookingMenu(db, bookingService);

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
            // Bokningslogiken flyttades till BookingMenu för att följa SRP.
            bookingMenu.BokaRum();
            break;

        case "2":
            Console.WriteLine("Du valde att avboka rum.");
            break;

        case "3":
            bookingMenu.VisaBokningar();
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