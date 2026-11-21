using HotelGrandioso.Data;
using HotelGrandioso.Models;

namespace HotelGrandioso.Services;

public class BookingService
{
    private readonly HotelContext _context;

    public BookingService(HotelContext context)
    {
        _context = context;
    }
    public List<Booking> GetAllBookings()
    {
        return _context.Bookings.ToList();
    }

    public bool IsRoomAvailable(int roomId, DateTime checkIn, DateTime checkOut)
    {
        bool isBooked = _context.Bookings.Any(b =>
            b.RoomId == roomId &&
            b.CheckIn < checkOut &&
            b.CheckOut > checkIn);

        return !isBooked;
    }
    public bool BookRoom(int guestId, int roomId, DateTime checkIn, DateTime checkOut)
    {
        if (checkIn >= checkOut)
        {
            return false;
        }

        if (!IsRoomAvailable(roomId, checkIn, checkOut))
        {
            return false;
        }

        Booking booking = new Booking
        {
            GuestId = guestId,
            RoomId = roomId,
            CheckIn = checkIn,
            CheckOut = checkOut
        };

        _context.Bookings.Add(booking);
        _context.SaveChanges();

        return true;
    }
}