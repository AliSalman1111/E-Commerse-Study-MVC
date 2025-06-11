using E_commerse_study.Data;
using E_commerse_study.Models;
using E_commerse_study.Repository.IRepository;

namespace E_commerse_study.Repository
{
    public class ProductRepositry : Repository<Product>, IProductRepositry
    {
        public ProductRepositry(AplicationDbContext db) : base(db)
        {

        }
    }
}
