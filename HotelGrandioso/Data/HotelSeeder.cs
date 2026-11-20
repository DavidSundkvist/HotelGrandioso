using HotelGrandioso.Models;

namespace HotelGrandioso.Data;

public class HotelSeeder
{
    public static void Seed(HotelContext context)
    {
        if (!context.RoomTypes.Any())
        {
            context.RoomTypes.AddRange(
                new RoomType { Name = "Standard", Description = "Vanligt hotellrum" },
                new RoomType { Name = "Luxury", Description = "Lyxigt hotellrum" },
                new RoomType { Name = "Bridal", Description = "Bröllopssvit" }
            );

            context.SaveChanges();
        }
        var standard = context.RoomTypes
    .First(rt => rt.Name == "Standard");

        var luxury = context.RoomTypes
            .First(rt => rt.Name == "Luxury");

        var bridal = context.RoomTypes
            .First(rt => rt.Name == "Bridal");
        if (context.Rooms.Any())
        {
            return;
        }
        //förhidra dubbletter
        // forloop nästa 
        for (int roomNumber = 101; roomNumber <= 105; roomNumber++)
        {
           context.Rooms.Add(new Room
           {
               RoomNumber = roomNumber,
               Floor = 1,
               SizeM2 = 80,
               PricePerNight = 800,
               RoomTypeId = standard.Id
           });
            //fick ändra paranteser
            
        }
        for (int roomNumber = 201; roomNumber <= 205; roomNumber++)
        {
            context.Rooms.Add(new Room
            {
                RoomNumber = roomNumber,
                Floor = 2,
                SizeM2 = 100,
                PricePerNight = 1000,
                RoomTypeId = standard.Id
            });
        }
        context.SaveChanges();
       
        for (int roomNumber = 301; roomNumber <= 303; roomNumber++)
        {
            context.Rooms.Add(new Room
            {
                RoomNumber = roomNumber,
                Floor = 3,
                SizeM2 = 120,
                PricePerNight = 1200,
                RoomTypeId = standard.Id
            });
        }
        // Rum Luxury
        context.Rooms.Add(new Room
        {
            RoomNumber = 304,
            Floor = 3,
            SizeM2 = 180,
            PricePerNight = 2800,
            RoomTypeId = luxury.Id
        });

        // Rum bröllopssvit
        context.Rooms.Add(new Room
        {
            RoomNumber = 305,
            Floor = 3,
            SizeM2 = 220,
            PricePerNight = 3500,
            RoomTypeId = bridal.Id
        });

        context.SaveChanges();
    }
}

