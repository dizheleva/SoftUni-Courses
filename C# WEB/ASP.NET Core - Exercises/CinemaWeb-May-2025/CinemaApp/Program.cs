namespace CinemaApp.Web
{
    using CinemaApp.Data;
    using CinemaApp.Data.Repository;
    using CinemaApp.Data.Repository.Interfaces;
    using CinemaApp.Data.Seeding;
    using CinemaApp.Services.Core.Interfaces;
    using CinemaApp.Services.Core.Services;
    using CinemaApp.Web.Infrastructure.Middlewares;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => 
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddScoped<IMovieRepository, MovieRepository>();

            builder.Services.AddScoped<IWatchlistRepository, WatchlistRepository>();

            builder.Services.AddScoped<IMovieService, MovieService>();

            builder.Services.AddScoped<IWatchlistService, WatchlistService>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseMiddleware<ManagerAccessMiddleware>();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            // Seed roles and initial data
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                RoleSeeder.SeedAsync(services);
            }

            app.Run();
        }
    }
}
