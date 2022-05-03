using Microsoft.EntityFrameworkCore.Migrations;

namespace StockPortfolio.Migrations
{
    public partial class ChangedToStockCost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StockValue",
                table: "PortfolioStocks",
                newName: "StockCost");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StockCost",
                table: "PortfolioStocks",
                newName: "StockValue");
        }
    }
}
