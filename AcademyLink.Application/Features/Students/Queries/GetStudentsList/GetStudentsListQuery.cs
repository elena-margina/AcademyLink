using MediatR;

namespace AcademyLink.Application.Features.Students.Queries.GetStudentsList
{
    public class GetStudentsListQuery : IRequest<List<StudentListVm>>
    {
    }
}
