using AcademyLink.Application.Features.Courses.Commands.CreateCourse;
using AcademyLink.Application.Features.Courses.Commands.DeleteCourse;
using AcademyLink.Application.Features.Courses.Commands.UpdateCourse;
using AcademyLink.Application.Features.Courses.Queries.GetCoursesList;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AcademyLink.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("GetAllCourses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CourseListVm>>> GetAllCourses()
        {
            var dtos = await _mediator.Send(new GetCoursesListQuery());
            return Ok(dtos);
        }

        [HttpPost("AddCourse")]
        public async Task<ActionResult<CreateCourseCommandResponse>> Create([FromBody] CreateCourseCommand createCourseCommand)
        {
            var response = await _mediator.Send(createCourseCommand);
            return Ok(response);
        }

        [HttpPut("UpdateCourse")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<UpdateCourseCommandResponse>> Update([FromBody] UpdateCourseCommand updateCourseCommand)
        {
            var response = await _mediator.Send(updateCourseCommand);
            return Ok(response);
        }

        [HttpDelete("DeleteCourse")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<DeleteCourseCommandResponse>> Delete(DeleteCourseCommand deleteCourseCommand)
        {
            var response =  await _mediator.Send(deleteCourseCommand);
            return Ok(response);
        }
    }
}
