using System;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace CostAnalysis
{
    public class DocumentOpener
    {
        internal SpreadsheetDocument OpenSpreadsheet(string path)
        {
            SpreadsheetDocument document = SpreadsheetDocument.Open(path, false);

            return document;
        }

        //Use 2019 hard coded because app developed for 2019 year only
        internal Worksheet GetSheet(string path, string sheetName = "2019")
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path '{path}' is not valid");
            }
            if (String.IsNullOrEmpty(sheetName))
            {
                throw new ArgumentException("Sheet name '{sheetName}' is not valid");
            }

            Worksheet worksheet;
            using (SpreadsheetDocument doc = OpenSpreadsheet(path))
            {
                Sheet sheet = doc.WorkbookPart.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == sheetName);
                worksheet = ((WorksheetPart)doc.WorkbookPart.GetPartById(sheet?.Id)).Worksheet;
            }

            return worksheet;
        }
    }
}