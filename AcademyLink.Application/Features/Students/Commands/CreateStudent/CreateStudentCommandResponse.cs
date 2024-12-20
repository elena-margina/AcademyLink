using AcademyLink.Application.Responses;

namespace AcademyLink.Application.Features.Students.Commands.CreateStudent
{
    public class CreateStudentCommandResponse: BaseResponse
    {
        public CreateStudentCommandResponse() : base() 
        { 

        }

        public CreateStudentDto Student { get; set; } = default!;
    }
}
