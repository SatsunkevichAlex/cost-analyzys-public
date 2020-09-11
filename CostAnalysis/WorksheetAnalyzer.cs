using System;
using CostAnalysis.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CostAnalysis
{
    public class WorksheetAnalyzer
    {
        private readonly SQLiteProcessor _sqlProcessor;

        public WorksheetAnalyzer()
        {
            _sqlProcessor = new SQLiteProcessor(AppConstants.dbConnectionString);
        }

        public double GetTotalSpent()
        {
            List<Day> days = _sqlProcessor.GetDays();
            double result = days.Sum(it => it.Shops.Sum(s => s.Value));
            return result;
        }

        public double GetSpentForCategory(Category category)
        {
            string dictPath = AppConstants.GetCategoryDictPath(category);
            var categoryDictionary = File.ReadAllLines(dictPath);
            List<List<KeyValuePair<string, double>>> daysShops = _sqlProcessor.GetDays().Select(it => it.Shops).ToList();

            double sum = 0;
            foreach (List<KeyValuePair<string, double>> day in daysShops)
            {
                IEnumerable<KeyValuePair<string, double>> shopsForCategory = day.Where(it => categoryDictionary.Any(d => d.Equals(it.Key)));
                if (shopsForCategory.Count() != 0)
                {
                    sum += shopsForCategory.Sum(it => it.Value);
                }
            }

            return sum;
        }

        public List<Day> GetDaysBetweenDates(DateTime from, DateTime to)
        {
            List<Day> result = _sqlProcessor.GetDays().Where(it => it.Date >= from && it.Date <= to).ToList();
            return result;
        }

        public double GetSpentInCatogoryBetweenDates(DateTime from, DateTime to, Category category)
        {
            string dictPath = AppConstants.GetCategoryDictPath(category);
            var categoryDictionary = File.ReadAllLines(dictPath);

            List<List<KeyValuePair<string, double>>> daysInDates = GetDaysBetweenDates(from, to).Select(it => it.Shops).ToList();

            double sum = 0;
            foreach (List<KeyValuePair<string, double>> day in daysInDates)
            {
                sum += day.Where(it => categoryDictionary.Contains(it.Key)).Sum(it => it.Value);
            }

            return sum;
        }

        public int GetItemCount(string shopName)
        {
            List<Day> days = _sqlProcessor.GetDays();
            List<List<KeyValuePair<string, double>>> daysShops = days.Select(it => it.Shops).ToList();
            int result = 0;
            for (int i = 0; i < daysShops.Count; i++)
            {
                for (int j = 0; j < daysShops[i].Count; j++)
                {
                    if (daysShops[i][j].Key.Contains(shopName))
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        public double GetTotalSpentForItem(string shopName)
        {
            List<Day> days = _sqlProcessor.GetDays();
            List<List<KeyValuePair<string, double>>> daysShops = days.Select(it => it.Shops).ToList();

            double result = 0;
            for (int i = 0; i < daysShops.Count; i++)
            {
                for (int j = 0; j < daysShops[i].Count; j++)
                {
                    if (daysShops[i][j].Key.Contains(shopName))
                    {
                        result += daysShops[i][j].Value;
                    }
                }
            }
            return Math.Round(result, 2);
        }

        public Day GetMaxDay()
        {
            var days = _sqlProcessor.GetDays();
            var maxTotal = days.Max(it => it.Total);
            return _sqlProcessor.GetDays().First(it => it.Total == maxTotal);
        }

        public Day GetDayByDate(DateTime date)
        {
            return _sqlProcessor.GetDays().Single(it =>
            it.Date.Year == date.Year &&
            it.Date.Month == date.Month &&
            it.Date.Day == date.Day);
        }

        public int GetTotalDaysCount()
        {
            return _sqlProcessor.GetDays().Count();
        }

        public double GetDaysTotalAverage()
        {
            var average = _sqlProcessor.GetDays().Average(it => it.Total);
            return Math.Round(average, 2);
        }

        public string[] GetDictDifference()
        {
            string[] alco = File.ReadAllLines(AppConstants.GetCategoryDictPath(Category.ALCO));
            string[] enter = File.ReadAllLines(AppConstants.GetCategoryDictPath(Category.ENETERT));
            string[] food = File.ReadAllLines(AppConstants.GetCategoryDictPath(Category.FOOD));
            string[] house = File.ReadAllLines(AppConstants.GetCategoryDictPath(Category.HOUSE));
            string[] allFromDict = alco.Concat(enter).Concat(food).Concat(house).ToArray();

            string[] allItems = File.ReadAllLines(AppConstants.shopsNamesPath);

            string[] diff = allItems.Except(allFromDict).ToArray();
            return diff;
        }
    }
}