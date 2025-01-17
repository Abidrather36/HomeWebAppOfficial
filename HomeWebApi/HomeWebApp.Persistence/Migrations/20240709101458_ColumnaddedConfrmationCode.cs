﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeWebApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ColumnaddedConfrmationCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfirmationCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmationCode",
                table: "Users");
        }
    }
}
