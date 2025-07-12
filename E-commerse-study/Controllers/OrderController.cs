using E_commerse_study.Models;
using E_commerse_study.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerse_study.Controllers
{
    public class OrderController : Controller
    {
        IOrderRepository orderRepository;
        UserManager<ApplicationUser> UserManager;
        public OrderController(IOrderRepository orderRepository, UserManager<ApplicationUser> UserManager)
        {
            this.orderRepository = orderRepository;
            this.UserManager = UserManager;
        }
        public IActionResult Index()
        {
            var orders = orderRepository.GetAll(new Func<IQueryable<Models.Order>, IQueryable<Models.Order>>[]
            {
                q=>q.Include(P=>P.ApplicationUser).Include(p=>p.OrderItems).ThenInclude(p=>p.Product)
            });
            return View(orders);
        }
        public IActionResult Index2()
        {

            var userId = UserManager.GetUserId(User);
            var orders = orderRepository.GetAll(new Func<IQueryable<Models.Order>, IQueryable<Models.Order>>[]
            {
                q=>q.Include(P=>P.ApplicationUser).Include(p=>p.OrderItems).ThenInclude(p=>p.Product).Where(e=>e.ApplicationUserId == userId)
            });
            return View(orders);
        }
    }
}
