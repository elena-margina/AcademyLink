using AcademyLink.Application.Contracts.Persistence;
using AcademyLink.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AcademyLink.Application.Features.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand>
    {
        private readonly IAsyncRepository<Course> _courseRepository;
        private readonly IMapper _mapper;

        public DeleteCourseCommandHandler(IMapper mapper, IAsyncRepository<Course> courseRepository)
        {
            _mapper = mapper;
            _courseRepository = courseRepository;
        }

        public async Task Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var courseToDelete = await _courseRepository.GetByIdAsync(request.CourseId);

            await _courseRepository.DeleteAsync(courseToDelete);
        }
    }
}
