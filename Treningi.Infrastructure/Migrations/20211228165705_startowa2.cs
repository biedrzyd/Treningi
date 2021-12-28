using Microsoft.EntityFrameworkCore.Migrations;

namespace Treningi.Infrastructure.Migrations
{
    public partial class startowa2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkiJumper_Coach_CoachID",
                table: "SkiJumper");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SkiJumper",
                table: "SkiJumper");

            migrationBuilder.RenameTable(
                name: "SkiJumper",
                newName: "Competitor");

            migrationBuilder.RenameIndex(
                name: "IX_SkiJumper_CoachID",
                table: "Competitor",
                newName: "IX_Competitor_CoachID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Competitor",
                table: "Competitor",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitor_Coach_CoachID",
                table: "Competitor",
                column: "CoachID",
                principalTable: "Coach",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitor_Coach_CoachID",
                table: "Competitor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Competitor",
                table: "Competitor");

            migrationBuilder.RenameTable(
                name: "Competitor",
                newName: "SkiJumper");

            migrationBuilder.RenameIndex(
                name: "IX_Competitor_CoachID",
                table: "SkiJumper",
                newName: "IX_SkiJumper_CoachID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SkiJumper",
                table: "SkiJumper",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SkiJumper_Coach_CoachID",
                table: "SkiJumper",
                column: "CoachID",
                principalTable: "Coach",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
