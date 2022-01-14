using Microsoft.EntityFrameworkCore.Migrations;

namespace Treningi.WebApp.Migrations
{
    public partial class imagetocompetitor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Competitor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Competitor_ImageId",
                table: "Competitor",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitor_Images_ImageId",
                table: "Competitor",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitor_Images_ImageId",
                table: "Competitor");

            migrationBuilder.DropIndex(
                name: "IX_Competitor_ImageId",
                table: "Competitor");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Competitor");
        }
    }
}
