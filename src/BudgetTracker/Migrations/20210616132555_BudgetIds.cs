using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetTracker.Migrations
{
    public partial class BudgetIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BudgetId",
                table: "ExpenseCategories",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategories_BudgetId",
                table: "ExpenseCategories",
                column: "BudgetId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseCategories_Budgets_BudgetId",
                table: "ExpenseCategories",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "BudgetId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseCategories_Budgets_BudgetId",
                table: "ExpenseCategories");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseCategories_BudgetId",
                table: "ExpenseCategories");

            migrationBuilder.DropColumn(
                name: "BudgetId",
                table: "ExpenseCategories");
        }
    }
}
