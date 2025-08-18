using Microsoft.AspNetCore.Identity;
using OnlineLearningSystem.Data;

namespace OnlineLearningSystem.Middlewares
{
    public static class IdentityMiddleware
    {
        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ApplicationDbContext>();
            return services;
        }
    }
}
