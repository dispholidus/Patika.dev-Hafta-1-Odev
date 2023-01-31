using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantMenuApi.Migrations
{
    /// <inheritdoc />
    public partial class SeconMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantMenuItems_Categories_CategoryId",
                table: "RestaurantMenuItems");

            migrationBuilder.DropIndex(
                name: "IX_RestaurantMenuItems_CategoryId",
                table: "RestaurantMenuItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RestaurantMenuItems_CategoryId",
                table: "RestaurantMenuItems",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantMenuItems_Categories_CategoryId",
                table: "RestaurantMenuItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
