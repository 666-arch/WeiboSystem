using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Core.Models.Migrations
{
    public partial class WeiBo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    RealName = table.Column<string>(type: "varchar(40)", nullable: true),
                    UserName = table.Column<string>(type: "varchar(30)", nullable: true),
                    Email = table.Column<string>(type: "varchar(40)", nullable: false),
                    Password = table.Column<string>(type: "varchar(30)", nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    PersonalElucidation = table.Column<string>(type: "varchar(500)", nullable: true),
                    ImagePath = table.Column<string>(type: "varchar(300)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MicroBlogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    MicroContent = table.Column<string>(type: "ntext", nullable: false),
                    MicroImagePath = table.Column<string>(type: "varchar(800)", nullable: true),
                    MicroVideo = table.Column<string>(type: "varchar(1000)", nullable: true),
                    MicroLikeCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MicroBlogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MicroBlogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MicroFanses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    FocusUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MicroFanses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MicroFanses_Users_FocusUserId",
                        column: x => x.FocusUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MicroFanses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhotoAlbums",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    PhotoAlbumName = table.Column<string>(type: "varchar(40)", nullable: false),
                    PhotoPermissions = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoAlbums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoAlbums_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MicroCommentses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    CommentsContent = table.Column<string>(type: "varchar(500)", nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    MicroBlogId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MicroCommentses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MicroCommentses_MicroBlogs_MicroBlogId",
                        column: x => x.MicroBlogId,
                        principalTable: "MicroBlogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MicroCommentses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MicroLikes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    MicroBlogId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true),
                    LikeSign = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MicroLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MicroLikes_MicroBlogs_MicroBlogId",
                        column: x => x.MicroBlogId,
                        principalTable: "MicroBlogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MicroLikes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhotoCollections",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    PhotoAlbumId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoCollections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoCollections_PhotoAlbums_PhotoAlbumId",
                        column: x => x.PhotoAlbumId,
                        principalTable: "PhotoAlbums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhotoCollections_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhotoDetailses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    ImagePaths = table.Column<string>(type: "varchar(300)", nullable: false),
                    PhotoAlbumId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoDetailses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoDetailses_PhotoAlbums_PhotoAlbumId",
                        column: x => x.PhotoAlbumId,
                        principalTable: "PhotoAlbums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhotoLikes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    PhotoAlbumId = table.Column<Guid>(nullable: true),
                    LikeSign = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoLikes_PhotoAlbums_PhotoAlbumId",
                        column: x => x.PhotoAlbumId,
                        principalTable: "PhotoAlbums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhotoLikes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReplyCommentses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    ReplyContent = table.Column<string>(type: "varchar(500)", nullable: false),
                    RespondUserId = table.Column<Guid>(nullable: true),
                    RespondTargetUserId = table.Column<Guid>(nullable: true),
                    ReplyType = table.Column<int>(nullable: false),
                    MicroCommentId = table.Column<Guid>(nullable: true),
                    ReplyToTargetCommentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplyCommentses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReplyCommentses_MicroCommentses_MicroCommentId",
                        column: x => x.MicroCommentId,
                        principalTable: "MicroCommentses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReplyCommentses_Users_RespondTargetUserId",
                        column: x => x.RespondTargetUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReplyCommentses_Users_RespondUserId",
                        column: x => x.RespondUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateTime", "DateOfBirth", "Email", "Gender", "ImagePath", "IsRemoved", "Password", "PersonalElucidation", "RealName", "UserName" },
                values: new object[] { new Guid("65b5f9e6-2f89-4dfc-a27c-caf23afd2db4"), new DateTime(2020, 8, 28, 22, 12, 43, 110, DateTimeKind.Local).AddTicks(6315), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1437855583@qq.com", 1, "default.png", false, "123456", "暂无", "王五", "Jack" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateTime", "DateOfBirth", "Email", "Gender", "ImagePath", "IsRemoved", "Password", "PersonalElucidation", "RealName", "UserName" },
                values: new object[] { new Guid("7bcf89e5-cef9-4f2f-b897-dff4b39891d7"), new DateTime(2020, 8, 28, 22, 12, 43, 111, DateTimeKind.Local).AddTicks(6316), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "25161531@qq.com", 2, "default.png", false, "123456", "暂无", "张三", "Bob" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateTime", "DateOfBirth", "Email", "Gender", "ImagePath", "IsRemoved", "Password", "PersonalElucidation", "RealName", "UserName" },
                values: new object[] { new Guid("dcc33546-3e6c-400d-9061-d6e7c8150414"), new DateTime(2020, 8, 28, 22, 12, 43, 111, DateTimeKind.Local).AddTicks(6316), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "156431515@qq.com", 1, "default.png", false, "123456", "暂无", "李四", "Mike" });

            migrationBuilder.CreateIndex(
                name: "IX_MicroBlogs_UserId",
                table: "MicroBlogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MicroCommentses_MicroBlogId",
                table: "MicroCommentses",
                column: "MicroBlogId");

            migrationBuilder.CreateIndex(
                name: "IX_MicroCommentses_UserId",
                table: "MicroCommentses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MicroFanses_FocusUserId",
                table: "MicroFanses",
                column: "FocusUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MicroFanses_UserId",
                table: "MicroFanses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MicroLikes_MicroBlogId",
                table: "MicroLikes",
                column: "MicroBlogId");

            migrationBuilder.CreateIndex(
                name: "IX_MicroLikes_UserId",
                table: "MicroLikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoAlbums_UserId",
                table: "PhotoAlbums",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoCollections_PhotoAlbumId",
                table: "PhotoCollections",
                column: "PhotoAlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoCollections_UserId",
                table: "PhotoCollections",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoDetailses_PhotoAlbumId",
                table: "PhotoDetailses",
                column: "PhotoAlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoLikes_PhotoAlbumId",
                table: "PhotoLikes",
                column: "PhotoAlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoLikes_UserId",
                table: "PhotoLikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyCommentses_MicroCommentId",
                table: "ReplyCommentses",
                column: "MicroCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyCommentses_RespondTargetUserId",
                table: "ReplyCommentses",
                column: "RespondTargetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyCommentses_RespondUserId",
                table: "ReplyCommentses",
                column: "RespondUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MicroFanses");

            migrationBuilder.DropTable(
                name: "MicroLikes");

            migrationBuilder.DropTable(
                name: "PhotoCollections");

            migrationBuilder.DropTable(
                name: "PhotoDetailses");

            migrationBuilder.DropTable(
                name: "PhotoLikes");

            migrationBuilder.DropTable(
                name: "ReplyCommentses");

            migrationBuilder.DropTable(
                name: "PhotoAlbums");

            migrationBuilder.DropTable(
                name: "MicroCommentses");

            migrationBuilder.DropTable(
                name: "MicroBlogs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
