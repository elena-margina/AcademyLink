using AcademyLink.Application.Responses;

namespace AcademyLink.Application.Features.StudentEnrolledCourse.Commands.CreateStudentEnrolledCourse
{
    public class CreateStudentEnrolledCourseCommandResponse : BaseResponse
    {
        public CreateStudentEnrolledCourseCommandResponse() : base()
        {

        }

        public CreateStudentEnrolledCourseDto CreateStudentEnrolledCourse { get; set; } = default!;
    }
}
