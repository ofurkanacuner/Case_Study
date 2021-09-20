using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoSys.UI.Controllers
{
    public class CategoriController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
