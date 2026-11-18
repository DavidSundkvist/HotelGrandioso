using System;
using System.Collections.Generic;
using System.Text;

namespace HotelGrandioso.Models;


public class Room
{
    public int Id { get; set; }
    public int RoomNumber { get; set; }
    public int Floor { get; set; }
    public int Size { get; set; }
    public decimal PricePerNight { get; set; }
    public int RoomTypeId { get; set; }
    public RoomType RoomType { get; set; } = null!;

}