namespace AMMDotNetTrainning.ChartWebApp.Models
{
    public class BarChartModel
    {
        public string Title { get; set; }
        public List<BarChartData> Data { get; set; }
        public List<string> XRowNames { get; set; }
    }
    public class BarChartData
    {
        public string Title { get; set; }
        public List<int> Data { get; set; }

        public BarChartData(string title, List<int> data)
        {
            this.Title = title;
            this.Data = data;
        }
    }
}
