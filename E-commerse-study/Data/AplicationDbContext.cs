using E_commerse_study.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerse_study.Data
{
	public class AplicationDbContext:DbContext
	{
        public AplicationDbContext(DbContextOptions<AplicationDbContext> dbContextOptions) 
			
			: base(dbContextOptions)
        {

        }
        public DbSet<Product> products { get; set; }
		public DbSet<Category> categories { get; set; }
        public DbSet<Company> companies { get; set; }

  //      public AplicationDbContext()
		//{

		//}

  //      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	base.OnConfiguring(optionsBuilder);

		//	var connection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
				
		//		optionsBuilder.UseSqlServer(connection);
		//}
	}
}
