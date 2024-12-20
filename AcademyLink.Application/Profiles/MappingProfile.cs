using AcademyLink.Application.Features.Courses.Commands.CreateCourse;
using AcademyLink.Application.Features.Courses.Commands.UpdateCourse;
using AcademyLink.Application.Features.Courses.Queries.GetCoursesList;
using AcademyLink.Application.Features.StudentEnrolledCourse.Commands.CreateStudentEnrolledCourse;
using AcademyLink.Application.Features.StudentEnrolledCourse.Queries.GetStudentEnrolledCourseList;
using AcademyLink.Application.Features.Students.Commands.CreateStudent;
using AcademyLink.Application.Features.Students.Commands.UpdateStudent;
using AcademyLink.Application.Features.Students.Queries.GetStudentsList;
using AcademyLink.Domain.Entities;
using AutoMapper;

namespace AcademyLink.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Student, StudentListVm>().ReverseMap();
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Student, StudentEnrolledCoursesListVm>().ReverseMap();

            CreateMap<Student, CreateStudentCommand>().ReverseMap();
            CreateMap<Student, UpdateStudentCommand>().ReverseMap();
            CreateMap<Student, CreateStudentCommand>().ReverseMap();
            CreateMap<Student, CreateStudentDto>().ReverseMap();

            CreateMap<Course, CourseListVm>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Course, CreateCourseCommand>().ReverseMap();
            CreateMap<Course, UpdateCourseCommand>().ReverseMap();
            CreateMap<Course, CreateCourseCommand>().ReverseMap();
            CreateMap<Course, CreateCourseDto>().ReverseMap();

            CreateMap<StudensEnrolledCourse, StudentEnrolledCoursesListVm>().ReverseMap();
            CreateMap<StudensEnrolledCourse, CreateStudentEnrolledCourseDto>().ReverseMap();

            CreateMap<StudensEnrolledCourse, StudentEnrolledCoursesListVm>()
            .ForMember(dest => dest.Course, opt => opt.Ignore()).ReverseMap();

            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.IsAvailable,
                           opt => opt.MapFrom(src => src.IsAvailable == 1 ? "Yes" : "No")) // Map 1/0 to "Yes"/"No"
                .ForMember(dest => dest.Duration,
                           opt => opt.MapFrom(src => (src.DateTo.Date - src.DateFrom.Date).Days)); // Calculate duration in days

            CreateMap<Student, StudentEnrolledCoursesListVm>()
                .ForMember(dest => dest.FullName,
                           opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ReverseMap();

            CreateMap<Student, CreateStudentEnrolledCourseDto>()
                .ForMember(dest => dest.FullName,
                           opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ReverseMap();

            CreateMap<StudensEnrolledCourse, CreateStudentEnrolledCourseDto>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.Student.FirstName} {src.Student.LastName}"))
                .ForMember(dest => dest.Course,
                    opt => opt.MapFrom(src => src.Course)); // Assuming CourseDto mapping is configured
        }
    }
}
