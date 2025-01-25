namespace AMMDotNetTrainning.ChartWebApp.Models
{
    public class RangeAreaChartModel
    {
        public string Title { get; set; }
        public string Label { get; set; }
        public List<ParentRangeArea> Data { get; set; }
    }

    public class ParentRangeArea
    {
        public string name { get; set; }
        public List<RangeAreaChartData> data { get; set; }

        public ParentRangeArea(string name, List<RangeAreaChartData> datas)
        {
            this.name = name;
            this.data = datas;
        }
    }

    public class RangeAreaChartData
    {
        public string x { get; set; }
        public List<int> y { get; set; }

        public RangeAreaChartData(string x, List<int> y) 
        {
            this.x = x;
            this.y = y;
        }
    }
}
