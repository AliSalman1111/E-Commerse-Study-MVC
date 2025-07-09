using E_commerse_study.Data;
using E_commerse_study.Models;
using E_commerse_study.Repository;
using E_commerse_study.Repository.IRepository;
using E_commerse_study.Static_Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace E_commerse_study
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AplicationDbContext>(
               option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
               );
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option => { option.Password.RequiredLength = 7;
            
          
            
            }).AddEntityFrameworkStores<AplicationDbContext>();

            builder.Services.AddScoped<IProductRepositry, ProductRepositry>();
            builder.Services.AddScoped<ICompanyRepositry, CompanyRepositry>();
            builder.Services.AddScoped<ICartRepository, cartRepository>();

            builder.Services.Configure<secretkey>(builder.Configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
