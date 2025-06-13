using E_commerse_study.Data;
using E_commerse_study.Models;
using E_commerse_study.Repository;
using E_commerse_study.Repository.IRepository;
using E_commerse_study.Static_Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_commerse_study.Controllers
{
    [Authorize(Roles = $"{SD.AdminRole},{SD.CompanyRole}")]
    public class ProductController : Controller
    {
        // AplicationDbContext db = new AplicationDbContext();

        IProductRepositry ProductRepositry;
        ICategoryRepository CategoryRepository;
        public ProductController(IProductRepositry productRepositry,ICategoryRepository categoryRepository)
        {
            this.ProductRepositry = productRepositry;
            this.CategoryRepository = categoryRepository;
        }
        public IActionResult Index()

        {
            ViewBag.Products = Request.Cookies["succes"];
            var products = ProductRepositry.GetAll(new Func<IQueryable<Product>, IQueryable<Product>>[]
{
                       q => q.Include(c => c.Category)
                            

});

            return View(products);
        }

        public IActionResult Create()
        {
           var categores= CategoryRepository.GetAll().Select(e=>new SelectListItem { Text=e.Name,Value=e.Id.ToString()});
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
                ProductRepositry.Add(product);
                ProductRepositry.Commit();
                TempData["succes"] = "Add Product Succesfuly";
                return RedirectToAction("Index");
            }
            var categores = CategoryRepository.GetAll();
            ViewBag.categores = categores;
           // Product product = new Product();
            return View(product);
        }

        public IActionResult Edit(int Id)
        {
            var product = ProductRepositry.Getone(new Func<IQueryable<Product>, IQueryable<Product>>[]
{
                       q => q.Include(c => c.Category)


},
filter: c => c.Id == Id

);
            var categores = CategoryRepository.GetAll();
            ViewData["categores"] = categores;
          // TempData["id"] = "Ali Salman";
            return View(product);
        }



        [HttpPost]
        public IActionResult Edit(Product product, IFormFile photo,int Id)
        {
            var oldproduct = ProductRepositry.Getone(new Func<IQueryable<Product>, IQueryable<Product>>[]
{
                       q => q.Include(c => c.Category)


},
       filter: c => c.Id == product.Id
       , tracked: false

);
            // product.photo = oldproduct.photo;

            // if (ModelState.IsValid)
            // {


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
            ProductRepositry.Edit(product);
            ProductRepositry.Commit();
              TempData["Succes"] = "Edit Product Succesfuly";
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddMinutes(1);

                Response.Cookies.Append("succes", "Edit Product Succesfuly", cookieOptions);
                return RedirectToAction("Index");
            }
            
        //    var categores = db.categories.ToList();
        //    ViewData["categores"] = categores;
        //    product.photo = oldproduct.photo;

        //    return View(product);
        //}







        public IActionResult Delete(int Id)
        {
            var product = ProductRepositry.Getone(new Func<IQueryable<Product>, IQueryable<Product>>[]
{
                       q => q.Include(c => c.Category)


},
filter: c => c.Id == Id

);
            // Category category=new Category() { Name= CategoryName };
            var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", product.photo);

            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);

            }

            ProductRepositry.Delete(product);
            ProductRepositry.Commit();
            TempData["Succes"] = "Delete Product Succesfuly";
            return RedirectToAction("Index");
        }
    }

}
