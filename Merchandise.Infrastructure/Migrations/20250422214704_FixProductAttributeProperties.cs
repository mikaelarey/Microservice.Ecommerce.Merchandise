﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Merchandise.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixProductAttributeProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "AttributeNameId",
                table: "AttributeValue",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AttributeNameId",
                table: "AttributeValue",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
