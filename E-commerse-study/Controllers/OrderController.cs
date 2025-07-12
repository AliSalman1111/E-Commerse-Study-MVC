using E_commerse_study.Models;
using E_commerse_study.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerse_study.Controllers
{
    public class OrderController : Controller
    {
        IOrderRepository orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public IActionResult Index()
        {
            var orders = orderRepository.GetAll(new Func<IQueryable<Models.Order>, IQueryable<Models.Order>>[]
            {
                q=>q.Include(P=>P.ApplicationUser).Include(p=>p.OrderItems).ThenInclude(p=>p.Product)
            });
            return View(orders);
        }
    }
}
