using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace CostAnalysisApiTest
{
    [TestClass]
    public class BaseTest
    {
        private const string _rootControllerUrl = "http://localhost:5000/days";
        private string _days = "days";

        protected CostAnalysisApi Api { get; set; }

        protected string DaysEndpoint
        {
            get => Path.Combine(_rootControllerUrl, _days);
            set => _days = value;
        }

        [TestInitialize]
        public void Init()
        {
            Api = new CostAnalysisApi(DaysEndpoint);
        }
    }
}