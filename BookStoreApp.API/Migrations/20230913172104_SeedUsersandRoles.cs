using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApp.API.Migrations
{
    public partial class SeedUsersandRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2a1a417b-7950-4b34-ace7-b7895654daa2", "78d74acb-d8ce-40da-8549-da69cf59167d", "Administrator", "ADMINISTRATOR" },
                    { "846ef4e5-73a5-471c-bbd1-4105b2286e6f", "430a811d-8bf9-47b3-bdb0-fdb0424c1aae", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "5b14fa75-2eec-4884-96f0-ea7b09a627a8", 0, "f69f93e9-6322-484c-bc28-c60fa1a0ac45", "user@bookstore.com", false, "System", "User", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAEAACcQAAAAELspOfr33YAcu3YrXtPlgqxWoVqlS1IngB7R1CINJ+Hj2pVw9eGQMuQM6hgGQi/DpA==", null, false, "bf994ce6-b61f-4dac-95ab-c57b9fc573d3", false, "user@bookstore.com" },
                    { "76d17262-756a-4863-b372-88aecf2fa2fc", 0, "82baa4d6-f068-47a8-bd19-d8fbdaeb70df", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAEAACcQAAAAENIZOiVYakMFdWJRy1HvduHIpXyU7DXBd5XtEv3EFxyj5ZO/CLrbIfQCDVTjuWg0fA==", null, false, "5f8a51ff-2e2c-490e-87b3-69819a4669dd", false, "admin@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "846ef4e5-73a5-471c-bbd1-4105b2286e6f", "5b14fa75-2eec-4884-96f0-ea7b09a627a8" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2a1a417b-7950-4b34-ace7-b7895654daa2", "76d17262-756a-4863-b372-88aecf2fa2fc" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "846ef4e5-73a5-471c-bbd1-4105b2286e6f", "5b14fa75-2eec-4884-96f0-ea7b09a627a8" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2a1a417b-7950-4b34-ace7-b7895654daa2", "76d17262-756a-4863-b372-88aecf2fa2fc" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a1a417b-7950-4b34-ace7-b7895654daa2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "846ef4e5-73a5-471c-bbd1-4105b2286e6f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5b14fa75-2eec-4884-96f0-ea7b09a627a8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76d17262-756a-4863-b372-88aecf2fa2fc");
        }
    }
}
