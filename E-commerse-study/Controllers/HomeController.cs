using System.Diagnostics;
using E_commerse_study.Data;
using E_commerse_study.Models;
using E_commerse_study.Repository;
using E_commerse_study.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace E_commerse_study.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //AplicationDbContext db=new AplicationDbContext();

        IProductRepositry ProductRepositry;

		public HomeController(ILogger<HomeController> logger, IProductRepositry productRepositry)
        {
            ProductRepositry = productRepositry;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var products = ProductRepositry.GetAll();
            return View(model: products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Detailes(int Id)

        {
            var product = ProductRepositry.Getone(new Func<IQueryable<Product>, IQueryable<Product>>[]{



            },
            filter:e=>e.Id==Id
            );



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
