using AcademyLink.Application.Contracts.Persistence;
using AcademyLink.Application.Features.Students.Commands.UpdateStudent;
using AcademyLink.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AcademyLink.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, UpdateCourseCommandResponse>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public UpdateCourseCommandHandler(IMapper mapper, ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _courseRepository = courseRepository;
        }

        public async Task<UpdateCourseCommandResponse> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var updateCourseCommandResponse = new UpdateCourseCommandResponse();
            var courseToUpdate = await _courseRepository.GetByIdAsync(request.CourseId);

            var validator = new UpdateCourseCommandValidator(_courseRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                updateCourseCommandResponse.Success = false;
                updateCourseCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    updateCourseCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }
            if (updateCourseCommandResponse.Success)
            {
                _mapper.Map(request, courseToUpdate, typeof(UpdateCourseCommand), typeof(Course));

                await _courseRepository.UpdateAsync(courseToUpdate);
            }

            return updateCourseCommandResponse;
        }
    }
}
