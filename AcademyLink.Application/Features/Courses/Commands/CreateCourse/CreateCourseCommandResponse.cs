using AcademyLink.Application.Responses;

namespace AcademyLink.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommandResponse : BaseResponse
    {
        public CreateCourseCommandResponse() : base()
        {

        }

        public CreateCourseDto Course { get; set; } = default!;
    }
}
