using MediatR;
using AutoMapper;
using AcademyLink.Application.Contracts.Persistence;
using AcademyLink.Domain.Entities;

namespace AcademyLink.Application.Features.Courses.Queries.GetCoursesList
{
    public class GetCoursesListQueryHandler : IRequestHandler<GetCoursesListQuery, List<CourseListVm>>
    {
        private readonly IAsyncRepository<Course> _courseRepository;
        private readonly IMapper _mapper;

        public GetCoursesListQueryHandler(IMapper mapper, IAsyncRepository<Course> courseRepository)
        {
            _mapper = mapper;
            _courseRepository = courseRepository;
        }

        public async Task<List<CourseListVm>> Handle(GetCoursesListQuery request, CancellationToken cancellationToken)
        {
            var allCategories = (await _courseRepository.ListAllAsync()).OrderBy(x => x.Name);
            return _mapper.Map<List<CourseListVm>>(allCategories);
        }
    }
}
