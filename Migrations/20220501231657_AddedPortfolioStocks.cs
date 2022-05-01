using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockPortfolio.Migrations
{
    public partial class AddedPortfolioStocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactions_Stocks_StockID",
                table: "StockTransactions");

            migrationBuilder.RenameColumn(
                name: "StockID",
                table: "StockTransactions",
                newName: "PortfolioStockID");

            migrationBuilder.RenameIndex(
                name: "IX_StockTransactions_StockID",
                table: "StockTransactions",
                newName: "IX_StockTransactions_PortfolioStockID");

            migrationBuilder.CreateTable(
                name: "PortfolioStocks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AveragePrice = table.Column<double>(type: "float", nullable: false),
                    Volume = table.Column<int>(type: "int", nullable: false),
                    InitialPurchaseDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioStocks", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactions_PortfolioStocks_PortfolioStockID",
                table: "StockTransactions",
                column: "PortfolioStockID",
                principalTable: "PortfolioStocks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactions_PortfolioStocks_PortfolioStockID",
                table: "StockTransactions");

            migrationBuilder.DropTable(
                name: "PortfolioStocks");

            migrationBuilder.RenameColumn(
                name: "PortfolioStockID",
                table: "StockTransactions",
                newName: "StockID");

            migrationBuilder.RenameIndex(
                name: "IX_StockTransactions_PortfolioStockID",
                table: "StockTransactions",
                newName: "IX_StockTransactions_StockID");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactions_Stocks_StockID",
                table: "StockTransactions",
                column: "StockID",
                principalTable: "Stocks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
