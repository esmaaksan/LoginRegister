using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Layer.Migrations
{
    /// <inheritdoc />
    public partial class mig_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_adresses_adressesId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_adressesId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "adressesId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "adresses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_adresses_UserId",
                table: "adresses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_adresses_AspNetUsers_UserId",
                table: "adresses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_adresses_AspNetUsers_UserId",
                table: "adresses");

            migrationBuilder.DropIndex(
                name: "IX_adresses_UserId",
                table: "adresses");

            migrationBuilder.AddColumn<int>(
                name: "adressesId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "adresses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_adressesId",
                table: "AspNetUsers",
                column: "adressesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_adresses_adressesId",
                table: "AspNetUsers",
                column: "adressesId",
                principalTable: "adresses",
                principalColumn: "Id");
        }
    }
}
