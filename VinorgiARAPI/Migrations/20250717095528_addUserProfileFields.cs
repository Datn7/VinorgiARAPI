using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinorgiARAPI.Migrations
{
    /// <inheritdoc />
    public partial class addUserProfileFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Model3D_AspNetUsers_UserId",
                table: "Model3D");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Model3D",
                table: "Model3D");

            migrationBuilder.RenameTable(
                name: "Model3D",
                newName: "Models");

            migrationBuilder.RenameIndex(
                name: "IX_Model3D_UserId",
                table: "Models",
                newName: "IX_Models_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Models",
                table: "Models",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_AspNetUsers_UserId",
                table: "Models",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_AspNetUsers_UserId",
                table: "Models");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Models",
                table: "Models");

            migrationBuilder.RenameTable(
                name: "Models",
                newName: "Model3D");

            migrationBuilder.RenameIndex(
                name: "IX_Models_UserId",
                table: "Model3D",
                newName: "IX_Model3D_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Model3D",
                table: "Model3D",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Model3D_AspNetUsers_UserId",
                table: "Model3D",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
