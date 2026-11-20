using HotelGrandioso.Data;

// Skapa en anslutning till databasen
using var db = new HotelContext();

Console.WriteLine("=== Hotel Grandioso ===");
Console.WriteLine("Databasen är ansluten!");