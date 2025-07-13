namespace CinemaApp.Data.Seeding
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class RoleSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roles = { "Manager", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            if (userManager == null)
            {
                var user = new IdentityUser
                {
                    UserName = "manager@mail.com",
                    Email = "manager@mail.com"
                };

                var result = await userManager.CreateAsync(user, "123456");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Manager");
                }
            }
        }
    }
}
