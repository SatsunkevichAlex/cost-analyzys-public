using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using System.Text.RegularExpressions;
using System.Globalization;
using Newtonsoft.Json;
using CostAnalysis.Models;

namespace CostAnalysis
{
    public class WorksheetProcessor
    {
        internal List<Day> GetDays()
        {
            DocumentOpener dop = new DocumentOpener();
            Worksheet ws = dop.GetSheet(AppConstants.dataSheetPath);
            List<List<string>> rows = GetRows(ws);

            List<Day> result = new List<Day>();
            List<List<string>> daysRows = ExcludeNonDays(rows);

            foreach (List<string> rowCells in daysRows)
            {
                Day day = ParseRowCells(rowCells);
                result.Add(day);
            }

            return result;
        }

        List<List<string>> GetRows(Worksheet sheet)
        {
            IEnumerable<Row> rows = sheet.Descendants<Row>();
            List<List<string>> rowsData = new List<List<string>>();

            foreach (Row row in rows)
            {
                List<string> cellsText = new List<string>();
                foreach (Cell cell in row)
                {
                    string cellValue = GetCellValue(cell);
                    cellsText.Add(cellValue);
                }
                rowsData.Add(cellsText);
            }

            int rowsToGet = AppConstants.daysIn2019Count + AppConstants.firstRowCount + AppConstants.resultsCellsCount;
            rowsData = rowsData.Take(rowsToGet).ToList();
            return rowsData;
        }        

        List<List<string>> ExcludeNonDays(List<List<string>> rowsCells)
        {
            List<List<string>> result = new List<List<string>>();

            foreach (List<string> rowCells in rowsCells)
            {
                if (IsRowWithColumnsNames(rowCells))
                {
                    continue;
                }

                if (IsRowMonthTotal(rowCells))
                {
                    continue;
                }

                result.Add(rowCells);
            }

            return result;
        }

        bool IsRowMonthTotal(List<string> cells) //There are row for each month and for the year. It contains work "Итог"
        {
            List<string> notNullCells = cells.Where(it => !String.IsNullOrEmpty(it)).ToList();
            bool result = notNullCells.Any(it => it.ToUpper().Contains(AppConstants.inTotal.ToUpper()));
            return result;
        }

        bool IsRowWithColumnsNames(List<string> cells) //First row not day and conatins word "Всего"
        {
            List<string> notNullCells = cells.Where(it => !String.IsNullOrEmpty(it)).ToList();
            bool result = notNullCells.Any(it => it.ToUpper().Contains(AppConstants.total.ToUpper()));
            return result;
        }

        Day ParseRowCells(List<string> cells)
        {
            var date = DateTime.ParseExact(cells[0], "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var total = Convert.ToDouble(cells[1]);
            var alcohol = Convert.ToDouble(cells[2]);
            var entertainments = Convert.ToDouble(cells[3]);
            var food = Convert.ToDouble(cells[4]);
            var household = Convert.ToDouble(cells[5]);
            var shops = ParseCellsForShops(cells);

            return new Day(date, total, alcohol, entertainments, food, household, shops);
        }

        List<KeyValuePair<string, double>> ParseCellsForShops(List<string> cells)
        {
            List<KeyValuePair<string, double>> result = new List<KeyValuePair<string, double>>();
            List<string> cellsWithShops = cells.Skip(7).TakeWhile(it => !String.IsNullOrEmpty(it)).ToList(); //Shops starts from 8 cell. Take cells while there is any value in it

            for (int i = 0; i < cellsWithShops.Count; i++)
            {
                if (IsFewShopsInOneCell(cellsWithShops[i]))
                {
                    List<KeyValuePair<string, double>> shopsInOneCell = MultiplyCell(cellsWithShops[i]);
                    foreach (KeyValuePair<string, double> item in shopsInOneCell)
                    {
                        result.Add(new KeyValuePair<string, double>(item.Key, item.Value));
                    }
                    continue;
                }

                string shop = GetShop(cellsWithShops[i]);
                double cost = GetPrice(cellsWithShops[i]);
                result.Add(new KeyValuePair<string, double>(shop.Trim().Replace("  ", " "), cost));
            }

            return result;
        }

        //IN REGEX PATTER RUSSIAN 'x' !!!
        bool IsFewShopsInOneCell(string cell)
        {
            string pattern = @"\sх\d\n\d.+";
            return Regex.Matches(cell, pattern).Count != 0 ? true : false;
        }

        List<KeyValuePair<string, double>> MultiplyCell(string cellText)
        {
            List<KeyValuePair<string, double>> result = new List<KeyValuePair<string, double>>();

            int count = GetItemsCountInOneCell(cellText);
            string item = GetShop(cellText);
            double totalPrice = GetPrice(cellText);

            double singleItemPrice = totalPrice / count;

            for (int i = 0; i < count; i++)
            {
                result.Add(new KeyValuePair<string, double>(item, singleItemPrice));
            }

            return result;
        }

        int GetItemsCountInOneCell(string cellText)
        {
            string pattern = @"х\d+";
            MatchCollection matches = Regex.Matches(cellText, pattern);
            string fMatch = matches[0].Value;
            string digitS = fMatch.Substring(1, fMatch.Length - 1);
            return Convert.ToInt16(digitS);
        }

        string GetShop(string cellText)
        {
            string itemText = cellText.Substring(0, cellText.LastIndexOf('\n'));

            string pattern = @"х\d+";
            MatchCollection matches = Regex.Matches(itemText, pattern);
            if (matches.Count != 0)
            {
                string fMatch = matches[0].Value;
                string multiplyier = fMatch.Substring(0, fMatch.Length);

                string withoutMuiltyplier = itemText.Replace(multiplyier, string.Empty).TrimEnd('\n');
                return withoutMuiltyplier;
            }

            return itemText.Replace('\n', ' ');
        }

        double GetPrice(string cellText)
        {
            string pattern = @"\n.+";
            MatchCollection matches = Regex.Matches(cellText, pattern);
            string fMatch = matches.Last().Value;
            string fDigigt = fMatch.Substring(1, fMatch.Length - 1);
            double price = Convert.ToDouble(fDigigt);
            return price;
        }

        string GetCellValue(Cell cell)
        {
            string value = null;

            if (cell.InnerText.Length > 0)
            {
                value = cell.InnerText;
                if (cell.DataType != null)
                {
                    switch (cell.DataType.Value)
                    {
                        case CellValues.SharedString:
                            SharedStringTablePart stringTable = GetWorkBookPart().GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                            if (stringTable != null)
                            {
                                int elementIndex = Int32.Parse(value);
                                var elementAT = stringTable.SharedStringTable.ElementAt(elementIndex);
                                value = elementAT.InnerText;
                            }
                            break;
                    }
                }
            }

            return value;
        }

        WorkbookPart GetWorkBookPart()
        {
            return new DocumentOpener().OpenSpreadsheet(AppConstants.dataSheetPath).WorkbookPart;
        }
    }
}