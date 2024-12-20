using AcademyLink.Domain.Entities;

namespace AcademyLink.Application.Contracts.Persistence
{
    public  interface IStudentRepository : IAsyncRepository<Student>
    {
        Task<bool> IsStudentNameAndEmailUnique(string studentName, string email);
        Task<bool> IsStudentNameAndEmailUnique(int studentId, string studentName, string email);
    }
}
