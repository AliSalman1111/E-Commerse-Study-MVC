using E_commerse_study.Models;
using E_commerse_study.Repository;
using E_commerse_study.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Stripe.Checkout;

namespace E_commerse_study.Controllers
{
    [Authorize]
    public class CartController : Controller
    {

        ICartRepository CartRepository;
        UserManager<ApplicationUser> userManager;
        IProductRepositry productRepositry;
        public CartController(ICartRepository cartRepository,UserManager<ApplicationUser> userManager, IProductRepositry productRepositry) {
            CartRepository = cartRepository;
            this.userManager = userManager;
            this.productRepositry = productRepositry;
        }

        [HttpPost]
        public IActionResult AddToCart(int count, int productId)
        {

            var product = productRepositry.Getone(new Func<IQueryable<Product>, IQueryable<Product>>[]
            {


            }
            ,filter:e=>e.Id== productId); 

            if (product == null)
            {
                TempData["Error"] = "Product not found.";
                return RedirectToAction("Index", "Home");
            }

          
            if (count > product.countaty)
            {
                TempData["Error"] = "Requested quantity exceeds available stock.";
                return RedirectToAction("Index", "Home");
            }

           
            Cart cart = new Cart()
            {
                ProductId = productId,
                count = count,
                ApplicationUserId = userManager.GetUserId(User)
            };

            CartRepository.Add(cart);
            CartRepository.Commit();
            TempData["Succes"] = "Add To Cart Successfully";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {

            var userid=userManager.GetUserId(User);



var products = CartRepository.GetAll(new Func<IQueryable<Cart>, IQueryable<Cart>>[]
{
    q=>q.Include(x=>x.Product).Where(e=>e.ApplicationUserId == userid)
});
     ViewBag.products= products.Sum(x=>x.Product.price*x.count);
            return View(products.ToList());

        }

        public IActionResult Increment(int Id)
        {

            var userid = userManager.GetUserId(User);
            var product = CartRepository.Getone(new Func<IQueryable<Cart>, IQueryable<Cart>>[]
            {

            },filter: e=>e.ApplicationUserId== userid && e.ProductId== Id
          

            ) ;

            if (product != null)
            {
                product.count++;
                CartRepository.Commit();
                return RedirectToAction("Index");

            }

            return RedirectToAction("NotFountPage", "Home");
        }



        public IActionResult Decress(int Id)
        {

            var userid = userManager.GetUserId(User);
            var product = CartRepository.Getone(new Func<IQueryable<Cart>, IQueryable<Cart>>[]
            {

            }, filter: e => e.ApplicationUserId == userid && e.ProductId == Id


            );
            if (product != null)
            {
                product.count--;

                if (product.count > 0)

                    CartRepository.Commit();
                else
                    product.count = 1;
               
                return RedirectToAction("Index");

            }

            return RedirectToAction("NotFountPage", "Home");
        }


       
        public IActionResult Delete(int Id)
        {

            var userid = userManager.GetUserId(User);
            var product = CartRepository.Getone(new Func<IQueryable<Cart>, IQueryable<Cart>>[]
            {

            }, filter: e => e.ApplicationUserId == userid && e.ProductId == Id


            );
            if (product != null)
            {
                CartRepository.Delete(product);
                CartRepository.Commit();
                return RedirectToAction("Index");

            }

            return RedirectToAction("NotFountPage", "Home");
        }
        
        public IActionResult Pay()
        {

            var userid = userManager.GetUserId(User);



            var products = CartRepository.GetAll(new Func<IQueryable<Cart>, IQueryable<Cart>>[]
            {
    q=>q.Include(x=>x.Product).Where(e=>e.ApplicationUserId == userid)
            });

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/checkout/success",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/checkout/cancel",
            };

            foreach (var item in products.ToList())
            {
                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "egp",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Name,
                            Description = item.Product.Description,
                        },
                        UnitAmount = (long)item.Product.price * 100,
                    },
                    Quantity = item.count,

                });
            }
            var service = new SessionService();
            var session = service.Create(options);
            return Redirect(session.Url);
        }
           
        
    }
}
