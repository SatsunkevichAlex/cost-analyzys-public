using CostAnalysis.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CostAnalysisApiTest
{
    [TestClass]    
    public class DaysControllerTests : BaseTest
    {
        [TestMethod]
        public void RootController_PostValidDay_OK()
        {
            var testDay = new Day
            {
                Date = Helpers.GetRandomDate(),
                Alcohol = 22.5,
                Entertainments = 10,
                Food = 200,
                Household = 50,
                Total = 285.5,
                Shops = new List<KeyValuePair<string, double>>
                {
                    new KeyValuePair<string, double> ("alcho", 22.5),
                    new KeyValuePair<string, double> ("enter", 10),
                    new KeyValuePair<string, double> ("food", 200),
                    new KeyValuePair<string, double> ("house", 500),
                }
            };

            var resp = Api.Post(testDay);

            Assert.IsTrue(testDay.Equals(resp.Data));
        }

        [TestMethod]
        public void RootController_GetDay_OK()
        {
            var testDate = Helpers.GetRandomDate();
            var testDay = new Day
            {
                Date = testDate,
                Alcohol = 22.5,
                Entertainments = 10,
                Food = 200,
                Household = 50,
                Total = 285.5,
                Shops = new List<KeyValuePair<string, double>>
                {
                    new KeyValuePair<string, double> ("alcho", 22.5),
                    new KeyValuePair<string, double> ("enter", 10),
                    new KeyValuePair<string, double> ("food", 200),
                    new KeyValuePair<string, double> ("house", 500),
                }
            };

            Api.Post(testDay);
            var dayFromApi = Api.Get(testDate);

            Assert.IsTrue(testDay.Equals(dayFromApi.Data));
        }
    }
}