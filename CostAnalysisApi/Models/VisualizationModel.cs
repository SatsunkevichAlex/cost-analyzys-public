using System.Collections.Generic;

namespace CostAnalysisAPI.Models
{
    public class VisualizationModel
    {
        public string Name { get; set; }
        public IEnumerable<DayPoint> Series { get; set; }
    }
}