using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Core.Models.Migrations
{
    public partial class ChangeGender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("02d42325-6bc9-4903-a427-88446dbf0193"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("819232fa-a199-42d5-bf2f-c9b1d56a1b47"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d0e33796-8c88-4b11-a6cd-4026084427fa"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateTime", "DateOfBirth", "Email", "Gender", "ImagePath", "IsRemoved", "Password", "PersonalElucidation", "RealName", "UserName" },
                values: new object[] { new Guid("02013f01-0b77-4d92-989e-e2cadd1d2f66"), new DateTime(2020, 8, 24, 9, 19, 58, 14, DateTimeKind.Local).AddTicks(1374), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1437855583@qq.com", 1, "default.png", false, "123456", "暂无", "王五", "Jack" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateTime", "DateOfBirth", "Email", "Gender", "ImagePath", "IsRemoved", "Password", "PersonalElucidation", "RealName", "UserName" },
                values: new object[] { new Guid("c5cb8748-4da9-4e18-8cc0-9e181f6fe598"), new DateTime(2020, 8, 24, 9, 19, 58, 16, DateTimeKind.Local).AddTicks(1375), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "25161531@qq.com", 2, "default.png", false, "123456", "暂无", "张三", "Bob" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateTime", "DateOfBirth", "Email", "Gender", "ImagePath", "IsRemoved", "Password", "PersonalElucidation", "RealName", "UserName" },
                values: new object[] { new Guid("fab990a7-a72b-492e-9924-8c9c347b1d54"), new DateTime(2020, 8, 24, 9, 19, 58, 16, DateTimeKind.Local).AddTicks(1375), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "156431515@qq.com", 1, "default.png", false, "123456", "暂无", "李四", "Mike" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("02013f01-0b77-4d92-989e-e2cadd1d2f66"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c5cb8748-4da9-4e18-8cc0-9e181f6fe598"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fab990a7-a72b-492e-9924-8c9c347b1d54"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateTime", "DateOfBirth", "Email", "Gender", "ImagePath", "IsRemoved", "Password", "PersonalElucidation", "RealName", "UserName" },
                values: new object[] { new Guid("02d42325-6bc9-4903-a427-88446dbf0193"), new DateTime(2020, 8, 23, 12, 54, 13, 380, DateTimeKind.Local).AddTicks(51), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1437855583@qq.com", 1, "default.png", false, "123456", "暂无", "王五", "Jack" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateTime", "DateOfBirth", "Email", "Gender", "ImagePath", "IsRemoved", "Password", "PersonalElucidation", "RealName", "UserName" },
                values: new object[] { new Guid("d0e33796-8c88-4b11-a6cd-4026084427fa"), new DateTime(2020, 8, 23, 12, 54, 13, 381, DateTimeKind.Local).AddTicks(52), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "25161531@qq.com", 2, "default.png", false, "123456", "暂无", "张三", "Bob" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateTime", "DateOfBirth", "Email", "Gender", "ImagePath", "IsRemoved", "Password", "PersonalElucidation", "RealName", "UserName" },
                values: new object[] { new Guid("819232fa-a199-42d5-bf2f-c9b1d56a1b47"), new DateTime(2020, 8, 23, 12, 54, 13, 382, DateTimeKind.Local).AddTicks(52), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "156431515@qq.com", 1, "default.png", false, "123456", "暂无", "李四", "Mike" });
        }
    }
}
