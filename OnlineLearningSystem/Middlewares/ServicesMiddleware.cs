using OnlineLearningSystem.Services;

namespace OnlineLearningSystem.Middlewares
{
    public static class ServicesMiddleware
    {
        public static IServiceCollection AddServices(this IServiceCollection services) {

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IInstructorService, InstructorService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<IFollowService, FollowService>();
            services.AddScoped<ILikeService, LikeService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<IAdminService, AdminService>();

            return services;
        }
    }
}
