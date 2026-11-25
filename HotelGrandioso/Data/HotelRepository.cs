using HotelGrandioso.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelGrandioso.Data;

public class HotelRepository
{
    private readonly HotelContext _context;

    public HotelRepository(HotelContext context)
    {
        _context = context;
    }
    public List<Booking> GetAllBookings()
    {
        return _context.Bookings.Include(b => b.Room).ToList();
    }
    public bool HasOverlappingBooking(int roomId, DateTime checkIn, DateTime checkOut)
    {
        return _context.Bookings.Any(b =>
            b.RoomId == roomId &&
            b.CheckIn < checkOut &&
            b.CheckOut > checkIn);
    }
    public void AddBooking(Booking booking)
    {
        _context.Bookings.Add(booking);
        _context.SaveChanges();
    }
    public Room? GetRoomByNumber(int roomNumber)
    {
        return _context.Rooms
            .FirstOrDefault(r => r.RoomNumber == roomNumber);
    }
    public Booking? GetBookingById(int bookingId)
    {
        return _context.Bookings
            .FirstOrDefault(b => b.Id == bookingId);
    }
    public void RemoveBooking(Booking booking)
    {
        _context.Bookings.Remove(booking);
        _context.SaveChanges();
    }
    public void AddGuestAndBooking(Guest guest, Booking booking)
    {
        using var transaction = _context.Database.BeginTransaction();

        _context.Guests.Add(guest);
        _context.SaveChanges();

        booking.GuestId = guest.Id;

        _context.Bookings.Add(booking);
        _context.SaveChanges();

        transaction.Commit();
    }
    public List<Room> GetAllRooms()
    {
        return _context.Rooms
            .Include(r => r.RoomType)
            .ToList();
    }
}