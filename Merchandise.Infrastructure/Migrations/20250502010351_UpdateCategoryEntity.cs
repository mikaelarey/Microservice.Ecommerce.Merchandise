using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Merchandise.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCategoryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRootCategory",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "ParentCategory",
                table: "Category");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentCategoryId",
                table: "Category",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentCategoryId",
                table: "Category",
                column: "ParentCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Category_ParentCategoryId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "ParentCategoryId",
                table: "Category");

            migrationBuilder.AddColumn<bool>(
                name: "IsRootCategory",
                table: "Category",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ParentCategory",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
