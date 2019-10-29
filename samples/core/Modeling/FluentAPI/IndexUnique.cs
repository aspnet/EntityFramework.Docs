﻿using Microsoft.EntityFrameworkCore;

namespace EFModeling.FluentAPI.IndexUnique
{
    class MyContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region ModelBuilder
            modelBuilder.Entity<Blog>()
                .HasIndex(b => b.Url)
                .IsUnique();
            #endregion
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
    }
}
