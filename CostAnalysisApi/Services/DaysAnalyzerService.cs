using CostAnalysis;
using CostAnalysis.Models;
using System;

namespace CostAnalysisAPI.Services
{
    public class DaysAnalyzerService
    {
        private readonly WorksheetAnalyzer _wsAnalyzer;

        public DaysAnalyzerService()
        {
            _wsAnalyzer = new WorksheetAnalyzer();
        }

        public Day GetMaxDay()
        {
            return _wsAnalyzer.GetMaxDay();
        }

        public Day GetDayByDate(string date)
        {
            var requestedDate = DateTime.Parse(date);
            return _wsAnalyzer.GetDayByDate(requestedDate);
        }

        public int GetTotalDaysCount()
        {
            return _wsAnalyzer.GetTotalDaysCount();
        }

        public double GetDaysTotalAverage()
        {
            return _wsAnalyzer.GetDaysTotalAverage();
        }
    }
}