using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelGrandioso.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Size",
                table: "Rooms",
                newName: "SizeM2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SizeM2",
                table: "Rooms",
                newName: "Size");
        }
    }
}
