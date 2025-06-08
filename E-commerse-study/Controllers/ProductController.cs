using E_commerse_study.Data;
using E_commerse_study.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_commerse_study.Controllers
{
    public class ProductController : Controller
    {
        AplicationDbContext db = new AplicationDbContext();
        public IActionResult Index()
        {

            var products = db.products.Include(e => e.Category).ToList();

            ViewBag.Products = Request.Cookies["succes"];
            return View(products);
        }

        public IActionResult Create()
        {
           var categores= db.categories.ToList().Select(e=>new SelectListItem { Text=e.Name,Value=e.Id.ToString()});
            ViewBag.categores = categores;
            Product product = new Product();
            return View(product);
        }

        [HttpPost]
        public IActionResult Create(Product product,IFormFile photo)
        {
            if (ModelState.IsValid)
            {


                if (photo.Length > 0)
                {
                    // var filePath = Path.GetTempFileName();

                    var fileName = Guid.NewGuid() + Path.GetExtension(photo.FileName);

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);


                    using (var stream = System.IO.File.Create(filePath))
                    {
                        photo.CopyTo(stream);
                    }

                    product.photo = fileName;
                }
                // Category category=new Category() { Name= CategoryName };
                db.products.Add(product);
                db.SaveChanges();
                TempData["succes"] = "Add Product Succesfuly";
                return RedirectToAction("Index");
            }
            var categores = db.categories.ToList();
            ViewBag.categores = categores;
           // Product product = new Product();
            return View(product);
        }

        public IActionResult Edit(int Id)
        {
            var product = db.products.Find(Id);
            var categores = db.categories.ToList();
            ViewData["categores"] = categores;
          // TempData["id"] = "Ali Salman";
            return View(product);
        }



        [HttpPost]
        public IActionResult Edit(Product product, IFormFile photo)
        {
            var oldproduct = db.products.AsNoTracking().FirstOrDefault(e => e.Id == product.Id);
            // product.photo = oldproduct.photo;

            if (ModelState.IsValid)
            {


                if (photo != null && photo.Length > 0)
                {
                    // var filePath = Path.GetTempFileName();

                    var fileName = Guid.NewGuid() + Path.GetExtension(photo.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", oldproduct.photo);


                    using (var stream = System.IO.File.Create(filePath))
                    {
                        photo.CopyTo(stream);
                    }

                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);

                    }
                    product.photo = fileName;
                }
                else
                {

                    product.photo = oldproduct.photo;
                }
                // Category category=new Category() { Name= CategoryName };
                db.products.Update(product);
                db.SaveChanges();
                TempData["Succes"] = "Edit Product Succesfuly";
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddMinutes(1);

                Response.Cookies.Append("succes", "Edit Product Succesfuly", cookieOptions);
                return RedirectToAction("Index");
            }
            
            var categores = db.categories.ToList();
            ViewData["categores"] = categores;
            product.photo = oldproduct.photo;

            return View(product);
        }







        public IActionResult Delete(int Id)
        {
            var product = db.products.Find(Id);
            // Category category=new Category() { Name= CategoryName };
            var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", product.photo);

            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);

            }

            db.products.Remove(product);
            db.SaveChanges();
            TempData["Succes"] = "Delete Product Succesfuly";
            return RedirectToAction("Index");
        }
    }

}
