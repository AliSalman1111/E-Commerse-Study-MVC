using System.Linq.Expressions;
using E_commerse_study.Data;
using E_commerse_study.Models;
using E_commerse_study.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public IEnumerable<T> GetAll(

    Func<IQueryable<T>, IQueryable<T>>[]? includes = null,
    Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbset;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = include(query);
                }
            }


            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            return query.ToList();
        }



        public T? Getone(Func<IQueryable<T>, IQueryable<T>>[]? includes = null,
    Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbset;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = include(query);
                }
            }


            if (filter != null)
            {
                query = query.Where(filter);
}
            //return query.ToList();
            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            return query.FirstOrDefault();


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
