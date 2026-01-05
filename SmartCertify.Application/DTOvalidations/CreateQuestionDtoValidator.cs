using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using SmartCertify.Application.DTOs;
using SmartCertify.Application.Interfaces.Courses;
using SmartCertify.Application.Interfaces.Questions;
using SmartCertify.Domain.Entities;

namespace SmartCertify.Application.DTOvalidations
{
    public class QuestionValidator : AbstractValidator<CreateQuestionDto>
    {
        public QuestionValidator()
        {
            RuleFor(q => q.QuestionText)
                .NotEmpty()
                .WithMessage("Question text is required.")
                .MaximumLength(500)
                .WithMessage("Question text cannot exceed 500 characters.");

            RuleFor(q => q.DifficultyLevel)
                .NotEmpty()
                .WithMessage("Difficulty level is required.")
                .MaximumLength(20)
                .WithMessage("Difficulty level cannot exceed 20 characters.");

            RuleFor(q => q.CourseId)
                .GreaterThan(0)
                .MustAsync(async (courseId, cancellation) =>
                await ICourseRepository.ExistsAsync(courseId))
                .WithMessage("CourseId does not exist.");
        }
    }

    public class UpdateQuestionValidator : AbstractValidator<UpdateQuestionDto>
    {
        public UpdateQuestionValidator()
        {
            RuleFor(q => q.QuestionText)
                .NotEmpty()
                .WithMessage("Question text is required.")
                .MaximumLength(500)
                .WithMessage("Question text cannot exceed 500 characters.");

            RuleFor(q => q.DifficultyLevel)
                .NotEmpty()
                .WithMessage("Difficulty level is required.")
                .MaximumLength(20)
                .WithMessage("Difficulty level cannot exceed 20 characters.");
        }
    }
}
