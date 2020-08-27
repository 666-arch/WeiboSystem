using System;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Core.Models
{
    public class WeiBoDbContext:DbContext
    {
        public WeiBoDbContext(DbContextOptions<WeiBoDbContext> options)
            :base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=WeiBoSysDb;Integrated Security=True");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ReplyComments> ReplyCommentses { get; set; }
        public DbSet<PhotoLike> PhotoLikes { get; set; }
        public DbSet<PhotoDetails> PhotoDetailses { get; set; }
        public DbSet<PhotoCollection> PhotoCollections { get; set; }
        public DbSet<PhotoAlbum> PhotoAlbums { get; set; }
        public DbSet<MicroLike> MicroLikes { get; set; }

        public DbSet<MicroComments> MicroCommentses { get; set; }
        public DbSet<MicroFans> MicroFanses { get; set; }
        public DbSet<MicroBlog> MicroBlogs { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MicroComments>()
                .HasOne(x => x.User);
            
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    RealName = "王五",
                    UserName = "Jack",
                    Email = "1437855583@qq.com",
                    Password = "123456",
                    Gender = Gender.男,
                    PersonalElucidation = "暂无",
                    ImagePath = "default.png"
                },
                new User
                {
                    RealName = "张三",
                    UserName = "Bob",
                    Email = "25161531@qq.com",
                    Password = "123456",
                    Gender = Gender.女,
                    PersonalElucidation = "暂无",
                    ImagePath = "default.png"
                },
                new User
                {
                    RealName = "李四",
                    UserName = "Mike",
                    Email = "156431515@qq.com",
                    Password = "123456",
                    Gender = Gender.男,
                    PersonalElucidation = "暂无",
                    ImagePath = "default.png"
                });
        }
    }
}
