using System.Diagnostics;
using E_commerse_study.Data;
using E_commerse_study.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_commerse_study.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

		AplicationDbContext db=new AplicationDbContext();
		public HomeController(ILogger<HomeController> logger)
        {
            
            _logger = logger;
        }

        public IActionResult Index()
        {
        var products=  db.products.ToList();
            return View(model: products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Detailes(int Id)

        {
         var product=  db.products.Find(Id);
            if (product != null)
            {
                return View(product);
            }

            else { 
            
            return RedirectToAction("NotFountPage");
            }
        }
        public IActionResult NotFountPage()
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
