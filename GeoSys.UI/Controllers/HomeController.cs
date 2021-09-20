using GeoSys.Domain.Helpers;
using GeoSys.Domain.ViewModels;
using GeoSys.Services.UIServices;
using GeoSys.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GeoSys.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        APIClient client = new APIClient();
        Dictionary<string, string> keys = new Dictionary<string, string>();
        private readonly ICategoriesServices _categoriesServices;
        private readonly IProductsServices _productsServices;
        List<CategoriesViewModel> cDto = new List<CategoriesViewModel>();
        List<ProductsViewModel> pDto = new List<ProductsViewModel>();

        public HomeController(ICategoriesServices categoriesServices, IProductsServices productsServices, ILogger<HomeController> logger)
        {
            _categoriesServices = categoriesServices;
            _productsServices = productsServices;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var cResponse = await _categoriesServices.GetCategoriList();
            var pResponse = await _productsServices.GetProductsList();
            cDto = JsonConvert.DeserializeObject<List<CategoriesViewModel>>(cResponse.Data);
            pDto = JsonConvert.DeserializeObject<List<ProductsViewModel>>(pResponse.Data);
            TempData["Categories"] = cDto;
            TempData["Products"] = pDto;
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
