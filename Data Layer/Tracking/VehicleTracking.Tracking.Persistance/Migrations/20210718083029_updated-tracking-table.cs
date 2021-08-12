using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleTracking.Tracking.Persistance.Migrations
{
    public partial class updatedtrackingtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Location",
                type: "NVARCHAR(350)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Licence",
                table: "Location",
                type: "NVARCHAR(60)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceId",
                table: "Location",
                type: "NVARCHAR(40)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "Licence",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "Location");
        }
    }
}
