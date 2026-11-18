using System;
using System.Collections.Generic;
using System.Text;

namespace HotelGrandioso.Models;

public class RoomType
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    // = ""; för att skapa ett tomt värde istället för null. så man kan fylla i det senare.
}
