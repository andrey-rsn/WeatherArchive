using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherArchive.Migrations
{
    public partial class UpdateModelRelativeHumidity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "RelativeHumidity",
                table: "weatherConditions",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RelativeHumidity",
                table: "weatherConditions",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }
    }
}
