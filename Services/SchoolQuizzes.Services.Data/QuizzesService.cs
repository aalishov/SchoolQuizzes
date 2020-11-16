namespace SchoolQuizzes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Services.Data.ModelsDto;

    public class QuizzesService : IQuizzesService
    {
        private readonly IDeletableEntityRepository<Quiz> quizisRepository;
        public QuizzesService(IDeletableEntityRepository<Quiz> quizisRepository)
        {
            this.quizisRepository = quizisRepository;
        }

        public async Task CreateAsync(GenerateQuizDto generateDto)
        {
            Quiz quiz = new Quiz();

            quiz.Title = generateDto.Title;
            quiz.CategoryId = generateDto.CategoryId;
            quiz.DifficultId = generateDto.DifficultId;

            foreach (var question in generateDto.Questions)
            {
                quiz.Questions.Add(new QuizzesQuestions { QuestionId = question.Id });
            }

            await this.quizisRepository.AddAsync(quiz);
            await this.quizisRepository.SaveChangesAsync();
        }

        public ICollection<Quiz> GetQuizzes()
        {
            return this.quizisRepository.All().ToList();
        }
    }
}
