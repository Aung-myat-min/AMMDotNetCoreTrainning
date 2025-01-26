using Microsoft.AspNetCore.Mvc;
using AMMDotNetTrainning.ChartWebApp.Models;

namespace AMMDotNetTrainning.ChartWebApp.Controllers
{
    public class HighChartController : Controller
    {
        public IActionResult TreeChart()
        {
            var model = new TreeChartModel
            {
                Title = "Phylogenetic language tree",
                Data = new List<List<object>>
                {
                    new List<object> { null, "Proto Indo-European" },
                    new List<object> { "Proto Indo-European", "Balto-Slavic" },
                    new List<object> { "Proto Indo-European", "Germanic" },
                    new List<object> { "Proto Indo-European", "Celtic" },
                    new List<object> { "Proto Indo-European", "Italic" },
                    new List<object> { "Proto Indo-European", "Hellenic" },
                    new List<object> { "Proto Indo-European", "Anatolian" },
                    new List<object> { "Proto Indo-European", "Indo-Iranian" },
                    new List<object> { "Proto Indo-European", "Tocharian" },
                    new List<object> { "Indo-Iranian", "Dardic" },
                    new List<object> { "Indo-Iranian", "Indic" },
                    new List<object> { "Indo-Iranian", "Iranian" },
                    new List<object> { "Iranian", "Old Persian" },
                    new List<object> { "Old Persian", "Middle Persian" },
                    new List<object> { "Indic", "Sanskrit" },
                    new List<object> { "Italic", "Osco-Umbrian" },
                    new List<object> { "Italic", "Latino-Faliscan" },
                    new List<object> { "Latino-Faliscan", "Latin" },
                    new List<object> { "Celtic", "Brythonic" },
                    new List<object> { "Celtic", "Goidelic" },
                    new List<object> { "Germanic", "North Germanic" },
                    new List<object> { "Germanic", "West Germanic" },
                    new List<object> { "Germanic", "East Germanic" },
                    new List<object> { "North Germanic", "Old Norse" },
                    new List<object> { "North Germanic", "Old Swedish" },
                    new List<object> { "North Germanic", "Old Danish" },
                    new List<object> { "West Germanic", "Old English" },
                    new List<object> { "West Germanic", "Old Frisian" },
                    new List<object> { "West Germanic", "Old Dutch" },
                    new List<object> { "West Germanic", "Old Low German" },
                    new List<object> { "West Germanic", "Old High German" },
                    new List<object> { "Old Norse", "Old Icelandic" },
                    new List<object> { "Old Norse", "Old Norwegian" },
                    new List<object> { "Old Swedish", "Middle Swedish" },
                    new List<object> { "Old Danish", "Middle Danish" },
                    new List<object> { "Old English", "Middle English" },
                    new List<object> { "Old Dutch", "Middle Dutch" },
                    new List<object> { "Old Low German", "Middle Low German" },
                    new List<object> { "Old High German", "Middle High German" },
                    new List<object> { "Balto-Slavic", "Baltic" },
                    new List<object> { "Balto-Slavic", "Slavic" },
                    new List<object> { "Slavic", "East Slavic" },
                    new List<object> { "Slavic", "West Slavic" },
                    new List<object> { "Slavic", "South Slavic" },
                    // Leaves:
                    new List<object> { "Proto Indo-European", "Phrygian", 6 },
                    new List<object> { "Proto Indo-European", "Armenian", 6 },
                    new List<object> { "Proto Indo-European", "Albanian", 6 },
                    new List<object> { "Proto Indo-European", "Thracian", 6 },
                    new List<object> { "Tocharian", "Tocharian A", 6 },
                    new List<object> { "Tocharian", "Tocharian B", 6 },
                    new List<object> { "Anatolian", "Hittite", 6 },
                    new List<object> { "Anatolian", "Palaic", 6 },
                    new List<object> { "Anatolian", "Luwic", 6 },
                    new List<object> { "Anatolian", "Lydian", 6 },
                    new List<object> { "Iranian", "Balochi", 6 },
                    new List<object> { "Iranian", "Kurdish", 6 },
                    new List<object> { "Iranian", "Pashto", 6 },
                    new List<object> { "Iranian", "Sogdian", 6 },
                    new List<object> { "Old Persian", "Pahlavi", 6 },
                    new List<object> { "Middle Persian", "Persian", 6 },
                    new List<object> { "Hellenic", "Greek", 6 },
                    new List<object> { "Dardic", "Dard", 6 },
                    new List<object> { "Sanskrit", "Sindhi", 6 },
                    new List<object> { "Sanskrit", "Romani", 6 },
                    new List<object> { "Sanskrit", "Urdu", 6 },
                    new List<object> { "Sanskrit", "Hindi", 6 },
                    new List<object> { "Sanskrit", "Bihari", 6 },
                    new List<object> { "Sanskrit", "Assamese", 6 },
                    new List<object> { "Sanskrit", "Bengali", 6 },
                    new List<object> { "Sanskrit", "Marathi", 6 },
                    new List<object> { "Sanskrit", "Gujarati", 6 },
                    new List<object> { "Sanskrit", "Punjabi", 6 },
                    new List<object> { "Sanskrit", "Sinhalese", 6 },
                    new List<object> { "Osco-Umbrian", "Umbrian", 6 },
                    new List<object> { "Osco-Umbrian", "Oscan", 6 },
                    new List<object> { "Latino-Faliscan", "Faliscan", 6 },
                    new List<object> { "Latin", "Portugese", 6 },
                    new List<object> { "Latin", "Spanish", 6 },
                    new List<object> { "Latin", "French", 6 },
                    new List<object> { "Latin", "Romanian", 6 },
                    new List<object> { "Latin", "Italian", 6 },
                    new List<object> { "Latin", "Catalan", 6 },
                    new List<object> { "Latin", "Franco-Provençal", 6 },
                    new List<object> { "Latin", "Rhaeto-Romance", 6 },
                    new List<object> { "Brythonic", "Welsh", 6 },
                    new List<object> { "Brythonic", "Breton", 6 },
                    new List<object> { "Brythonic", "Cornish", 6 },
                    new List<object> { "Brythonic", "Cuymbric", 6 },
                    new List<object> { "Goidelic", "Modern Irish", 6 },
                    new List<object> { "Goidelic", "Scottish Gaelic", 6 },
                    new List<object> { "Goidelic", "Manx", 6 },
                    new List<object> { "East Germanic", "Gothic", 6 },
                    new List<object> { "Middle Low German", "Low German", 6 },
                    new List<object> { "Middle High German", "(High) German", 6 },
                    new List<object> { "Middle High German", "Yiddish", 6 },
                    new List<object> { "Middle English", "English", 6 },
                    new List<object> { "Middle Dutch", "Hollandic", 6 },
                    new List<object> { "Middle Dutch", "Flemish", 6 },
                    new List<object> { "Middle Dutch", "Dutch", 6 },
                    new List<object> { "Middle Dutch", "Limburgish", 6 },
                    new List<object> { "Middle Dutch", "Brabantian", 6 },
                    new List<object> { "Middle Dutch", "Rhinelandic", 6 },
                    new List<object> { "Old Frisian", "Frisian", 6 },
                    new List<object> { "Middle Danish", "Danish", 6 },
                    new List<object> { "Middle Swedish", "Swedish", 6 },
                    new List<object> { "Old Norwegian", "Norwegian", 6 },
                    new List<object> { "Old Norse", "Faroese", 6 },
                    new List<object> { "Old Icelandic", "Icelandic", 6 },
                    new List<object> { "Baltic", "Old Prussian", 6 },
                    new List<object> { "Baltic", "Lithuanian", 6 },
                    new List<object> { "Baltic", "Latvian", 6 },
                    new List<object> { "West Slavic", "Polish", 6 },
                    new List<object> { "West Slavic", "Slovak", 6 },
                    new List<object> { "West Slavic", "Czech", 6 },
                    new List<object> { "West Slavic", "Wendish", 6 },
                    new List<object> { "East Slavic", "Bulgarian", 6 },
                    new List<object> { "East Slavic", "Old Church Slavonic", 6 },
                    new List<object> { "East Slavic", "Macedonian", 6 },
                    new List<object> { "East Slavic", "Serbo-Croatian", 6 },
                    new List<object> { "East Slavic", "Slovene", 6 },
                    new List<object> { "South Slavic", "Russian", 6 },
                    new List<object> { "South Slavic", "Ukrainian", 6 },
                    new List<object> { "South Slavic", "Belarusian", 6 },
                    new List<object> { "South Slavic", "Rusyn", 6 }
                }
            };

            return View("TreeChart", model);
        }

        public IActionResult PieChart3D()
        {
            var model = new PieChart3DModel
            {
                Title = "Global smartphone shipments market share, Q1 2022",
                Subtitle = "Source: <a href='https://www.counterpointresearch.com/global-smartphone-share/' target='_blank'>Counterpoint Research</a>",
                Data = new List<PieChart3DData>
                {
                    new PieChart3DData("Samsung", 23),
                    new PieChart3DData("Apple", 18),
                    new PieChart3DData("Xiaomi", 12, true, true),
                    new PieChart3DData("Oppo*", 9),
                    new PieChart3DData("Vivo", 8),
                    new PieChart3DData("Others", 30)
                }
            };

            return View("PieChart3D", model);
        }
    }
}