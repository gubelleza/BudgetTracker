using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetTracker.Migrations
{
    public partial class Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Users_PaidBy",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_Users_PaidBy",
                table: "Incomes");

            migrationBuilder.DropIndex(
                name: "IX_Incomes_PaidBy",
                table: "Incomes");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_PaidBy",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "PaidBy",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "PaidBy",
                table: "Expenses");

            migrationBuilder.AddColumn<string>(
                name: "BudgetMemberName",
                table: "Incomes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BudgetMemberName",
                table: "Expenses",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IncomeCategories_CategoryName",
                table: "IncomeCategories",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategories_CategoryName",
                table: "ExpenseCategories",
                column: "CategoryName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_IncomeCategories_CategoryName",
                table: "IncomeCategories");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseCategories_CategoryName",
                table: "ExpenseCategories");

            migrationBuilder.DropColumn(
                name: "BudgetMemberName",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "BudgetMemberName",
                table: "Expenses");

            migrationBuilder.AddColumn<Guid>(
                name: "PaidBy",
                table: "Incomes",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PaidBy",
                table: "Expenses",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_PaidBy",
                table: "Incomes",
                column: "PaidBy");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_PaidBy",
                table: "Expenses",
                column: "PaidBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Users_PaidBy",
                table: "Expenses",
                column: "PaidBy",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_Users_PaidBy",
                table: "Incomes",
                column: "PaidBy",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
