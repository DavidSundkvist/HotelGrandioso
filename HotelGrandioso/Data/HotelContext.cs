using Microsoft.EntityFrameworkCore;
using HotelGrandioso.Models;    
// tillgång EF Core klasser och våra modellklasser
namespace HotelGrandioso.Data;

public class HotelContext : DbContext
    //ärver från DbContext
{
    public DbSet<Guest> Guests => Set<Guest>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<RoomType> RoomTypes => Set<RoomType>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Treatment> Treatments => Set<Treatment>();
    public DbSet<SpaBooking> SpaBookings => Set<SpaBooking>();
    // AI säger ersätt min { get; set; } = null!; med => Set<Guest>() eftersom => retunerar samlingen va EF och man slipper null! 
   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
     string connectionString = Environment.GetEnvironmentVariable("HOTEL_DB")?? throw new InvalidOperationException("Miljövariabeln HOTEL_DB saknas.");

        optionsBuilder.UseSqlite(connectionString);
        //Ai hjälp gå tllbacks och kolla på kod
   }
}