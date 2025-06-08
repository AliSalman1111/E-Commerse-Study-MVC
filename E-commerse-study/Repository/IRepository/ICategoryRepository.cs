using E_commerse_study.Models;

namespace E_commerse_study.Repository.IRepository
{
    public interface ICategoryRepository
    {
        public List<Category> GetAll(string? products = null);

        public Category? Getone(int Id);

        public void Add(Category category);


        public void Edit(Category category);

        public void Delete(Category category);

        public void Commit();
     
    }
}
