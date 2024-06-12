using Etrade.DAL.Abstract;
using Etrade.Data.Models.ViewModels;
using ETrade.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ETrade.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryDAL _categoryDAL;
        private readonly IProductDAL _productDAL;
        public HomeController(ILogger<HomeController> logger, ICategoryDAL categoryDAL, IProductDAL productDAL) 
        {
            _logger = logger;
            _categoryDAL = categoryDAL;
            _productDAL = productDAL;
        }

        public IActionResult Index()
        {
            var products = _productDAL.GetAll(i => i.IsHome && i.IsApproved);
            return View(products);
        }
        [Authorize(Roles ="Admin")]
        public IActionResult List(int? id)
        {
            ViewBag.Id = id;
            var products = _productDAL.GetAll(i => i.IsApproved);

            if (id != null)
            {
                products = products.Where(p => p.CategoryId == id).ToList();
            }
            var models = new ListViewModel()
            {
                Categories = _categoryDAL.GetAll(),
                Products = products
            };
            return View(models);
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