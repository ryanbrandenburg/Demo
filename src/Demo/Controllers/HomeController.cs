using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        Localization.LocalizationService loc;

        public HomeController(Localization.LocalizationService _loc)
        {
            loc = _loc;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Language"] = loc.Get("KEY:Test", System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);
            //Simulate small job
            await Task.Delay(500);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
