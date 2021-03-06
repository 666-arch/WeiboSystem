﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.Core.Models;

namespace WebApi.Core.Models.Migrations
{
    [DbContext(typeof(WeiBoDbContext))]
    [Migration("20200828141243_WeiBo")]
    partial class WeiBo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApi.Core.Models.MicroBlog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("MicroContent")
                        .IsRequired()
                        .HasColumnType("ntext");

                    b.Property<string>("MicroImagePath")
                        .HasColumnType("varchar(800)");

                    b.Property<int>("MicroLikeCount")
                        .HasColumnType("int");

                    b.Property<string>("MicroVideo")
                        .HasColumnType("varchar(1000)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("MicroBlogs");
                });

            modelBuilder.Entity("WebApi.Core.Models.MicroComments", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CommentsContent")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<Guid?>("MicroBlogId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MicroBlogId");

                    b.HasIndex("UserId");

                    b.ToTable("MicroCommentses");
                });

            modelBuilder.Entity("WebApi.Core.Models.MicroFans", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("FocusUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FocusUserId");

                    b.HasIndex("UserId");

                    b.ToTable("MicroFanses");
                });

            modelBuilder.Entity("WebApi.Core.Models.MicroLike", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<bool>("LikeSign")
                        .HasColumnType("bit");

                    b.Property<Guid?>("MicroBlogId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MicroBlogId");

                    b.HasIndex("UserId");

                    b.ToTable("MicroLikes");
                });

            modelBuilder.Entity("WebApi.Core.Models.PhotoAlbum", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("PhotoAlbumName")
                        .IsRequired()
                        .HasColumnType("varchar(40)");

                    b.Property<bool>("PhotoPermissions")
                        .HasColumnType("bit");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PhotoAlbums");
                });

            modelBuilder.Entity("WebApi.Core.Models.PhotoCollection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<Guid?>("PhotoAlbumId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PhotoAlbumId");

                    b.HasIndex("UserId");

                    b.ToTable("PhotoCollections");
                });

            modelBuilder.Entity("WebApi.Core.Models.PhotoDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagePaths")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<Guid?>("PhotoAlbumId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PhotoAlbumId");

                    b.ToTable("PhotoDetailses");
                });

            modelBuilder.Entity("WebApi.Core.Models.PhotoLike", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<bool>("LikeSign")
                        .HasColumnType("bit");

                    b.Property<Guid?>("PhotoAlbumId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PhotoAlbumId");

                    b.HasIndex("UserId");

                    b.ToTable("PhotoLikes");
                });

            modelBuilder.Entity("WebApi.Core.Models.ReplyComments", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<Guid?>("MicroCommentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ReplyContent")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<Guid>("ReplyToTargetCommentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ReplyType")
                        .HasColumnType("int");

                    b.Property<Guid?>("RespondTargetUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RespondUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MicroCommentId");

                    b.HasIndex("RespondTargetUserId");

                    b.HasIndex("RespondUserId");

                    b.ToTable("ReplyCommentses");
                });

            modelBuilder.Entity("WebApi.Core.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(40)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("ImagePath")
                        .HasColumnType("varchar(300)");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("PersonalElucidation")
                        .HasColumnType("varchar(500)");

                    b.Property<string>("RealName")
                        .HasColumnType("varchar(40)");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("65b5f9e6-2f89-4dfc-a27c-caf23afd2db4"),
                            CreateTime = new DateTime(2020, 8, 28, 22, 12, 43, 110, DateTimeKind.Local).AddTicks(6315),
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "1437855583@qq.com",
                            Gender = 1,
                            ImagePath = "default.png",
                            IsRemoved = false,
                            Password = "123456",
                            PersonalElucidation = "暂无",
                            RealName = "王五",
                            UserName = "Jack"
                        },
                        new
                        {
                            Id = new Guid("7bcf89e5-cef9-4f2f-b897-dff4b39891d7"),
                            CreateTime = new DateTime(2020, 8, 28, 22, 12, 43, 111, DateTimeKind.Local).AddTicks(6316),
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "25161531@qq.com",
                            Gender = 2,
                            ImagePath = "default.png",
                            IsRemoved = false,
                            Password = "123456",
                            PersonalElucidation = "暂无",
                            RealName = "张三",
                            UserName = "Bob"
                        },
                        new
                        {
                            Id = new Guid("dcc33546-3e6c-400d-9061-d6e7c8150414"),
                            CreateTime = new DateTime(2020, 8, 28, 22, 12, 43, 111, DateTimeKind.Local).AddTicks(6316),
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "156431515@qq.com",
                            Gender = 1,
                            ImagePath = "default.png",
                            IsRemoved = false,
                            Password = "123456",
                            PersonalElucidation = "暂无",
                            RealName = "李四",
                            UserName = "Mike"
                        });
                });

            modelBuilder.Entity("WebApi.Core.Models.MicroBlog", b =>
                {
                    b.HasOne("WebApi.Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WebApi.Core.Models.MicroComments", b =>
                {
                    b.HasOne("WebApi.Core.Models.MicroBlog", "MicroBlog")
                        .WithMany()
                        .HasForeignKey("MicroBlogId");

                    b.HasOne("WebApi.Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WebApi.Core.Models.MicroFans", b =>
                {
                    b.HasOne("WebApi.Core.Models.User", "FocusUser")
                        .WithMany()
                        .HasForeignKey("FocusUserId");

                    b.HasOne("WebApi.Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WebApi.Core.Models.MicroLike", b =>
                {
                    b.HasOne("WebApi.Core.Models.MicroBlog", "MicroBlog")
                        .WithMany()
                        .HasForeignKey("MicroBlogId");

                    b.HasOne("WebApi.Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WebApi.Core.Models.PhotoAlbum", b =>
                {
                    b.HasOne("WebApi.Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WebApi.Core.Models.PhotoCollection", b =>
                {
                    b.HasOne("WebApi.Core.Models.PhotoAlbum", "PhotoAlbum")
                        .WithMany()
                        .HasForeignKey("PhotoAlbumId");

                    b.HasOne("WebApi.Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WebApi.Core.Models.PhotoDetails", b =>
                {
                    b.HasOne("WebApi.Core.Models.PhotoAlbum", "PhotoAlbum")
                        .WithMany()
                        .HasForeignKey("PhotoAlbumId");
                });

            modelBuilder.Entity("WebApi.Core.Models.PhotoLike", b =>
                {
                    b.HasOne("WebApi.Core.Models.PhotoAlbum", "PhotoAlbum")
                        .WithMany()
                        .HasForeignKey("PhotoAlbumId");

                    b.HasOne("WebApi.Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WebApi.Core.Models.ReplyComments", b =>
                {
                    b.HasOne("WebApi.Core.Models.MicroComments", "MicroComments")
                        .WithMany()
                        .HasForeignKey("MicroCommentId");

                    b.HasOne("WebApi.Core.Models.User", "Users")
                        .WithMany()
                        .HasForeignKey("RespondTargetUserId");

                    b.HasOne("WebApi.Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("RespondUserId");
                });
#pragma warning restore 612, 618
        }
    }
}
