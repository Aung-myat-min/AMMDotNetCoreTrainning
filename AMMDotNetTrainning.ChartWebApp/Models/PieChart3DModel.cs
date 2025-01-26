namespace AMMDotNetTrainning.ChartWebApp.Models
{
    public class PieChart3DModel
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public List<PieChart3DData> Data { get; set; }
    }

    public class PieChart3DData
    {
        public string name { get; set; }
        public int y { get; set; }
        public bool sliced { get; set; } = false;
        public bool selected { get; set; } = false;

        public PieChart3DData(string name, int y, bool? slice = false, bool? selected = false)
        {
            this.name = name;
            this.y = y;
            this.sliced = slice ?? false;
            this.selected = selected ?? false;
        }
    }
}
