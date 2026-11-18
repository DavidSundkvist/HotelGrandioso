

namespace HotelGrandioso.Models;

public class SpaBooking
{
    public int id { get; set; }
    public int Guestid { get; set; }
    public Guest Guest { get; set; } = null!;
    public int Treatmentid { get; set; }
    public Treatment Treatment { get; set; } = null!;
    public DateTime Date { get; set; }
    // DateTime är en data typ 
}
