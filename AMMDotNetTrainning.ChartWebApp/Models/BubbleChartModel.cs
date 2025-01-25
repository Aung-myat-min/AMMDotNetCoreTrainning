namespace AMMDotNetTrainning.ChartWebApp.Models
{
    public class BubbleChartModel
    {
        public string Title { get; set; }
        public List<ParentData> Data { get; set; }
        public string BackgroundColor { get; set; } = "rgb(255, 99, 132)";
    }

    public class ParentData
    {
        public string label { get; set; }
        public List<BubbleChartData> data { get; set; }

        public ParentData(string l)
        {
            this.label = l;
        }
    }

    public class BubbleChartData
    {
        public int x { get; set; }
        public int y { get; set; }
        public int r { get; set; }

        public BubbleChartData(int x, int y, int r)
        {
            this.x = x;
            this.y = y;
            this.r = r;
        }
    }
}
