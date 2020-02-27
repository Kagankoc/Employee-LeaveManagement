using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Employee_LeaveManagement.Data.Migrations
{
    public partial class AddedRepositories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LeaveTypes",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RequestingEmployeeId",
                table: "LeaveHistories",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "ApprovedById",
                table: "LeaveHistories",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<bool>(
                name: "Approved",
                table: "LeaveHistories",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "LeaveAllocations",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateJoined",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveHistories_ApprovedById",
                table: "LeaveHistories",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveHistories_RequestingEmployeeId",
                table: "LeaveHistories",
                column: "RequestingEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocations_EmployeeId",
                table: "LeaveAllocations",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_AspNetUsers_EmployeeId",
                table: "LeaveAllocations",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveHistories_AspNetUsers_ApprovedById",
                table: "LeaveHistories",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveHistories_AspNetUsers_RequestingEmployeeId",
                table: "LeaveHistories",
                column: "RequestingEmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocations_AspNetUsers_EmployeeId",
                table: "LeaveAllocations");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveHistories_AspNetUsers_ApprovedById",
                table: "LeaveHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveHistories_AspNetUsers_RequestingEmployeeId",
                table: "LeaveHistories");

            migrationBuilder.DropIndex(
                name: "IX_LeaveHistories_ApprovedById",
                table: "LeaveHistories");

            migrationBuilder.DropIndex(
                name: "IX_LeaveHistories_RequestingEmployeeId",
                table: "LeaveHistories");

            migrationBuilder.DropIndex(
                name: "IX_LeaveAllocations_EmployeeId",
                table: "LeaveAllocations");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateJoined",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LeaveTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<Guid>(
                name: "RequestingEmployeeId",
                table: "LeaveHistories",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ApprovedById",
                table: "LeaveHistories",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Approved",
                table: "LeaveHistories",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                table: "LeaveAllocations",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
