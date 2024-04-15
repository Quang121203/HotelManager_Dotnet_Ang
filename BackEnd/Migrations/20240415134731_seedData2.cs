using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class seedData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3413c0f4-0670-4731-9b20-154cb942fa32", "4592f05e-503f-4481-8d4a-7943762dcf2b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3413c0f4-0670-4731-9b20-154cb942fa32");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4592f05e-503f-4481-8d4a-7943762dcf2b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1200df0d-ecc4-4b83-a8ed-39f3b7eeb966", null, "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateJoined", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ea6ba637-a9f9-4380-aa9a-51eec8ccc967", 0, "47ea1fe1-454c-4eb6-b431-12179dca53c3", new DateTimeOffset(new DateTime(2024, 4, 15, 20, 47, 30, 851, DateTimeKind.Unspecified).AddTicks(5148), new TimeSpan(0, 7, 0, 0, 0)), "admin@gmail.com", false, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEBpoR0h9JQ3L+TDONmRlaW3gvDgU+PHt2DobXkwz1jnTxzKjx8O0ypmTsYVdsps/MQ==", null, false, "9289e411-0e46-4309-bfdc-9f1842f5f27a", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1200df0d-ecc4-4b83-a8ed-39f3b7eeb966", "ea6ba637-a9f9-4380-aa9a-51eec8ccc967" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1200df0d-ecc4-4b83-a8ed-39f3b7eeb966", "ea6ba637-a9f9-4380-aa9a-51eec8ccc967" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1200df0d-ecc4-4b83-a8ed-39f3b7eeb966");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ea6ba637-a9f9-4380-aa9a-51eec8ccc967");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3413c0f4-0670-4731-9b20-154cb942fa32", null, "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateJoined", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4592f05e-503f-4481-8d4a-7943762dcf2b", 0, "e1c55d98-2aad-493a-9747-145681a145ca", new DateTimeOffset(new DateTime(2024, 4, 15, 20, 34, 36, 97, DateTimeKind.Unspecified).AddTicks(1036), new TimeSpan(0, 7, 0, 0, 0)), "admin@gmail.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEJ/8t+HviE9wNopAy+k8OuN6g22MUq0sIuVCg23K8q9/aTiyvA2qYwriuUe8MgT3fQ==", null, false, "bdac454c-8627-4405-9e00-3eb20deebb5b", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3413c0f4-0670-4731-9b20-154cb942fa32", "4592f05e-503f-4481-8d4a-7943762dcf2b" });
        }
    }
}
