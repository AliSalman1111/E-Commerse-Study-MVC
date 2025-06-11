using E_commerse_study.Data;
using E_commerse_study.Models;
using E_commerse_study.Repository.IRepository;

namespace E_commerse_study.Repository
{
    public class CompanyRepositry : Repository<Company>,ICompanyRepositry
    {
        public CompanyRepositry(AplicationDbContext db) : base(db)
        {
        }
    }
}
