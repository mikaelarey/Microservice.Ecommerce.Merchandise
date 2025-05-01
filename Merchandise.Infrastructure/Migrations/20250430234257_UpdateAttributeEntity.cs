using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Merchandise.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAttributeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeName");

            migrationBuilder.CreateTable(
                name: "CodeDecodeAttribute",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Decode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateTimeLastUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeDecodeAttribute", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodeDecodeAttribute_Code",
                table: "CodeDecodeAttribute",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CodeDecodeAttribute_Name",
                table: "CodeDecodeAttribute",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodeDecodeAttribute");

            migrationBuilder.CreateTable(
                name: "AttributeName",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    DateTimeCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateTimeLastUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeName", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeName_Name",
                table: "AttributeName",
                column: "Name",
                unique: true);
        }
    }
}
