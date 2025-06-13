using E_commerse_study.Data;
using E_commerse_study.Models;
using E_commerse_study.Repository.IRepository;
using E_commerse_study.Static_Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerse_study.Controllers
{

    [Authorize(Roles = $"{SD.AdminRole}")]
    public class CompanyController : Controller
    {
        // AplicationDbContext db = new AplicationDbContext();

        ICompanyRepositry CompanyRepositry;
            public CompanyController(ICompanyRepositry companyRepositry)
        {
            CompanyRepositry=companyRepositry;
        }
        public IActionResult Index()
        {
            var companes = CompanyRepositry.GetAll(new Func<IQueryable<Company>, IQueryable<Company>>[]
            {
                q=>q.Include(c=>c.Products)
            });
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
            CompanyRepositry.Add(company);
            CompanyRepositry.Commit();

                return RedirectToAction("Index");
           // }
            //return View(company);
        }


        public IActionResult Edit(int Id)
        {
            var company = CompanyRepositry.Getone(new Func<IQueryable<Company>, IQueryable<Company>>[]{



    },filter:e=>e.Id==Id
    );
            return View(company);
        }

        [HttpPost]
        public IActionResult Edit(Company company)
        {
            // if (ModelState.IsValid)
            // {
            CompanyRepositry.Edit(company);
            CompanyRepositry.Commit();

                return RedirectToAction("Index");

           // }
            // Category category=new Category() { Name= CategoryName };
           // return View(company);
        }

    }
}
