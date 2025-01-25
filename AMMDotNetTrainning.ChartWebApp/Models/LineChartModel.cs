namespace AMMDotNetTrainning.ChartWebApp.Models
{
    public class LineChartModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProjectSaleName { get; set; }
        public List<LineChartData> ProjectSaleData { get; set; }
        public string ActualSaleName { get; set; }
        public List<LineChartData> ActualSaleData { get; set; }
    }

    public class LineChartData 
    {
        public DateTime x { get; set; }
        public int y { get; set; }

        public LineChartData(DateTime x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

}
