using E_commerse_study.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using E_commerse_study.ViewModel;

namespace E_commerse_study.Data
{
	public class AplicationDbContext:IdentityDbContext<ApplicationUser>
	{
        public AplicationDbContext(DbContextOptions<AplicationDbContext> dbContextOptions) 
			
			: base(dbContextOptions)
        {


        }
        public DbSet<Product> products { get; set; }
		public DbSet<Category> categories { get; set; }
        public DbSet<Company> companies { get; set; }
	    public DbSet<E_commerse_study.ViewModel.ApplicatinUserVM> ApplicatinUserVM { get; set; } = default!;
	    public DbSet<E_commerse_study.ViewModel.loginVM> loginVM { get; set; } = default!;

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
