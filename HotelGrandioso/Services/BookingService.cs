using HotelGrandioso.Data;
using HotelGrandioso.Models;
using Microsoft.EntityFrameworkCore;

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
        return _context.Bookings.Include(b => b.Room).ToList();
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
    public Room? GetRoomByNumber(int roomNumber)
    {
        return _context.Rooms
            .FirstOrDefault(r => r.RoomNumber == roomNumber);
    }
    public bool CancelBooking(int bookingId)
    {
        var booking = _context.Bookings
            .FirstOrDefault(b => b.Id == bookingId);

        if (booking == null)
        {
            return false;
        }

        _context.Bookings.Remove(booking);
        _context.SaveChanges();

        return true;
    }
    public bool BookRoomForGuest(
    string firstName,
    string lastName,
    int roomId,
    DateTime checkIn,
    DateTime checkOut)
    {
        if (checkIn >= checkOut || !IsRoomAvailable(roomId, checkIn, checkOut))
        {
            return false;
        }

        using var transaction = _context.Database.BeginTransaction();

        Guest guest = new Guest
        {
            FirstName = firstName,
            LastName = lastName
        };

        _context.Guests.Add(guest);
        _context.SaveChanges();

        bool lyckades = BookRoom(guest.Id, roomId, checkIn, checkOut);

        if (!lyckades)
        {
            return false;
        }

        transaction.Commit();
        return true;
    }
}