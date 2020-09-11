using CostAnalysis.Models;
using NUnit.Framework;
using System;

namespace CostAnalysis.Tests
{
    [TestFixture]
    public class DayTests
    {
        [Test]
        public void Day_DataConverted_Test()
        {
            var testDay = new Day();
            var nowDate = DateTime.Now;

            testDay.Date = nowDate;

            var nowDateString = nowDate.ToString("yyyy-MM-dd");
            var testDateString = testDay.Date.ToString("yyyy-MM-dd");

            Assert.True(nowDateString.Equals(testDateString));
        }
    }
}