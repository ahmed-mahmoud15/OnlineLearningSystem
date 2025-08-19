using OnlineLearningSystem.Services;

namespace OnlineLearningSystem.Middlewares
{
    public static class ServicesMiddleware
    {
        public static IServiceCollection AddServices(this IServiceCollection services) {

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IStudentService, StudentService>();
            return services;
        }
    }
}
