using Microsoft.EntityFrameworkCore.Migrations;

namespace Solution.FrontEnd.Data.Migrations
{
    public partial class RoleSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "82873dba-5609-4ea3-83cf-6620ad750189", "6d294c91-4446-4f37-95a4-b88f9b276186", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2f6649bc-5abf-4ee0-a83b-aee8166a8198", "1be02328-e346-458a-a4bb-800846012593", "Oferente", "OFERENTE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "617b7a4a-3619-4a8e-a200-650bb1f650e7", "6118d357-b4b2-4747-b976-588c1184acf9", "Empleador", "EMPLEADOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f6649bc-5abf-4ee0-a83b-aee8166a8198");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "617b7a4a-3619-4a8e-a200-650bb1f650e7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82873dba-5609-4ea3-83cf-6620ad750189");
        }
    }
}
