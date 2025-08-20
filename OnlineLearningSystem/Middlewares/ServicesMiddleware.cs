using OnlineLearningSystem.Services;

namespace OnlineLearningSystem.Middlewares
{
    public static class ServicesMiddleware
    {
        public static IServiceCollection AddServices(this IServiceCollection services) {

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IInstructorService, InstructorService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ICategoryService, CategoryService>();
            return services;
        }
    }
}
