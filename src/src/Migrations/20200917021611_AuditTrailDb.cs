using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace src.Migrations
{
    public partial class AuditTrailDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "ExpireDate",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "IsStudent",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "IsTimeout",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "LockerNumber",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "MedicalCondition",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Plan",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "Members",
                newName: "ControlNumber");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Members",
                newName: "Entitlement");

            migrationBuilder.RenameColumn(
                name: "School",
                table: "Members",
                newName: "Coverage");

            migrationBuilder.CreateTable(
                name: "DeletedDatas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<int>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Origin = table.Column<string>(nullable: true),
                    PlateNumber = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeletedDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EditedDatas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ControlNumber = table.Column<int>(nullable: false),
                    DateEdited = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<string>(nullable: true),
                    EditedData = table.Column<string>(nullable: true),
                    Origin = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditedDatas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeletedDatas");

            migrationBuilder.DropTable(
                name: "EditedDatas");

            migrationBuilder.RenameColumn(
                name: "Entitlement",
                table: "Members",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "Coverage",
                table: "Members",
                newName: "School");

            migrationBuilder.RenameColumn(
                name: "ControlNumber",
                table: "Members",
                newName: "Weight");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Members",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate",
                table: "Members",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsStudent",
                table: "Members",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTimeout",
                table: "Members",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LockerNumber",
                table: "Members",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MedicalCondition",
                table: "Members",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Plan",
                table: "Members",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Members",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
