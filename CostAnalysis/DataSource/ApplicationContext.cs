using CostAnalysis.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CostAnalysis.DataSource
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<Day> Days { get; set; }
        public DbSet<User> Users { get; set; }

        public static readonly ILoggerFactory MyLoggerFactory =
            LoggerFactory.Create(builder =>
            {
                builder.AddProvider(new MyLoggerProvider());
            });

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Day>().Property(d => d.Shops).HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<KeyValuePair<string, double>>>(v)
                );
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}