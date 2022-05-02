using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockPortfolio.Migrations
{
    public partial class ChangedTransactionName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockTransactions");

            migrationBuilder.CreateTable(
                name: "PortfolioStockTransactions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortfolioStockID = table.Column<int>(type: "int", nullable: false),
                    TransactionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioStockTransactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PortfolioStockTransactions_PortfolioStocks_PortfolioStockID",
                        column: x => x.PortfolioStockID,
                        principalTable: "PortfolioStocks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioStockTransactions_PortfolioStockID",
                table: "PortfolioStockTransactions",
                column: "PortfolioStockID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortfolioStockTransactions");

            migrationBuilder.CreateTable(
                name: "StockTransactions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortfolioStockID = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TransactionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StockTransactions_PortfolioStocks_PortfolioStockID",
                        column: x => x.PortfolioStockID,
                        principalTable: "PortfolioStocks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_PortfolioStockID",
                table: "StockTransactions",
                column: "PortfolioStockID");
        }
    }
}
