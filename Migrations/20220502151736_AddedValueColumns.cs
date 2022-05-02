using Microsoft.EntityFrameworkCore.Migrations;

namespace StockPortfolio.Migrations
{
    public partial class AddedValueColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TransactionValue",
                table: "PortfolioStockTransactions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "StockValue",
                table: "PortfolioStocks",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionValue",
                table: "PortfolioStockTransactions");

            migrationBuilder.DropColumn(
                name: "StockValue",
                table: "PortfolioStocks");
        }
    }
}
