using AcademyLink.Application.Features.StudentEnrolledCourse.Commands.CreateStudentEnrolledCourse;
using AcademyLink.Application.Features.StudentEnrolledCourse.Commands.UpdateStudentEnrolledCourse;
using AcademyLink.Application.Features.StudentEnrolledCourse.Queries.GetStudentEnrolledCourseList;
using AcademyLink.Application.Features.Students.Commands.UpdateStudent;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AcademyLink.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentEnrolledCourseController : Controller
    {
        private readonly IMediator _mediator;

        public StudentEnrolledCourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetStudentEnrolledCourses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<StudentEnrolledCoursesListVm>> GetAllStudentEnrolledCourses(int studentId)
        {
            var getStEnrCQuery = new GetStudentEnrolledCoursesListQuery() { StudentId = studentId };
            var res = await _mediator.Send(getStEnrCQuery);
            return Ok(res);
        }

        [HttpPost("EnrollStudentInCourse")]
        public async Task<ActionResult<CreateStudentEnrolledCourseCommandResponse>> Create([FromBody] CreateStudentEnrolledCourseCommand createStudentEnrolledCourseCommand)
        {
            var response = await _mediator.Send(createStudentEnrolledCourseCommand);
            return Ok(response);
        }

        [HttpPut("ProcessStudentEnrollmentAndUpdateCourse")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<UpdateStudentEnrolledCourseProgressCommandResponse>> ProcessStudentEnrollmentAndUpdateCourse([FromBody] UpdateStudentEnrolledCourseProgressCommand updateStudentProgressCommand)
        {
            var response = await _mediator.Send(updateStudentProgressCommand);
            return Ok(response);
        }
    }
}
