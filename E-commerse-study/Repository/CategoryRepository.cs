using E_commerse_study.Data;
using E_commerse_study.Models;
using E_commerse_study.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace E_commerse_study.Repository
{


    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {

      //  private readonly AplicationDbContext db;
        public CategoryRepository(AplicationDbContext db) : base(db)
        {

        }
    }
}
