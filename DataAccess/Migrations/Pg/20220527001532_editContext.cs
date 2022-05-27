using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations.Pg
{
    public partial class editContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Stocks_CardId",
                table: "Stocks",
                column: "CardId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockOrderses_CardId",
                table: "StockOrderses",
                column: "CardId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StockOrderses_Cards_CardId",
                table: "StockOrderses",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Cards_CardId",
                table: "Stocks",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockOrderses_Cards_CardId",
                table: "StockOrderses");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Cards_CardId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_CardId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_StockOrderses_CardId",
                table: "StockOrderses");
        }
    }
}
