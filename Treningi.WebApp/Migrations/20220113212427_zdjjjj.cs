using Microsoft.EntityFrameworkCore.Migrations;

namespace Treningi.WebApp.Migrations
{
    public partial class zdjjjj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userID",
                table: "Activity");

            migrationBuilder.AlterColumn<int>(
                name: "CompetitorID",
                table: "Activity",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.AlterColumn<string>(
                name: "CompetitorID",
                table: "Activity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "userID",
                table: "Activity",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
