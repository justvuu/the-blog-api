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

        public DbSet<Vocab> Vocab { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<Category> Category { set; get; }
        public DbSet<VocabSet> VocabSet { get; set; }
        public DbSet<Subscriber> Subscriber { get; set; }
    }
}

