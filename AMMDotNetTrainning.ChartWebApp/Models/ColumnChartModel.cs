namespace AMMDotNetTrainning.ChartWebApp.Models
{
    public class ColumnChartModel
    {
        public string Title { get; set; }
        public List<string> Categories { get; set; }
        public List<ColumnChartData> Data { get; set; }
    }

    public class ColumnChartData
    {
        public string name { get; set; }
        public List<double> data { get; set; }

        public ColumnChartData(string n, List<double> d) 
        {
            this.name = n;
            this.data = d;
        }
    }
}
