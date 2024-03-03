using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookingcare.Migrations
{
    public partial class delete_gender_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "1d60294d-2899-48db-ad25-13faf91f7880");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "4adb50b6-f8ef-4a6c-8831-2fa9f758548e");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "541e6be6-1953-47cd-a9b7-9144d58d0d7a");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "64ae1c77-9e08-446a-880f-d815cb646b27", "3", "Doctor", "Doctor" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8251f0a1-c838-4d38-8ea5-94d1a0728aca", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b07ca236-d6b5-463b-8a29-e23cc1d18cce", "2", "User", "User" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "64ae1c77-9e08-446a-880f-d815cb646b27");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "8251f0a1-c838-4d38-8ea5-94d1a0728aca");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "b07ca236-d6b5-463b-8a29-e23cc1d18cce");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1d60294d-2899-48db-ad25-13faf91f7880", "2", "User", "User" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4adb50b6-f8ef-4a6c-8831-2fa9f758548e", "3", "Doctor", "Doctor" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "541e6be6-1953-47cd-a9b7-9144d58d0d7a", "1", "Admin", "Admin" });
        }
    }
}
