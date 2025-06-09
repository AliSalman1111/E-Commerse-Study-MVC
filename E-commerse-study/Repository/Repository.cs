using System.Linq.Expressions;
using E_commerse_study.Data;
using E_commerse_study.Models;
using E_commerse_study.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace E_commerse_study.Repository
{
    public class Repository<T> : IRepositry<T> where T : class
    {
      

        AplicationDbContext db;//= new AplicationDbContext();
        DbSet<T>dbset;
        public Repository(AplicationDbContext db)
        {
            this.db = db;
           dbset= db.Set<T>();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, object>>[]? products = null, Expression<Func<T, bool>>? expression = null)
        {
            IQueryable<T> query = dbset;
            if (products != null)
            {


                foreach (var product in products)
                {
                    query = query.Include(product);

                }
                return query.ToList();


            }
            else if(expression != null)
            {
                return query.Where(expression).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        public T? Getone(Expression <Func<T,bool>> expression)
        {

            return dbset.Where(expression).FirstOrDefault();


        }
        public void Add(T entity)
        {

            dbset.Add(entity);

        }

        public void Edit(T entity)
        {
            dbset.Update(entity);
        }
        public void Delete(T entity)
        {
            dbset.Remove(entity);
        }
        public void Commit()
        {
            db.SaveChanges();
        }
    }
}
