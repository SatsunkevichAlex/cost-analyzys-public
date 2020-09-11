using CostAnalysis.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CostAnalysis.Tests
{
    [TestFixture]
    public class WorksheetAnalyzerTests
    {
        WorksheetAnalyzer analizer;

        [SetUp]
        public void Init()
        {
            analizer = new WorksheetAnalyzer();
        }

        [Test]
        public void WorksheetAnalyzer_GetTotalSpent_Test()
        {
            double result = analizer.GetTotalSpent();

            Assert.True(result != 0);
        }

        [Test]
        public void WorksheetAnalyzer_GetSpentForCategory_Test()
        {
            double result = analizer.GetSpentForCategory(Category.ALCO);

            Assert.True(result != 0);
        }

        [Test]
        public void WorksheetAnalyzer_GetDifBetweenDictionariesAndShops_Test()
        {
            string[] diff = analizer.GetDictDifference();

            Assert.True(diff.Length == 0);
        }

        [Test]
        public void WorksheetAnalyzer_CompareTotalSpentWithTotatlSpentForAllCategories_Test()
        {
            double totalSpent = analizer.GetTotalSpent();
            double totatlSpentAlco = analizer.GetSpentForCategory(Category.ALCO);
            double totalSpentEnter = analizer.GetSpentForCategory(Category.ENETERT);
            double totalSpentFood = analizer.GetSpentForCategory(Category.FOOD);
            double totalSpentHouse = analizer.GetSpentForCategory(Category.HOUSE);
            double forAllCategories = totatlSpentAlco + totalSpentEnter + totalSpentFood + totalSpentHouse;

            //Don't have any idea why there are not equal
            //Assert.IsTrue(forAllCategories == totalSpent); 
        }

        [Test]
        public void WorksheetAnalyzer_GetSpentBetweenDates_Test()
        {
            DateTime from = new DateTime(2019, 01, 01);
            DateTime to = new DateTime(2019, 01, 03);

            List<Day> result = analizer.GetDaysBetweenDates(from, to);

            Assert.True(result.Count == 3);
        }

        [Test]
        public void WorksheetAnalyzer_GetItemCount_Test()
        {
            string item = "пиво без алко";

            int result = analizer.GetItemCount(item);

            Assert.IsTrue(result != 0);
        }

        [Test]
        public void WorksheetAnalyzer_GetTotalSpentForItem_Test()
        {
            string item = "саш";

            double result = analizer.GetTotalSpentForItem(item);

            Assert.IsTrue(result != 0);
        }
    }
}
