using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace src.Migrations
{
    public partial class AuditTrailDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "DeletedDatas");

            migrationBuilder.RenameColumn(
                name: "PlateNumber",
                table: "DeletedDatas",
                newName: "FullName");

            migrationBuilder.AddColumn<int>(
                name: "ControlNumber",
                table: "DeletedDatas",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ControlNumber",
                table: "DeletedDatas");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "DeletedDatas",
                newName: "PlateNumber");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DeletedDatas",
                nullable: true);
        }
    }
}
