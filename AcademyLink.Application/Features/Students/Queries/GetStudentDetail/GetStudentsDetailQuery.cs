using MediatR;

namespace AcademyLink.Application.Features.Students.Queries.GetStudentDetail
{
    public class GetStudentDetailQuery : IRequest<StudentDetailVm>
    {
        public int Id { get; set; }
    }
}
