using Microsoft.EntityFrameworkCore;
using Someren_Database.Data;
using Someren_Database.Repositories;

namespace Someren_Database
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			// Register ApplicationDbContext for dependency injection
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("Someren_Database")));

            // Register IRoomRepository/IStudentsRepository with RoomRepository/DbStudentsRepository
            // for dependency injection
            builder.Services.AddScoped<IRoomRepository, RoomRepository>();
            builder.Services.AddSingleton<IActivityRepository, DbActivityRepository>();
			builder.Services.AddSingleton<IStudentsRepository, DbStudentsRepository>(); 
			builder.Services.AddSingleton<ITeachersRepository, DbTeachersRepository>();
			builder.Services.AddSingleton<IDrinksRepository, DbDrinksRepository>();
            builder.Services.AddControllersWithViews();		

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
