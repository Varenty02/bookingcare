using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookingcare.Migrations
{
    public partial class migrate_position_gender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "47879a94-ab33-419f-83c5-9bd265e4bad7");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6f3fac44-3656-4f34-9be4-5021c5ffadf2");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "773cb3f2-0948-47ee-8cb0-672b2d6adffb");

            migrationBuilder.DeleteData(
                table: "TimeTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TimeTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TimeTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TimeTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TimeTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TimeTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TimeTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TimeTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueVie = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueVie = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "Value", "ValueVie" },
                values: new object[,]
                {
                    { 1, "Male", "Nam" },
                    { 2, "Female", "Nữ" },
                    { 3, "Other", "Khác" }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "Value", "ValueVie" },
                values: new object[,]
                {
                    { 1, "None", "Bác sĩ" },
                    { 2, "Master", "Thạc sĩ" },
                    { 3, "Doctor", "Tiến sĩ" },
                    { 4, "Associate Professor", "Phó giáo sư" },
                    { 5, "Professor", "Giáo sư" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1d60294d-2899-48db-ad25-13faf91f7880", "2", "User", "User" },
                    { "4adb50b6-f8ef-4a6c-8831-2fa9f758548e", "3", "Doctor", "Doctor" },
                    { "541e6be6-1953-47cd-a9b7-9144d58d0d7a", "1", "Admin", "Admin" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "Positions");

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

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "47879a94-ab33-419f-83c5-9bd265e4bad7", "1", "Admin", "Admin" },
                    { "6f3fac44-3656-4f34-9be4-5021c5ffadf2", "3", "Doctor", "Doctor" },
                    { "773cb3f2-0948-47ee-8cb0-672b2d6adffb", "2", "User", "User" }
                });

            migrationBuilder.InsertData(
                table: "TimeTypes",
                columns: new[] { "Id", "Value", "ValueVie" },
                values: new object[,]
                {
                    { 1, "8:00 AM - 9:00 AM", "8:00 - 9:00" },
                    { 2, "9:00 AM - 10:00 AM", "9:00 - 10:00" },
                    { 3, "10:00 AM - 11:00 AM", "10:00 - 11:00" },
                    { 4, "11:00 AM - 0:00 PM", "11:00 - 12:00" },
                    { 5, "1:00 PM - 2:00 PM", "13:00 - 14:00" },
                    { 6, "2:00 PM - 3:00 PM", "14:00 - 15:00" },
                    { 7, "3:00 PM - 4:00 PM", "15:00 - 16:00" },
                    { 8, "4:00 PM - 5:00 PM", "16:00 - 17:00" }
                });
        }
    }
}
