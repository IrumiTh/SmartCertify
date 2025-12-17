using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCertify.Application.DTOs;

namespace SmartCertify.Application.Interfaces.Questions
{
    internal interface IQuestionService
    {
        Task<IEnumerable<QuestionDto>> GetAllQuestionAsync();

        Task<QuestionDto?> GetQuestionByIdAsync(int courseId);

        Task AddCourseAsync(CreateCourseDto createCourseDto);

        Task<bool> IsTitleDuplicateAsync(string title);

        Task UpdateCourseAsync(int courseId, UpdateCourseDto updateCourseDto);

        Task DeleteCourseAsync(int courseId);

        Task UpdateDescriptionAsync(int courseId, string descriptionDto);
    }
}
