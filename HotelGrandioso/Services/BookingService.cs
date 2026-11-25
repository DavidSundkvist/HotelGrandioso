using HotelGrandioso.Data;
using HotelGrandioso.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelGrandioso.Services;

public class BookingService
{
    private readonly HotelRepository _repository;

    public BookingService(HotelRepository repository)
    {
        _repository = repository;
    }
    public List<Booking> GetAllBookings()
    {
        return _repository.GetAllBookings();
    }

    public bool IsRoomAvailable(int roomId, DateTime checkIn, DateTime checkOut)
    {
        bool isBooked = _repository.HasOverlappingBooking(roomId, checkIn, checkOut);

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
        _repository.AddBooking(booking);

        return true;
    }
    public Room? GetRoomByNumber(int roomNumber)
    {
        return _repository.GetRoomByNumber(roomNumber);
    }
    public bool CancelBooking(int bookingId)
    {
        var booking = _repository.GetBookingById(bookingId);

        if (booking == null)
        {
            return false;
        }

        _repository.RemoveBooking(booking);

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

        Guest guest = new Guest
        {
            FirstName = firstName,
            LastName = lastName
        };

        Booking booking = new Booking
        {
            RoomId = roomId,
            CheckIn = checkIn,
            CheckOut = checkOut
        };

        _repository.AddGuestAndBooking(guest, booking);

        return true;
    }
}