using AcademyLink.Application.Contracts.Persistence;
using AcademyLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AcademyLink.Persistence.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(AcademyLinkDBContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> IsCourseNameUnique(string name)
        {
            var matches = _dbContext.Courses.Any(e => (e.Name).Equals(name));
            return Task.FromResult(matches);
        }

        public Task<bool> IsCourseNameUnique(int courseId, string name)
        {
            var matches = _dbContext.Courses.Any(e => (e.Name).Equals(name) && e.CourseId != courseId);
            return Task.FromResult(matches);
        }
    }
}
