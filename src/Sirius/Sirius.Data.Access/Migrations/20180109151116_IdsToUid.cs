using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Sirius.Data.Access.Migrations
{
    public partial class IdsToUid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityUid",
                table: "Settings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Settings",
                table: "Settings");
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Settings");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Settings",
                nullable: false);

            migrationBuilder.AddPrimaryKey("PK_Settings", "Settings", "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Settings",
                table: "Settings");
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Settings");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Settings",
                nullable: false)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddPrimaryKey("PK_Settings", "Settings", "Id");

            migrationBuilder.AddColumn<Guid>(
                name: "EntityUid",
                table: "Settings",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
