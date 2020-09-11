using NUnit.Framework;

namespace CostAnalysis.Tests
{
    [TestFixture]
    public class PathTests
    {
        /// <summary>
        /// This test doesn't have any value. Created just for debugging
        /// </summary>
        [Test]
        public void DataSheet_Path_Test()
        {
            string path = AppConstants.dataSheetPath;

            Assert.IsNotNull(path);
        }
    }
}