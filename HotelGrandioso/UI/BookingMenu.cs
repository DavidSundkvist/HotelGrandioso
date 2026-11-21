
using HotelGrandioso.Data;
using HotelGrandioso.Services;
using HotelGrandioso.Models;
namespace HotelGrandioso.UI;

public class BookingMenu
{
    private readonly HotelContext _db;
    private readonly BookingService _bookingService;

    public BookingMenu(HotelContext db, BookingService bookingService)
    {
        _db = db;
        _bookingService = bookingService;
    }
    //ny konstruktor som tar emot db och bookingservice som parametrar
    public void BokaRum()
    {
        Console.WriteLine("=== BOKA RUM ===");

        Console.Write("Ange rumsnummer: ");
        string rumInput = Console.ReadLine() ?? "";

        if (!int.TryParse(rumInput, out int rumsnummer))
        {
            Console.WriteLine("Ogiltigt rumsnummer.");
            return;
        }
        var valtRum = _db.Rooms.FirstOrDefault(r => r.RoomNumber == rumsnummer);

        if (valtRum == null)
        {
            Console.WriteLine("Rummet finns inte.");
            return;
        }

        Console.WriteLine($"Du valde rum {valtRum.RoomNumber} - {valtRum.PricePerNight} kr/natt.");
        Console.Write("Ange incheckningsdatum (åååå-MM-dd): ");
        string checkInInput = Console.ReadLine() ?? "";

        if (!DateTime.TryParse(checkInInput, out DateTime checkIn))
        {
            Console.WriteLine("Ogiltigt incheckningsdatum.");
            return;
        }
        Console.Write("Ange utcheckningsdatum (åååå-MM-dd): ");
        string checkOutInput = Console.ReadLine() ?? "";

        if (!DateTime.TryParse(checkOutInput, out DateTime checkOut))
        {
            Console.WriteLine("Ogiltigt utcheckningsdatum.");
            return;
        }
        if (checkOut <= checkIn)
        {
            Console.WriteLine("Utcheckningsdatum måste vara efter incheckningsdatum.");
            return;
        }
        // borde förhindrar dubbelbokning genom att anropa IsRoomAvailable i BookingService
        if (!_bookingService.IsRoomAvailable(valtRum.Id, checkIn, checkOut))
        {
            Console.WriteLine("Rummet är redan bokat under den perioden.");
            return;
        }
        Console.Write("Ange gästens förnamn: ");
        string firstName = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(firstName))
        {
            Console.WriteLine("Förnamn får inte vara tomt.");
            return;
        }
        Console.Write("Ange gästens efternamn: ");
        string lastName = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(lastName))
        {
            Console.WriteLine("Efternamn får inte vara tomt.");
            return;
        }
        // Skapa en ny gäst med användarens uppgifter
        Guest guest = new Guest
        {
            FirstName = firstName,
            LastName = lastName
        };
        // Starta en transaktion
        using var transaction = _db.Database.BeginTransaction();

        _db.Guests.Add(guest);
        _db.SaveChanges();
        // Skapa bokningen med BookingService
        bool bookingSucceeded = _bookingService.BookRoom(
            guest.Id,
            valtRum.Id,
            checkIn,
            checkOut
        );

       
        if (bookingSucceeded)
        {
            transaction.Commit();

            int antalNatter = (checkOut - checkIn).Days;
            decimal totalPris = antalNatter * valtRum.PricePerNight;

            Console.WriteLine("Bokningen har skapats!");
            Console.WriteLine($"Totalpris: {totalPris} kr för {antalNatter} nätter.");
        }
        else
        {
            Console.WriteLine("Bokningen kunde inte genomföras.");
        }
    }
    // metod som hanterar bokning av rumm
    public void VisaBokningar()
    {
        var bokningar = _bookingService.GetAllBookings();

        if (bokningar.Count == 0)
        {
            Console.WriteLine("Det finns inga bokningar.");
            return;
        }

        foreach (var bokning in bokningar)
        {
            Console.WriteLine($"Bokning {bokning.Id}: Rum {bokning.Room.RoomNumber}, {bokning.CheckIn:yyyy-MM-dd} till {bokning.CheckOut:yyyy-MM-dd}");
        }
    }
}