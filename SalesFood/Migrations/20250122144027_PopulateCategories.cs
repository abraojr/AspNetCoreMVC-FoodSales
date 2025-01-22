using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesFood.Migrations
{
    public partial class PopulateCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categories (Name, Description) " +
                "VALUES ('Normal', 'Food made with normal ingredients')");

            migrationBuilder.Sql("INSERT INTO Categories (Name, Description) " +
                "VALUES ('Natural', 'Food made with whole and natural ingredients')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categories");
        }
    }
}
