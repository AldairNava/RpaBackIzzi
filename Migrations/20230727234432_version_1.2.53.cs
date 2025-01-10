using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class version1253 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "BotsProcess",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ip",
                table: "BotsProcess",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "BotsProcess");

            migrationBuilder.DropColumn(
                name: "ip",
                table: "BotsProcess");
        }
    }
}
