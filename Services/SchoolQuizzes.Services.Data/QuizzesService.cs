namespace SchoolQuizzes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Services.Mapping;
    using SchoolQuizzes.Web.ViewModels.Answers;
    using SchoolQuizzes.Web.ViewModels.Questions;
    using SchoolQuizzes.Web.ViewModels.Quizzes;

    public class QuizzesService : IQuizzesService
    {

        private readonly IDeletableEntityRepository<Quiz> quizisRepository;
        private readonly IAnswersService answersService;
        private readonly IQuestionsService questionsService;
        private readonly IMapper mapper;

        public QuizzesService(IDeletableEntityRepository<Quiz> quizisRepository, IAnswersService answersService, IQuestionsService questionsServicer)
        {
            this.quizisRepository = quizisRepository;
            this.answersService = answersService;
            this.questionsService = questionsServicer;
            this.mapper = AutoMapperConfig.MapperInstance;
        }

        public async Task CreateAsync(GenerateQuizViewModel inputModel)
        {


            Quiz quiz = new Quiz();
            this.mapper.Map<GenerateQuizViewModel, Quiz>(inputModel, quiz);

            inputModel.Questions = this.questionsService.GetRandomQuestionsForQuiz(inputModel.CategoryId, inputModel.DifficultId, inputModel.StageId, inputModel.Count);

            foreach (var question in inputModel.Questions)
            {
                quiz.Questions.Add(new QuizQuestion { QuestionId = question.Id });
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

        public ICollection<T> GetQuizzes<T>(int page, int itemsPerPage)
        {
            return this.quizisRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage).To<T>().ToList();
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
                question.Answers = this.answersService.GetQuestionAnswersById<AnswerViewModel>(question.Id);
            }

            return model;
        }

        public int GetQuizQuestionsCountByQuizId(int quizId)
        {
            return this.questionsService.GetQuestionsByQuizId<QuestionQuizViewModel>(quizId).Count();
        }

        public ICollection<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.quizisRepository.AllAsNoTracking()
                        .Select(x => new { x.Id, x.Title })
                        .ToList()
                        .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Title))
                        .ToList();
        }

        public SelectList GetQuizzesByCategoryAndDifficultAsSelectList(int categoryId, int difficultId)
        {
            if (difficultId > 0)
            {
                return new SelectList(this.quizisRepository.AllAsNoTracking().Where(x => x.CategoryId == categoryId && x.DifficultId == difficultId), "Id", "Title");
            }
            else
            {
                return new SelectList(this.quizisRepository.AllAsNoTracking().Where(x => x.CategoryId == categoryId), "Id", "Title");
            }
        }

        public int GetQuizzesCount()
        {
            return this.quizisRepository.AllAsNoTracking().Count();
        }
    }
}
