using AcademyLink.Application.Contracts.Persistence;
using AcademyLink.Domain.Entities;
using AcademyLink.Domain.Enums;


namespace AcademyLink.Persistence.Repositories
{
    public class StudensEnrolledCourseRepository : BaseRepository<StudensEnrolledCourse>, IStudensEnrolledCourseRepository
    {
        public StudensEnrolledCourseRepository(AcademyLinkDBContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> CourseAvailabilityCheck(int courseId) 
        {
            var matches = false;
            var studentEnrolledCourse = _dbContext.StudensEnrolledCourses.Where(e => e.CourseId == courseId);
            var course = _dbContext.Courses.Where(e => e.CourseId == courseId).FirstOrDefault();

            if (course != null && studentEnrolledCourse.Where(s => s.Status != EnrollmentStatus.Completed || s.Status != EnrollmentStatus.Cancelled).Count() < course.SeatsAvailable)
            {
                matches = true;
            }
            
            return Task.FromResult(matches);
        }

        public Task<bool> StudentExistsCheck(int studentId)
        {
            var exists = _dbContext.Students.Any(e => e.StudentId == studentId);
            return Task.FromResult(exists);
        }

        public Task<bool> CourseExistsCheck(int courseId)
        {
            var exists = _dbContext.Courses.Any(e => e.CourseId == courseId);
            return Task.FromResult(exists);
        }

        public Task<bool> StudentInCourseExistsCheck(int studentId, int courseId)
        {
            var exists = false;
            var studentEnrolledCourse = _dbContext.StudensEnrolledCourses.Where(e => e.CourseId == courseId && e.StudentId == studentId);
            if(studentEnrolledCourse.Count() >=1)
            {
                exists = true;
            }

            return Task.FromResult(exists);
        }

        public Task<bool> StatusCheck(EnrollmentStatus status)
        {
            var exists = Enum.IsDefined(typeof(EnrollmentStatus), status);
            return Task.FromResult(exists);
        }
    }
}
