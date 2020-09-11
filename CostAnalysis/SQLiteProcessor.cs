using CostAnalysis.DataSource;
using CostAnalysis.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CostAnalysis
{
    public class SQLiteProcessor
    {
        private readonly DbContextOptionsBuilder<ApplicationContext> _optionsBuilder;
        private readonly DbContextOptions<ApplicationContext> _options;

        public SQLiteProcessor(string connectionString)
        {
            _optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            _options = _optionsBuilder.UseSqlite(connectionString).UseLoggerFactory(ApplicationContext.MyLoggerFactory).Options;
        }

        public void InsertAllDaysToDatabase()
        {
            List<Day> days = new WorksheetProcessor().GetDays();

            using (ApplicationContext db = new ApplicationContext(_options))
            {
                db.Days.AddRange(days);
                db.SaveChanges();
            }
        }

        public void InsertDayToDatabase(Day day)
        {
            using (ApplicationContext db = new ApplicationContext(_options))
            {
                db.Days.Add(day);
                db.SaveChanges();
            }
        }

        public Day GetDay(Day dayToGet)
        {
            Day result;
            using (ApplicationContext db = new ApplicationContext(_options))
            {
                result = db.Days.SingleOrDefault(it => it.Equals(dayToGet));
            }
            return result;
        }

        public List<Day> GetDays()
        {
            List<Day> result;
            using (ApplicationContext db = new ApplicationContext(_options))
            {
                result = db.Days.ToList();
            }
            return result;
        }

        public void SaveAllShopsInFile()
        {
            List<Day> days = GetDays();
            string[] shopsNames = GetNameOfShops(days);
            string filename = AppConstants.shopsNamesPath;
            for (int i = 0; i < shopsNames.Count(); i++)
            {
                File.AppendAllText(filename, shopsNames[i] + Environment.NewLine);
            }
        }

        public List<KeyValuePair<string, double>> GetShops(List<Day> days)
        {
            List<KeyValuePair<string, double>> result = new List<KeyValuePair<string, double>>();

            foreach (Day day in days)
            {
                foreach (KeyValuePair<string, double> shop in day.Shops)
                {
                    result.Add(shop);
                }
            }

            return result;
        }

        public string[] GetNameOfShops(List<Day> days)
        {
            List<KeyValuePair<string, double>> shops = GetShops(days);
            string[] result = shops.Select(it => it.Key.ToLower().Replace("  ", " ").Trim()).Distinct().ToArray();
            Array.Sort(result);
            return result;
        }       
    }
}