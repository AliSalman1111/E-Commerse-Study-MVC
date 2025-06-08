using E_commerse_study.Data;
using E_commerse_study.Models;
using E_commerse_study.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerse_study.Controllers
{
    public class CategoryController : Controller
    {
       // AplicationDbContext db = new AplicationDbContext();

        CategoryRepository CategoryRepository =new CategoryRepository();

        public IActionResult Index()
        {
           //var categories = db.categories.Include(e => e.Products).ToList();
            var categories = CategoryRepository.GetAll("Products");
            return View(categories);

        }
        public IActionResult Create()
        {
            Category category = new Category();
            return View(category);
           
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {

            // Category category=new Category() { Name= CategoryName };
            //if (ModelState.IsValid)
            //{
             // db.categories.Add(category);
                CategoryRepository.Add(category);
                //db.SaveChanges();
               CategoryRepository.Commit();
                return RedirectToAction("Index");
            //}
            //return View(category);
        }


        public IActionResult Edit ( int Id)
        {
            var category = CategoryRepository.Getone(Id);
            return View(category);
        }



        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                // db.categories.Update(category);
                CategoryRepository.Edit(category);
                // db.SaveChanges();
                CategoryRepository.Commit();

                return RedirectToAction("Index");

            }
                // Category category=new Category() { Name= CategoryName };
           return View(category);
        }

        public IActionResult Delete(Category category)
        {

            // Category category=new Category() { Name= CategoryName };
            // db.categories.Remove(category);
            CategoryRepository.Delete(category);
            // db.SaveChanges();
            CategoryRepository.Commit();

            return RedirectToAction("Index");


        }
    }
}
