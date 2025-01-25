namespace AMMDotNetTrainning.ChartWebApp.Models
{
    public class AreaChartModel
    {
        public string Title { get; set; }
        public List<AreaChartData> Data { get; set; }
    }

    public class AreaChartData
    {
        public int y { get; set; }

        public AreaChartData(int y)
        {
            this.y = y;
        }
    }
}

