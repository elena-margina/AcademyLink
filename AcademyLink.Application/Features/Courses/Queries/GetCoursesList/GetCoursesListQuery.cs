using MediatR;

namespace AcademyLink.Application.Features.Courses.Queries.GetCoursesList
{
    public class GetCoursesListQuery : IRequest<List<CourseListVm>>
    {
    }
}
