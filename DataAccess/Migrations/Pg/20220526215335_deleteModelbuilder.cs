using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataAccess.Migrations.Pg
{
    public partial class deleteModelbuilder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockOrderses_Stocks_Id",
                table: "StockOrderses");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Storages_StorageId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_StorageId",
                table: "Stocks");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "StockOrderses",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "StockOrderses",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_StorageId",
                table: "Stocks",
                column: "StorageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StockOrderses_Stocks_Id",
                table: "StockOrderses",
                column: "Id",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Storages_StorageId",
                table: "Stocks",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
