using CostAnalysis.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace CostAnalysis.Tests
{
    [TestFixture]
    public class SQLiteProcessorTests
    {
        string connectionSring;
        SQLiteProcessor pr;

        [SetUp]
        public void Init()
        {
            //string testDbPath = AppConstants.testDbPath;
            string dbPath = AppConstants.testDbPath;
            connectionSring = $"Data Source={dbPath};";
            pr = new SQLiteProcessor(connectionSring);
        }

        [Test]
        public void SQLiteProcessor_InsertDay_Test()
        {
            double testTotal = 123.456;
            Day testDay = new Day(testTotal)
            {
                Shops = new List<KeyValuePair<string, double>>
                {
                    new KeyValuePair<string, double>("хлеб", 1.5),
                    new KeyValuePair<string, double>("молоко", 2),
                }
            };

            pr.InsertDayToDatabase(testDay);
            Day returnedDay = pr.GetDay(testDay);

            Assert.True(testDay.Equals(returnedDay));
        }

        // This is not acutally test. It used for inserting data from excel sheet to db
        //[Test]
        public void SQLiteProcessor_SaveAllShopsInFile_Test()
        {
            pr.InsertAllDaysToDatabase();

            //pr.SaveAllShopsInFile();
        }
    }
}