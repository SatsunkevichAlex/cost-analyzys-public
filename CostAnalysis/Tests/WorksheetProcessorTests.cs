using System.Collections.Generic;
using CostAnalysis.Models;
using NUnit.Framework;

namespace CostAnalysis.Tests
{
    [TestFixture]
    public class WorksheetProcessorTests
    {
        WorksheetProcessor pr;

        [SetUp]
        public void Init()
        {
            pr = new WorksheetProcessor();
        }

        [Test]
        public void WorksheetProcessor_GetDays_Test()
        {
            List<Day> days = pr.GetDays();

            Assert.IsTrue(days.Count == 365);
        }
    }
}
