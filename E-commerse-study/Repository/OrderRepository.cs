using E_commerse_study.Data;
using E_commerse_study.Models;
using E_commerse_study.Repository.IRepository;

namespace E_commerse_study.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AplicationDbContext db) : base(db)
        {

        }
    }
}
