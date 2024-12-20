using AcademyLink.Domain.Entities;

namespace AcademyLink.Application.Contracts.Persistence
{
    public interface IEnrollmentRepository : IAsyncRepository<StudensEnrolledCourse>
    {
    }
}
