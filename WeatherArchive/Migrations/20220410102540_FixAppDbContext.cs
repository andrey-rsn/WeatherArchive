using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherArchive.Migrations
{
    public partial class FixAppDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "weatherConditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    AirTemerature = table.Column<double>(type: "float", nullable: false),
                    RelativeHumidity = table.Column<int>(type: "int", nullable: false),
                    Td = table.Column<double>(type: "float", nullable: false),
                    AtmosphericPressure = table.Column<int>(type: "int", nullable: false),
                    WindDirection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WindSpeed = table.Column<int>(type: "int", nullable: false),
                    CloudCover = table.Column<int>(type: "int", nullable: false),
                    H = table.Column<int>(type: "int", nullable: false),
                    VV = table.Column<int>(type: "int", nullable: true),
                    WeatherPhenomena = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weatherConditions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "weatherConditions");
        }
    }
}
