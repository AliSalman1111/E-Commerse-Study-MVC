using E_commerse_study.Data;
using E_commerse_study.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerse_study.Controllers
{
    public class CompanyController : Controller
    {
        AplicationDbContext db = new AplicationDbContext();
        public IActionResult Index()
        {
            var companes =db.companies.Include(e=>e.Products).ToList();
            return View(companes);
        }
        public IActionResult Create()
        {
          //  Company company = new Company();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Company company)
        {

            // Category category=new Category() { Name= CategoryName };
           // if (ModelState.IsValid)
           // {
                db.companies.Add(company);
                db.SaveChanges();

                return RedirectToAction("Index");
           // }
            //return View(company);
        }


        public IActionResult Edit(int Id)
        {
            var company = db.companies.Find(Id);
            return View(company);
        }

        [HttpPost]
        public IActionResult Edit(Company company)
        {
           // if (ModelState.IsValid)
           // {
                db.companies.Update(company);
                db.SaveChanges();

                return RedirectToAction("Index");

           // }
            // Category category=new Category() { Name= CategoryName };
           // return View(company);
        }

    }
}
