﻿
using AcademyLink.Application.Contracts.Persistence;
using AcademyLink.Application.Features.StudentEnrolledCourse.Commands.CreateStudentEnrolledCourse;
using FluentValidation;

namespace AcademyLink.Application.Features.StudentEnrolledCourse.Commands.UpdateStudentEnrolledCourse
{
    public class UpdateStudentEnrolledCourseProgressCommandValidator : AbstractValidator<UpdateStudentEnrolledCourseProgressCommand>
    {
        private readonly IStudensEnrolledCourseRepository _studentEnrolledCourseRepository;
        public UpdateStudentEnrolledCourseProgressCommandValidator(IStudensEnrolledCourseRepository studentEnrolledCourseRepository)
        {
            _studentEnrolledCourseRepository = studentEnrolledCourseRepository;

            RuleFor(p => p.StudentId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.CourseId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(e => e)
                .MustAsync(StatusCheck)
                .WithMessage("The status does not exists.");

            RuleFor(e => e)
                .MustAsync(StudentExistsCheck)
                .WithMessage("The student does not exists.");

            RuleFor(e => e)
                .MustAsync(CourseExistsCheck)
                .WithMessage("The course does not exists.");

            RuleFor(e => e)
                .MustAsync(StudentInCourseExistsCheck)
                .WithMessage("The student is not enrolled in this course.");

            RuleFor(e => e)
                .Must(e => !(e.Progress == 100 && e.Status != Domain.Enums.EnrollmentStatus.Completed)).WithMessage("If the pgrogress is 100% then the status must be Completed.");

            RuleFor(e => e)
                .Must(e => !(e.Progress != 100 && e.Status == Domain.Enums.EnrollmentStatus.Completed)).WithMessage("If the status is Completed then the pgrogress must be 100%.");

            RuleFor(e => e.Progress)
                .Must(e => e <= 100).WithMessage("{PropertyName} must be less or equal to 100%.");
        }

        private async Task<bool> StatusCheck(UpdateStudentEnrolledCourseProgressCommand e, CancellationToken token)
        {
            return await _studentEnrolledCourseRepository.StatusCheck(e.Status);
        }

        private async Task<bool> StudentExistsCheck(UpdateStudentEnrolledCourseProgressCommand e, CancellationToken token)
        {
            return await _studentEnrolledCourseRepository.StudentExistsCheck(e.StudentId);
        }

        private async Task<bool> CourseExistsCheck(UpdateStudentEnrolledCourseProgressCommand e, CancellationToken token)
        {
            return await _studentEnrolledCourseRepository.CourseExistsCheck(e.CourseId);
        }

        private async Task<bool> StudentInCourseExistsCheck(UpdateStudentEnrolledCourseProgressCommand e, CancellationToken token)
        {
            return await _studentEnrolledCourseRepository.StudentInCourseExistsCheck(e.StudentId, e.CourseId);
        }
    }
}
