using Microsoft.EntityFrameworkCore;
using AcademyLink.Domain.Entities;

namespace AcademyLink.Persistence
{
    public class AcademyLinkDBContext : DbContext
    {
        public AcademyLinkDBContext(DbContextOptions<AcademyLinkDBContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudensEnrolledCourse> StudensEnrolledCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AcademyLinkDBContext).Assembly);
         
        }
        
    }
}
