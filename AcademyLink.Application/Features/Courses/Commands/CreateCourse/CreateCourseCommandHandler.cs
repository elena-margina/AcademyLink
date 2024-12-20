using AcademyLink.Application.Contracts.Persistence;
using AcademyLink.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AcademyLink.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CreateCourseCommandResponse>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CreateCourseCommandHandler(IMapper mapper, ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _courseRepository = courseRepository;
        }

        public async Task<CreateCourseCommandResponse> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var createCourseCommandResponse = new CreateCourseCommandResponse();

            var validator = new CreateCourseCommandValidator(_courseRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                createCourseCommandResponse.Success = false;
                createCourseCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createCourseCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }
            if (createCourseCommandResponse.Success)
            {
                var course = new Course() { Name = request.Name, Description = request.Description, SeatsAvailable = request.SeatsAvailable, DateFrom = request.DateFrom, DateTo = request.DateTo };
                course = await _courseRepository.AddAsync(course);
                createCourseCommandResponse.Course = _mapper.Map<CreateCourseDto>(course);
            }

            return createCourseCommandResponse;
        }
    }
}
