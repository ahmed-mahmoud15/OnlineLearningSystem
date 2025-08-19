using OnlineLearningSystem.Repositories;

namespace OnlineLearningSystem.Middlewares
{
    public static class RepositoriesMiddleware
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services) {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IInstructorRepository, InstructorRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
