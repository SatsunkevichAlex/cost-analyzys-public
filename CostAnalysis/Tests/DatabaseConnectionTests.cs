using System.Collections.Generic;
using System.Linq;
using CostAnalysis.DataSource;
using CostAnalysis.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CostAnalysis.Tests
{
    [TestFixture]
    public class DatabaseConnectionTests
    {
        DbContextOptionsBuilder<ApplicationContext> optionsBuilder;
        DbContextOptions<ApplicationContext> options;
        string connectionString;

        [SetUp]
        public void Init()
        {
            string dbPath = AppConstants.testDbPath;
            connectionString = $"Data Source={dbPath};";
            optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            options = optionsBuilder.UseSqlite(connectionString).Options;
        }

        [Test]
        public void ApplicationConnect_ConnectToDb_Test()
        {
            const double testTotal = 100.12345;

            using (ApplicationContext db = new ApplicationContext(options))
            {
                Day testDay = new Day(total: testTotal);
                db.Days.Add(testDay);
                db.SaveChanges();
            }

            using (ApplicationContext db = new ApplicationContext(options))
            {
                List<Day> days = db.Days.ToList();

                Day actualDay = days.SingleOrDefault(it => it.Total == testTotal);

                Assert.True(actualDay.Total == testTotal);
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                db.Database.EnsureDeleted();
                db.SaveChanges();
            }
        }
    }
}