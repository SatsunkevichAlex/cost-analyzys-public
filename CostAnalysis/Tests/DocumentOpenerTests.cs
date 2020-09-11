using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using NUnit.Framework;

namespace CostAnalysis.Tests
{
    public class DocumentOpenerTests
    {
        [Test]
        public void OpenDocument_ValidPath_Test()
        {
            string path = AppConstants.dataSheetPath;

            SpreadsheetDocument doc = new DocumentOpener().OpenSpreadsheet(path);

            Assert.IsNotNull(doc);
        }

        [Test]
        public void OpenSheetByName_ValidName_Test()
        {
            string path = AppConstants.dataSheetPath;
            string name = AppConstants.SpreadsheetName2019;

            Worksheet testSheet = new DocumentOpener().GetSheet(path, name);

            Assert.IsNotNull(testSheet);
        }
    }
}