using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartCertify.Application.Interfaces.Questions;
using SmartCertify.Domain.Entities;
using SmartCertify.Application.DTOs;

namespace SmartCertify.Infrastructure
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly SmartCertifyContext _dbContext;
        private readonly IMapper _mapper;

        public QuestionRepository(SmartCertifyContext dbContext , IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
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

        public async Task UpdateQuestionAndChoicesAsync(int id, QuestionDto dto)
        {
            var question = await GetQuestionByIdAsync(id);
            if (question == null)
                throw new KeyNotFoundException("Question not found");

            // Map basic properties (excluding choices)
            _mapper.Map(dto, question);

            // Handle Choices separately
            var existingChoiceIds = question.Choices.Select(c => c.ChoiceId).ToList();
            var incomingChoiceIds = dto.Choices.Select(c => c.ChoiceId).ToList();

            // Find choices to delete
            var choicesToDelete = question.Choices.Where(c => !incomingChoiceIds.Contains(c.ChoiceId)).ToList();
            foreach (var choice in choicesToDelete)
            {
                _dbContext.Choices.Remove(choice);
            }

            // Update existing choices and add new ones
            foreach (var choiceDto in dto.Choices)
            {
                var existingChoice = question.Choices.FirstOrDefault(c => c.ChoiceId == choiceDto.ChoiceId);
                if (existingChoice != null)
                {
                    // Update existing choice
                    _mapper.Map(choiceDto, existingChoice);
                }
                else
                {
                    // Add new choice
                    var newChoice = _mapper.Map<Choice>(choiceDto);
                    question.Choices.Add(newChoice);
                }
            }

            await UpdateQuestionAsync(question);
        }

    }
}
