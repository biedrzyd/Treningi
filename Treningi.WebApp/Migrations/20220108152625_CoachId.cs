using Microsoft.EntityFrameworkCore.Migrations;

namespace Treningi.WebApp.Migrations
{
    public partial class CoachId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitor_Coach_CoachID",
                table: "Competitor");

            migrationBuilder.DropIndex(
                name: "IX_Competitor_CoachID",
                table: "Competitor");

            migrationBuilder.RenameColumn(
                name: "CoachID",
                table: "Competitor",
                newName: "CoachId");

            migrationBuilder.AlterColumn<int>(
                name: "CoachId",
                table: "Competitor",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    day = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    exercise = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompetitorID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.RenameColumn(
                name: "CoachId",
                table: "Competitor",
                newName: "CoachID");

            migrationBuilder.AlterColumn<int>(
                name: "CoachID",
                table: "Competitor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Competitor_CoachID",
                table: "Competitor",
                column: "CoachID");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitor_Coach_CoachID",
                table: "Competitor",
                column: "CoachID",
                principalTable: "Coach",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
