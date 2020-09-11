using System.ComponentModel;

namespace CostAnalysis.Models
{
    public enum Category
    {
        [Description("alco")] ALCO,
        [Description("food")] FOOD,
        [Description("enertainment")] ENETERT,
        [Description("household")] HOUSE
    }
}