using Microsoft.AspNetCore.Identity;
using OnlineLearningSystem.Data;

namespace OnlineLearningSystem.Middlewares
{
    public static class IdentityMiddleware
    {
        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();
            return services;
        }

        public static async Task<IServiceProvider> AddCustomRolesAsync(this IServiceProvider services)
        {


            var roleManager = services.CreateScope().ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Student", "Instructor", "Admin" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            return services;
        }
    }
}
