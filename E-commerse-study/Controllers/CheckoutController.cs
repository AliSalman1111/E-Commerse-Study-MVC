using E_commerse_study.Models;
using E_commerse_study.Repository;
using E_commerse_study.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerse_study.Controllers
{
    public class CheckoutController : Controller
    {

        IOrderItemRepository orderItemRepository;
        IOrderRepository orderRepository;
        ICartRepository CartRepository;
        UserManager<ApplicationUser> userManager;
       public CheckoutController(IOrderItemRepository orderItemRepository, IOrderRepository orderRepository, ICartRepository CartRepository, UserManager<ApplicationUser> userManager)
        {
            this.orderItemRepository = orderItemRepository;
            this.orderRepository = orderRepository;
            this.CartRepository = CartRepository;   
            this.userManager = userManager;
                
                }
        public IActionResult Success()
        {
           
            return View();
        }
        [HttpPost,HttpGet]
        public IActionResult successes()
        {
            var userId = userManager.GetUserId(User);

            // 1. استرجاع محتوى السلة
            var cartItems = CartRepository.GetAll(new Func<IQueryable<Cart>, IQueryable<Cart>>[]
            {
        q => q.Include(c => c.Product).Where(c => c.ApplicationUserId == userId)
            }).ToList();

            if (!cartItems.Any())
            {
                return RedirectToAction("Index", "Home"); 
            }
          
            // 2. إنشاء الطلب
            var order = new Order
            {
                ApplicationUserId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = (decimal)cartItems.Sum(i => i.Product.price * i.count),
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in cartItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.count,
                    UnitPrice = (decimal)item.Product.price
                });
            }

            // 3. إضافة الطلب لقاعدة البيانات
            orderRepository.Add(order);
            orderRepository.Commit();
            // 4. حذف المنتجات من السلة
            foreach (var item in cartItems)
            {
                CartRepository.Delete(item);
            }
            CartRepository.Commit();



            return View("Index", "Home"); // يعرض صفحة success
        }

        public IActionResult Cancel()
        {
            
            return View();
        }
    }
}
