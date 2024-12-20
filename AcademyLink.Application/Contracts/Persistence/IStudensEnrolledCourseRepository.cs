using AcademyLink.Domain.Entities;
using AcademyLink.Domain.Enums;

namespace AcademyLink.Application.Contracts.Persistence
{
    public interface IStudensEnrolledCourseRepository : IAsyncRepository<StudensEnrolledCourse>
    {
        Task<bool> CourseAvailabilityCheck(int courseId);
        Task<bool> StudentExistsCheck(int studentId);
        Task<bool> CourseExistsCheck(int courseId);
        Task<bool> StudentInCourseExistsCheck(int StudentId, int CourseId);
        Task<bool> StatusCheck(EnrollmentStatus status);
    }
}
