using AcademyLink.Application.Contracts.Persistence;
using AcademyLink.Application.Features.Courses.Commands.CreateCourse;
using AcademyLink.Application.Features.Courses.Commands.UpdateCourse;
using AcademyLink.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AcademyLink.Application.Features.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, DeleteCourseCommandResponse>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public DeleteCourseCommandHandler(IMapper mapper, ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _courseRepository = courseRepository;
        }

        public async Task<DeleteCourseCommandResponse> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var deleteCourseCommandResponse = new DeleteCourseCommandResponse();
            var validator = new DeleteCourseCommandValidator(_courseRepository);

            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                deleteCourseCommandResponse.Success = false;
                deleteCourseCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    deleteCourseCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }
            if (deleteCourseCommandResponse.Success)
            {
                var courseToDelete = await _courseRepository.GetByIdAsync(request.CourseId);
                await _courseRepository.DeleteAsync(courseToDelete);
            }
            return deleteCourseCommandResponse;
        }
    }
}
