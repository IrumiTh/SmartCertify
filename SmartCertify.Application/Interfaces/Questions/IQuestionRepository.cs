using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCertify.Application.DTOs;
using SmartCertify.Domain.Entities;

namespace SmartCertify.Application.Interfaces.Questions
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Question>> GetAllQuestionAsync();

        Task<Question?> GetQuestionByIdAsync(int questionId);


        Task AddQuestionAsync(Question question);

        Task UpdateQuestionAsync(Question question);

        Task DeleteQuestionAsync(Question question);

        Task UpdateQuestionAndChoicesAsync(int id, QuestionDto dto);
    }
}
