using E_commerse_study.Data;
using E_commerse_study.Models;
using E_commerse_study.Repository.IRepository;

namespace E_commerse_study.Repository
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(AplicationDbContext db) : base(db)
        {
        }
    }
}
