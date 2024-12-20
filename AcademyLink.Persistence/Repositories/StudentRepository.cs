using AcademyLink.Application.Contracts.Persistence;
using AcademyLink.Domain.Entities;

namespace AcademyLink.Persistence.Repositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(AcademyLinkDBContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> IsStudentNameAndEmailUnique(string studentName, string email)
        {
            var matches = _dbContext.Students.Any(e => (e.FirstName + " " +e.LastName).Equals(studentName) && e.Email.Equals(email));
            return Task.FromResult(matches);
        }

        public Task<bool> IsStudentNameAndEmailUnique(int studentId, string studentName, string email)
        {
            var matches = _dbContext.Students.Any(e => (e.FirstName + " " + e.LastName).Equals(studentName) && e.Email.Equals(email) && e.StudentId != studentId);
            return Task.FromResult(matches);
        }
    }
}
