using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetTracker.Migrations
{
    public partial class ChangedRecurrence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recurrence",
                table: "Expenses");

            migrationBuilder.AddColumn<int>(
                name: "Recurrence",
                table: "ExpenseCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recurrence",
                table: "ExpenseCategories");

            migrationBuilder.AddColumn<int>(
                name: "Recurrence",
                table: "Expenses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
