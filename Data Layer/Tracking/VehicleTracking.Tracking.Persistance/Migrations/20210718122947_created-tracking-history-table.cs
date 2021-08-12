using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleTracking.Tracking.Persistance.Migrations
{
    public partial class createdtrackinghistorytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrackingHistory",
                columns: table => new
                {
                    TrackingHistoryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<long>(type: "bigint", nullable: false),
                    DeviceId = table.Column<long>(type: "bigint", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    PlaceId = table.Column<string>(type: "NVARCHAR(40)", nullable: true),
                    Licence = table.Column<string>(type: "NVARCHAR(60)", nullable: true),
                    DisplayName = table.Column<string>(type: "NVARCHAR(350)", nullable: true),
                    DateEntered = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingHistory", x => x.TrackingHistoryId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackingHistory");
        }
    }
}
