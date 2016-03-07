using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        Localization.LocalizationService LocalizationService;

        public HomeController(Localization.LocalizationService _locService)
        {
            LocalizationService = _locService;
        }

        public async Task<IActionResult> Index()
        {
            //Set a Viewdata var in order to comprare what string should be inside the view,
            //because from controller, CurrentUICulture is always correct but view CurrentUICulture can be default.
            ViewData["ExpectedString"] = LocalizationService.Get("KEY:Test", System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);
            ViewData["ControllerCurrentUICulture"] = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
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
