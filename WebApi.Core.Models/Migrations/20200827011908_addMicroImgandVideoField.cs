using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Core.Models.Migrations
{
    public partial class addMicroImgandVideoField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "MicroImagePath",
                table: "MicroBlogs",
                type: "varchar(800)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MicroVideo",
                table: "MicroBlogs",
                type: "varchar(1000)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateTime", "DateOfBirth", "Email", "Gender", "ImagePath", "IsRemoved", "Password", "PersonalElucidation", "RealName", "UserName" },
                values: new object[] { new Guid("37480aee-1338-46e0-8fa0-31f4893a8ee8"), new DateTime(2020, 8, 27, 9, 19, 8, 126, DateTimeKind.Local).AddTicks(2149), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1437855583@qq.com", 1, "default.png", false, "123456", "暂无", "王五", "Jack" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateTime", "DateOfBirth", "Email", "Gender", "ImagePath", "IsRemoved", "Password", "PersonalElucidation", "RealName", "UserName" },
                values: new object[] { new Guid("310c70e0-b89f-4874-8e37-06e999b746a8"), new DateTime(2020, 8, 27, 9, 19, 8, 128, DateTimeKind.Local).AddTicks(2150), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "25161531@qq.com", 2, "default.png", false, "123456", "暂无", "张三", "Bob" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateTime", "DateOfBirth", "Email", "Gender", "ImagePath", "IsRemoved", "Password", "PersonalElucidation", "RealName", "UserName" },
                values: new object[] { new Guid("e091baf4-a9ca-4042-8940-c5b664548817"), new DateTime(2020, 8, 27, 9, 19, 8, 128, DateTimeKind.Local).AddTicks(2150), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "156431515@qq.com", 1, "default.png", false, "123456", "暂无", "李四", "Mike" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("310c70e0-b89f-4874-8e37-06e999b746a8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("37480aee-1338-46e0-8fa0-31f4893a8ee8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e091baf4-a9ca-4042-8940-c5b664548817"));

            migrationBuilder.DropColumn(
                name: "MicroImagePath",
                table: "MicroBlogs");

            migrationBuilder.DropColumn(
                name: "MicroVideo",
                table: "MicroBlogs");

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
    }
}
