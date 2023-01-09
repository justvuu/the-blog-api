using System;
using Microsoft.EntityFrameworkCore;
using TheBlogAPI.Models.Entities;

namespace TheBlogAPI.Data
{
    public class TheBlogDbContext : DbContext
    {
        public TheBlogDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Category> Categories { set; get; }

        public DbSet<User> Users { get; set; }

        public DbSet<Vocabulary> Vocabulary { get; set; }

        public DbSet<VocabCategory> VocabCategory { get; set; }
    }
}

