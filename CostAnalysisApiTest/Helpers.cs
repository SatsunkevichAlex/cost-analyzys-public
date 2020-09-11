using System;

namespace CostAnalysisApiTest
{
    internal class Helpers
    {
        public static DateTime GetRandomDate()
        {
            const int twenteeYearBefore = -73000;
            const int twenteeYearAfter = 73000;

            var randDayValue = new Random().
                Next(twenteeYearBefore, twenteeYearAfter);

            var dateNow = DateTime.Now;
            return dateNow.AddDays(randDayValue);
        }
    }
}