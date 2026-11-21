using HotelGrandioso.Data;
using HotelGrandioso.Services;

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
        // Kontrollera om rummet är ledigt under de valda datumen
        if (!_bookingService.IsRoomAvailable(valtRum.Id, checkIn, checkOut))
        {
            Console.WriteLine("Rummet är redan bokat under den perioden.");
            return;
        }
    }
    // metod som hanterar bokning av rumm
}