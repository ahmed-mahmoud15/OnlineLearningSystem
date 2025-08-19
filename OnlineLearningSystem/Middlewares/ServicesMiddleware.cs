using OnlineLearningSystem.Services;

namespace OnlineLearningSystem.Middlewares
{
    public static class ServicesMiddleware
    {
        public static IServiceCollection AddServices(this IServiceCollection services) {

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IInstructorService, InstructorServic>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ILikeService, LikeService>();
            services.AddScoped<IFollowService, FollowService>();
            services.AddScoped<IPaymentService, PaymentService>();
            return services;
        }
    }
}
