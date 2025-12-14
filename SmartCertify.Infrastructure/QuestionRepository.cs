using Microsoft.EntityFrameworkCore;
using SmartCertify.Application.Interfaces.Questions;
using SmartCertify.Domain.Entities;

namespace SmartCertify.Infrastructure
{
    internal class QuestionRepository : IQuestionRepository
    {
        private readonly SmartCertifyContext _dbContext;

        public QuestionRepository(SmartCertifyContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task AddQuestionAsync(Question question)
        {
            _dbContext.Questions.Add(question);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteQuestionAsync(Question question)
        {
            _dbContext.Questions.Remove(question);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Question>> GetAllQuestionAsync()
        {
            return await _dbContext.Questions.ToListAsync();
        }

        public async Task<Question?> GetQuestionByIdAsync(int questionId)
        {
            return await _dbContext.Questions.FindAsync(questionId);
        }

        public async Task UpdateQuestionAsync(Question question)
        {
            _dbContext.Questions.Update(question);
            await _dbContext.SaveChangesAsync();
        }

    }
}
