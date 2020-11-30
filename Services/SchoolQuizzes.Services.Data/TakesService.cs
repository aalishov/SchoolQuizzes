namespace SchoolQuizzes.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;
    using SchoolQuizzes.Services.Mapping;
    using SchoolQuizzes.Web.ViewModels.Questions;
    using SchoolQuizzes.Web.ViewModels.Quizzes;
    using SchoolQuizzes.Web.ViewModels.Takes;

    public class TakesService : ITakesService
    {
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

        public async Task FinishQuizAsync(int takeId)
        {
            Take take = this.GetTakeById(takeId);
            take.IsFinished = true;
            this.takeRepository.Update(take);
            await this.takeRepository.SaveChangesAsync();
        }

        public TakeQuestionAnswerViewModel GetExamQuestion(string userId, int page, int num = 1)
        {
            int itemsPerPage = 1;
            int quizId = this.GetTakeQuizByUserId(userId);

            
            Quiz quiz = this.quizzesService.GetQuizById(quizId);
            int takeId = this.GetTake(userId).Id;
            ICollection<QuestionQuizViewModel> questions = this.questionsService.GetQuestionsByQuizId<QuestionQuizViewModel>(quizId);
            QuestionQuizViewModel question = questions
                .OrderByDescending(x => x.Id).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).FirstOrDefault();

            TakeQuestionAnswerViewModel model = new TakeQuestionAnswerViewModel();
            model.TakeId = takeId;
            model.ItemsPerPage = itemsPerPage;
            model.Title = quiz.Title;
            model.QuizId = quiz.Id;
            model.CurrentQuestionId = question.Id;
            model.QuestionsCount = questions.Count();
            model.Question = question.Value;
            model.PageNumber = page;
            model.ElementsCount = questions.Count();
            model.Answers = this.answersService.GetQuestionAnswersForTakesById(model.CurrentQuestionId);
            model.TakenAnswer = this.SelectedAnswere(question.Id, takeId);
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
                take.QuestionsCount = this.quizzesService.GetQuizQuestionsCountByQuizId(take.QuizId);
                take.Result = this.GetResult(take.Id);
            }

            return takes;
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

        private int GetTakeQuizByUserId(string userId)
        {
            var notFinishedTake = this.takeRepository.All().FirstOrDefault(x => x.UserId == userId && x.IsFinished == false);
            if (notFinishedTake != null)
            {
                return notFinishedTake.QuizId;
            }

            return -1;
        }


    }
}
