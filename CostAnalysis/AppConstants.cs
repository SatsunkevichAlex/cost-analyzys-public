using CostAnalysis.Models;
using System;
using System.IO;

namespace CostAnalysis
{
    public class AppConstants
    {
        static readonly string rootFolder = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName;

        public static string SpreadsheetName2019 = "2019";
        public static string dataSheetPath = rootFolder + "\\Data\\ExpensesData.xlsx";
        public static string dbPath = rootFolder + "\\Data\\Databases\\Expenditures.db";
        public static string testDbPath = rootFolder + "\\Data\\Databases\\TestExpenditures.db";
        public static string shopsNamesPath = rootFolder + "\\Data\\shopsNames.txt";
        public static string alcoDictPath = rootFolder + "\\Data\\Dictionaries\\alco.txt";
        public static string dbConnectionString = $"Data Source={testDbPath}";
        public static int daysIn2019Count = 365;
        public static int resultsCellsCount = 13; //Each month and for the year
        public static int firstRowCount = 1;
        public static string total = "итог";
        public static string inTotal = "Всего";

        public static string GetCategoryDictPath(Category cat) => $"{rootFolder}\\Data\\Dictionaries\\{cat.GetDescription()}.txt";
    }
}