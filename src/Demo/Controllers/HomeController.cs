using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {       
        IStringLocalizer SR;

        public HomeController(IStringLocalizerFactory SRFactory)
        {
            SR = SRFactory.Create(null);
        }

        public async Task<IActionResult> Index()
        {
            var uiCulture =  this.HttpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture?.UICulture;
            //Set a Viewdata var in order to comprare what string should be inside the view,
            //because from controller, CurrentUICulture is always correct but view CurrentUICulture can be default.
            //ViewData["ExpectedString"] = LocalizationService.Get("KEY:Test", System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);
            //Using EntityFramework String Localizer
            ViewData["ExpectedString"] = SR["Hello"];
            ViewData["ControllerCurrentUICulture"] = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            ViewData["HttpContextUiCulture"] = uiCulture.TwoLetterISOLanguageName;
            var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            ViewData["ControllerThreadId"] = threadId;            
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
