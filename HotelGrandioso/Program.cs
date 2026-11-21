using HotelGrandioso.Data;

// Skapa en anslutning till databasen
using var db = new HotelContext();
HotelSeeder.Seed(db);

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
            Console.WriteLine("Du valde att boka rum.");
            break;

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