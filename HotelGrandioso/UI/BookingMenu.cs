
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
        var valtRum = _bookingService.GetRoomByNumber(rumsnummer);

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
        bool bookingSucceeded = _bookingService.BookRoomForGuest(
          firstName,
          lastName,
          valtRum.Id,
          checkIn,
          checkOut
          );


        if (bookingSucceeded)
        {
         
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
    public void AvbokaRum()
    {
        Console.WriteLine("\n=== AVBOKA RUM ===");
        Console.Write("Ange boknings-ID: ");

        if (!int.TryParse(Console.ReadLine(), out int bookingId))
        {
            Console.WriteLine("Ogiltigt boknings-ID.");
            return;
        }

        bool lyckades = _bookingService.CancelBooking(bookingId);

        if (lyckades)
        {
            Console.WriteLine("Bokningen har avbokats!");
        }
        else
        {
            Console.WriteLine("Ingen bokning hittades med detta ID.");
        }
    }
}