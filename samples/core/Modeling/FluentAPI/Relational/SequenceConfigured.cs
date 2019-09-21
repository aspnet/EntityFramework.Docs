﻿using Microsoft.EntityFrameworkCore;

namespace EFModeling.FluentAPI.Relational.SequenceConfigured
{
    class MyContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("OrderNumbers", schema: "shared")
                .StartsAt(1000)
                .IncrementsBy(5);
        }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public int OrderNo { get; set; }
        public string Url { get; set; }
    }
}
