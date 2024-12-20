using AcademyLink.Application.Features.Students.Commands.CreateStudent;
using AcademyLink.Application.Features.Students.Commands.DeleteStudent;
using AcademyLink.Application.Features.Students.Commands.UpdateStudent;
using AcademyLink.Application.Features.Students.Queries.GetStudentsList;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace AcademyLink.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<StudentListVm>>> GetAllStudents()
        {
            var dtos = await _mediator.Send(new GetStudentsListQuery());
            return Ok(dtos);
        }

        [HttpPost("AddStudent")]
        public async Task<ActionResult<CreateStudentCommandResponse>> Create([FromBody] CreateStudentCommand createStudentCommand)
        {
            var response = await _mediator.Send(createStudentCommand);
            return Ok(response);
        }

        [HttpPut("UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<UpdateStudentCommandResponse>> Update([FromBody] UpdateStudentCommand updateStudentCommand)
        {
            var response =  await _mediator.Send(updateStudentCommand);
            return Ok(response);
        }

        [HttpDelete("{id}/DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var deleteStudentCommand = new DeleteStudentCommand() { StudentId = id };
            await _mediator.Send(deleteStudentCommand);
            return NoContent();
        }
    }
}
