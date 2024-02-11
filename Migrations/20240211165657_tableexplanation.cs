using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AparmentSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class tableexplanation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MainBuildings_AspNetUsers_UserId",
                table: "MainBuildings");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_MainBuildings_MainBuildingId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MainBuildings",
                table: "MainBuildings");

            migrationBuilder.RenameTable(
                name: "MainBuildings",
                newName: "MainBuilding");

            migrationBuilder.RenameIndex(
                name: "IX_MainBuildings_UserId",
                table: "MainBuilding",
                newName: "IX_MainBuilding_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MainBuilding",
                table: "MainBuilding",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MainBuilding_AspNetUsers_UserId",
                table: "MainBuilding",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_MainBuilding_MainBuildingId",
                table: "Payments",
                column: "MainBuildingId",
                principalTable: "MainBuilding",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MainBuilding_AspNetUsers_UserId",
                table: "MainBuilding");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_MainBuilding_MainBuildingId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MainBuilding",
                table: "MainBuilding");

            migrationBuilder.RenameTable(
                name: "MainBuilding",
                newName: "MainBuildings");

            migrationBuilder.RenameIndex(
                name: "IX_MainBuilding_UserId",
                table: "MainBuildings",
                newName: "IX_MainBuildings_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MainBuildings",
                table: "MainBuildings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MainBuildings_AspNetUsers_UserId",
                table: "MainBuildings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_MainBuildings_MainBuildingId",
                table: "Payments",
                column: "MainBuildingId",
                principalTable: "MainBuildings",
                principalColumn: "Id");
        }
    }
}
