using Microsoft.EntityFrameworkCore.Migrations;

namespace Treningi.WebApp.Migrations
{
    public partial class nwwnwnwnwn11sdas1ws : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitor_Images_ImageId1",
                table: "Competitor");

            migrationBuilder.DropIndex(
                name: "IX_Competitor_ImageId1",
                table: "Competitor");

            migrationBuilder.DropColumn(
                name: "ImageId1",
                table: "Competitor");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Competitor",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserImageId",
                table: "Competitor",
                type: "nvarchar(max)",
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
                name: "UserImageId",
                table: "Competitor");

            migrationBuilder.AlterColumn<string>(
                name: "ImageId",
                table: "Competitor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId1",
                table: "Competitor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Competitor_ImageId1",
                table: "Competitor",
                column: "ImageId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitor_Images_ImageId1",
                table: "Competitor",
                column: "ImageId1",
                principalTable: "Images",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
