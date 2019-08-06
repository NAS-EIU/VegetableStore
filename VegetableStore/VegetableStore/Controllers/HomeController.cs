using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using VegetableStore.Interfaces;
using VegetableStore.Models;
using VegetableStore.Models.ViewModels;

namespace VegetableStore.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository _productService;
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(IProductRepository productService, IStringLocalizer<HomeController> localizer)
        {
            _productService = productService;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            
                //var title = _localizer["Title"];
                //var culture = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
                ViewData["BodyClass"] = "cms-index-index cms-home-page";
                var homeVm = new HomeViewModel();
                homeVm.HotProducts = _productService.GetHotProduct(12);
                homeVm.TopSellProducts = _productService.GetLastest(12);
                
                return View(homeVm);
            
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
