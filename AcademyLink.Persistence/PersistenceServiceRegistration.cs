using AcademyLink.Application.Contracts.Persistence;
using AcademyLink.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AcademyLink.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AcademyLinkDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AcademyLinkConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IStudensEnrolledCourseRepository, StudensEnrolledCourseRepository>();

            return services;
        }
    }
}
