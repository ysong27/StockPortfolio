using Microsoft.EntityFrameworkCore.Migrations;

namespace StockPortfolio.Migrations
{
    public partial class AddedInitialPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "InitialPrice",
                table: "PortfolioStocks",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialPrice",
                table: "PortfolioStocks");
        }
    }
}
