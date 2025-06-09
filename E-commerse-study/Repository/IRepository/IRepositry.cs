using System.Linq.Expressions;
using E_commerse_study.Models;

namespace E_commerse_study.Repository.IRepository
{
    public interface IRepositry<T> where T : class
    {
        public IEnumerable<T> GetAll(Expression<Func<T, object>>[]? products = null, Expression<Func<T, bool>>? expression = null)
;
        public T? Getone(Expression<Func<T, bool>> expression);

        public void Add(T category);


        public void Edit(T category);

        public void Delete(T category);

        public void Commit();

    }
}
