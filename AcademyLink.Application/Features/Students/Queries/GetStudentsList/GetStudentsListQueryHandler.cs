using AcademyLink.Application.Contracts.Persistence;
using AcademyLink.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AcademyLink.Application.Features.Students.Queries.GetStudentsList
{
    public class GetStudentsListQueryHandler : IRequestHandler<GetStudentsListQuery, List<StudentListVm>>
    {
        private readonly IAsyncRepository<Student> _studentRepository;
        private readonly IMapper _mapper;

        public GetStudentsListQueryHandler(IMapper mapper, IAsyncRepository<Student> studentRepository)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
        }

        public async Task<List<StudentListVm>> Handle(GetStudentsListQuery request, CancellationToken cancellationToken)
        {
            var allStudents = (await _studentRepository.ListAllAsync()).OrderBy(x => x.FirstName);
            return _mapper.Map<List<StudentListVm>>(allStudents);
        }
    }
}
