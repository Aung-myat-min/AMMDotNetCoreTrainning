using AMMDotNetTrainning.ChartWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AMMDotNetTrainning.ChartWebApp.Controllers
{
    public class CanvasJSController : Controller
    {
        public IActionResult LineChart()
        {
            var _lineChartData = new LineChartModel();
            _lineChartData.Title = "Actual vs Projected Sales";
            _lineChartData.Description = "Number of Sales";
            _lineChartData.ProjectSaleName = "Projected Sales";
            _lineChartData.ProjectSaleData = new List<LineChartData>
            {
                new LineChartData ( new DateTime(2017, 10, 1), 63 ),
                new LineChartData( new DateTime(2017, 10, 2), 69 ),
                new LineChartData( new DateTime(2017, 10, 3), 65 ),
                new LineChartData( new DateTime(2017, 10, 4), 70 ),
                new LineChartData( new DateTime(2017, 10, 5), 71 ),
                new LineChartData( new DateTime(2017, 10, 6), 65 ),
                new LineChartData( new DateTime(2017, 10, 7), 73 ),
                new LineChartData( new DateTime(2017, 10, 8), 96 ),
                new LineChartData( new DateTime(2017, 10, 9), 84 ),
                new LineChartData( new DateTime(2017, 10, 10), 85 ),
                new LineChartData( new DateTime(2017, 10, 11), 86 ),
                new LineChartData( new DateTime(2017, 10, 12), 94 ),
                new LineChartData( new DateTime(2017, 10, 13), 97 ),
                new LineChartData( new DateTime(2017, 10, 14), 86 ),
                new LineChartData( new DateTime(2017, 10, 15), 89 )
            };
            _lineChartData.ActualSaleName = "Actual Sales";
            _lineChartData.ActualSaleData = new List<LineChartData>
            {
                new LineChartData( new DateTime(2017, 10, 1),  60),
                new LineChartData( new DateTime(2017, 10, 2),  57),
                new LineChartData( new DateTime(2017, 10, 3),  51),
                new LineChartData( new DateTime(2017, 10, 4),  56),
                new LineChartData( new DateTime(2017, 10, 5),  54),
                new LineChartData( new DateTime(2017, 10, 6),  55),
                new LineChartData( new DateTime(2017, 10, 7),  54),
                new LineChartData( new DateTime(2017, 10, 8),  69),
                new LineChartData( new DateTime(2017, 10, 9),  65),
                new LineChartData( new DateTime(2017, 10, 10),  66),
                new LineChartData( new DateTime(2017, 10, 11),  63),
                new LineChartData( new DateTime(2017, 10, 12),  67),
                new LineChartData( new DateTime(2017, 10, 13),  66),
                new LineChartData( new DateTime(2017, 10, 14),  56),
                new LineChartData( new DateTime(2017, 10, 15),  64)
            };
            return View("LineChart", _lineChartData);
        }

        public IActionResult AreaChart()
        {
            var _areaChartData = new AreaChartModel();
            _areaChartData.Title = "Spline Area Chart";
            _areaChartData.Data = new List<AreaChartData>
            {
                new AreaChartData( 10 ),
                new AreaChartData( 6 ),
                new AreaChartData( 14 ),
                new AreaChartData( 12 ),
                new AreaChartData( 19 ),
                new AreaChartData( 14 ),
                new AreaChartData( 26 ),
                new AreaChartData( 10 ),
                new AreaChartData( 22 ),
            };
            return View("AreaChart");
        }
    }
}
