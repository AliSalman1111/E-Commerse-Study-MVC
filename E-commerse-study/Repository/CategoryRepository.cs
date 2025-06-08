using E_commerse_study.Data;
using E_commerse_study.Models;
using E_commerse_study.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace E_commerse_study.Repository
{


    public class CategoryRepository : ICategoryRepository
    {
        AplicationDbContext db;//= new AplicationDbContext();
        public CategoryRepository(AplicationDbContext db) 
        {
            this.db = db;
        }

        public List<Category> GetAll(string? products =null)
        {
            if (products == null)
            {
                return db.categories.ToList();
            }
            else
            {
                return db.categories.Include(products).ToList();
            }
        }
        public Category? Getone(int Id) {

            return db.categories.Find(Id);


        }
        public void Add(Category category) { 

               db.categories.Add(category);
        
        }

        public void Edit(Category category) {
               db.categories.Update(category);
        }
        public void Delete(Category category) {
            db.categories.Remove(category);
        }
        public void Commit()
        {
            db.SaveChanges();
        }
    }
}
