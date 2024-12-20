using AcademyLink.Domain.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace AcademyLink.Application.Features.StudentEnrolledCourse.Commands.UpdateStudentEnrolledCourse
{
    public class UpdateStudentEnrolledCourseProgressCommand : IRequest<UpdateStudentEnrolledCourseProgressCommandResponse>
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public decimal Progress { get; set; } 
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EnrollmentStatus Status { get; set; } 
    }
}
