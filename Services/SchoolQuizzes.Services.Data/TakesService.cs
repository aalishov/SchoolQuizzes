namespace SchoolQuizzes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Services.Mapping;
    using SchoolQuizzes.Web.ViewModels.Questions;
    using SchoolQuizzes.Web.ViewModels.Takes;

    public class TakesService : ITakesService
    {
        private const int ItemsPerPage = 1;

        private readonly IQuizzesService quizzesService;
        private readonly IAnswersService answersService;
        private readonly IQuestionsService questionsService;
        private readonly IDeletableEntityRepository<Take> takeRepository;
        private readonly IRepository<TakedAnswer> takedAnswerRepository;

        public TakesService(IQuizzesService quizzesService, IAnswersService answersService, IQuestionsService questionsService, IDeletableEntityRepository<Take> takeRepository, IRepository<TakedAnswer> takedAnswerRepository)
        {
            this.quizzesService = quizzesService;
            this.answersService = answersService;
            this.questionsService = questionsService;
            this.takeRepository = takeRepository;
            this.takedAnswerRepository = takedAnswerRepository;
        }

        public async Task CreateTakeAsync(string userId, int quizId)
        {
            Take take = new Take() { QuizId = quizId, UserId = userId };
            await this.takeRepository.AddAsync(take);
            await this.takeRepository.SaveChangesAsync();
        }

        public int GetCorrectAnswerCountByTakeId(int takeId)
        {
            int correctAnswers = 0;
            var quizId = this.takeRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == takeId).QuizId;
            var takes = this.takedAnswerRepository.AllAsNoTracking().Where(x => x.TakeId == takeId).ToList();

            int questionsCount = this.quizzesService.GetQuizQuestionsCountByQuizId(quizId);

            foreach (var take in takes)
            {
                bool isCorrect = this.questionsService.IsCorrectAnswer(take.QuestionId, take.AnswerId);
                if (isCorrect)
                {
                    correctAnswers++;
                }
            }

            return correctAnswers;
        }

        public int GetTakeQuizByUserId(string userId)
        {
            var notFinishedTake = this.takeRepository.All().FirstOrDefault(x => x.UserId == userId && x.IsFinished == false);
            if (notFinishedTake != null)
            {
                return notFinishedTake.QuizId;
            }

            return -1;
        }

        public async Task FinishQuizAsync(int takeId)
        {
            Take take = this.GetTakeById(takeId);
            take.IsFinished = true;
            this.takeRepository.Update(take);
            await this.takeRepository.SaveChangesAsync();
        }

        public TakeQuestionAnswerViewModel GetExamQuestion(string userId, int page, int num = 1)
        {
            int quizId = this.GetTakeQuizByUserId(userId);
            int takeId = this.GetTake(userId).Id;
            Quiz quiz = this.quizzesService.GetQuizById(quizId);
            QuestionQuizViewModel question = this.questionsService
                .GetQuestionsByQuizId<QuestionQuizViewModel>(quizId)
                .OrderByDescending(x => x.Id).Skip((page - 1) * ItemsPerPage)
                .Take(ItemsPerPage)
                .FirstOrDefault();

            TakeQuestionAnswerViewModel model = this.takeRepository.All()
                .Where(x => x.Id == takeId)
                .To<TakeQuestionAnswerViewModel>()
                .ToList()
                .FirstOrDefault();

            this.SetModelValue(page, takeId, question, model);
            return model;
        }

        public async Task SaveTakedAnswerAsync(string userId, int questionId, int answerId)
        {
            Take take = this.GetTake(userId);
            TakedAnswer takedAnswer = this.takedAnswerRepository.All().Where(x => x.QuestionId == questionId && x.TakeId == take.Id).FirstOrDefault();
            if (takedAnswer != null)
            {
                this.takedAnswerRepository.All().FirstOrDefault(x => x.TakeId == take.Id && x.QuestionId == questionId).AnswerId = answerId;
            }
            else
            {
                take.TakedAnswers.Add(new TakedAnswer { QuestionId = questionId, AnswerId = answerId });
            }

            this.takeRepository.Update(take);
            await this.takeRepository.SaveChangesAsync();
        }

        public bool IsUserHasNotFinishedQuiz(string userId)
        {
            return this.takeRepository.All().Any(x => x.UserId == userId && x.IsFinished == false);
        }

        public string GetResult(int takeId)
        {
            int questionsCount = this.GetQuestionsCountByTakeId(takeId);

            int correctAnswers = this.GetCorrectAnswerCountByTakeId(takeId);

            return $"{correctAnswers}/{questionsCount}";
        }

        public Take GetTakeById(int takeId)
        {
            return this.takeRepository.All().FirstOrDefault(x => x.Id == takeId);
        }

        public ICollection<UserTakeViewModel> GetUserTakesByUserId(string userId)
        {
            var takes = this.takeRepository
                 .AllAsNoTracking()
                 .Where(x => x.UserId == userId)
                 .To<UserTakeViewModel>()
                 .ToList();
            foreach (var take in takes)
            {
                take.Result = this.GetResult(take.Id);
            }

            return takes;
        }

        public int GetQuestionsCountByTakeId(int takeId)
        {
            return this.quizzesService.GetQuizQuestionsCountByQuizId(this.takeRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == takeId).QuizId);
        }

        private Take GetTake(string userId)
        {
            return this.takeRepository.All().FirstOrDefault(x => x.UserId == userId && x.IsFinished == false);
        }

        private int? SelectedAnswere(int questionId, int takeId)
        {
            TakedAnswer takedAnswer = this.takedAnswerRepository.All().Where(x => x.QuestionId == questionId && x.TakeId == takeId).FirstOrDefault();

            if (takedAnswer != null)
            {
                return takedAnswer.AnswerId;
            }

            return null;
        }

        private void SetModelValue(int page, int takeId, QuestionQuizViewModel question, TakeQuestionAnswerViewModel model)
        {
            model.ItemsPerPage = ItemsPerPage;
            model.PageNumber = page;
            model.ElementsCount = model.QuizQuestionsCount;
            model.CurrentQuestionId = question.Id;
            model.QuestionValue = question.Value;
            model.AverageRating = this.questionsService.GetQuestionRatingById(question.Id);
            model.Answers = this.answersService.GetQuestionAnswersForTakesById(model.CurrentQuestionId);
            model.TakenAnswer = this.SelectedAnswere(question.Id, takeId);
        }
    }
}
