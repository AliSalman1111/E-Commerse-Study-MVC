using E_commerse_study.Data;
using E_commerse_study.Models;
using E_commerse_study.Repository.IRepository;

namespace E_commerse_study.Repository
{
    public class cartRepository : Repository<Cart>, ICartRepository
    {
        public cartRepository(AplicationDbContext db) : base(db)
        {
        }
    }
}
