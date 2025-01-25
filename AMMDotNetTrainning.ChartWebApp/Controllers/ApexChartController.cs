using AMMDotNetTrainning.ChartWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AMMDotNetTrainning.ChartWebApp.Controllers
{
    public class ApexChartController : Controller
    {
        public IActionResult ColumnChart()
        {
            var _chartData = new ColumnChartModel();
            _chartData.Title = "Monthly Inflation in Argentina, 2002";
            _chartData.Categories = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            _chartData.Data = new List<ColumnChartData>
            {
                new ColumnChartData("Inflation",
                new List<double>{ 2.3, 3.1, 4.0, 10.1, 4.0, 3.6, 3.2, 2.3, 1.4, 0.8, 0.5, 0.2 })
            };
            return View("ColumnChart", _chartData);
        }

        public IActionResult RangeAreaChart()
        {
            var chartData = new RangeAreaChartModel();
            chartData.Title = "New York Temperature (all year round)";
            chartData.Label = " °C";
            chartData.Data = new List<ParentRangeArea>
            {
                new ParentRangeArea("New York Temperature", new List<RangeAreaChartData>
                {
                    new RangeAreaChartData("Jan", new List<int> { -2, 4 }),
                    new RangeAreaChartData("Feb", new List<int> { -1, 6 }),
                    new RangeAreaChartData("Mar", new List<int> { 3, 10 }),
                    new RangeAreaChartData("Apr", new List<int> { 8, 16 }),
                    new RangeAreaChartData("May", new List<int> { 13, 22 }),
                    new RangeAreaChartData("Jun", new List<int> { 18, 26 }),
                    new RangeAreaChartData("Jul", new List<int> { 21, 29 }),
                    new RangeAreaChartData("Aug", new List<int> { 21, 28 }),
                    new RangeAreaChartData("Sep", new List<int> { 17, 24 }),
                    new RangeAreaChartData("Oct", new List<int> { 11, 18 }),
                    new RangeAreaChartData("Nov", new List<int> { 6, 12 }),
                    new RangeAreaChartData("Dec", new List<int> { 1, 7 }),
                })
            };
            return View("RangeAreaChart", chartData);
        }


    }
}
