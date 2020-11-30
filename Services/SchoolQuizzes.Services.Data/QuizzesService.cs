namespace SchoolQuizzes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Services.Data.ModelsDto;
    using SchoolQuizzes.Services.Mapping;
    using SchoolQuizzes.Web.ViewModels.Answers;
    using SchoolQuizzes.Web.ViewModels.Questions;
    using SchoolQuizzes.Web.ViewModels.Quizzes;
    using SchoolQuizzes.Web.ViewModels.Takes;

    public class QuizzesService : IQuizzesService
    {
        private readonly IDeletableEntityRepository<Quiz> quizisRepository;
        private readonly IAnswersService answersService;
        private readonly ICategoriesService categoriesService;
        private readonly IDifficultsService difficultsService;
        private readonly IQuestionsService questionsService;

        public QuizzesService(IDeletableEntityRepository<Quiz> quizisRepository, IAnswersService answersService, ICategoriesService categoriesService, IDifficultsService difficultsService, IQuestionsService questionsService)
        {
            this.quizisRepository = quizisRepository;
            this.answersService = answersService;
            this.categoriesService = categoriesService;
            this.difficultsService = difficultsService;
            this.questionsService = questionsService;
        }

        public async Task CreateAsync(GenerateQuizDto generateDto)
        {
            Quiz quiz = new Quiz();

            quiz.Title = generateDto.Title;
            quiz.CategoryId = generateDto.CategoryId;
            quiz.DifficultId = generateDto.DifficultId;
            quiz.AddedByUserId = generateDto.UserId;
            generateDto.Questions = this.questionsService.GetRandomQuestionsForQuiz(generateDto.CategoryId, generateDto.DifficultId, generateDto.Count);

            foreach (var question in generateDto.Questions)
            {
                quiz.Questions.Add(new QuizzesQuestions { QuestionId = question.Id });
            }

            await this.quizisRepository.AddAsync(quiz);
            await this.quizisRepository.SaveChangesAsync();
        }

        public string GetGuizNameById(int id)
        {
            return this.quizisRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id).Title;
        }

        public Quiz GetQuizById(int id)
        {
            return this.quizisRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public ICollection<Quiz> GetQuizzes()
        {
            return this.quizisRepository.All().ToList();
        }

        public DetailsQuizViewModel GetQuizWithQuestionsAndAnswersById(int id)
        {
            DetailsQuizViewModel model = this.quizisRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<DetailsQuizViewModel>()
                .FirstOrDefault(x => x.Id == id);

            model.Questions = this.questionsService.GetQuestionsByQuizId<QuestionQuizViewModel>(id);

            foreach (var question in model.Questions)
            {
                question.Answers = this.answersService.GetQuestionAnswersById<AnswerQuizViewModel>(question.Id);
            }

            return model;
        }

        public int GetQuizQuestionsCountByQuizId(int quizId)
        {
            return this.questionsService.GetQuestionsByQuizId<QuestionQuizViewModel>(quizId).Count();
        }
    }
}
