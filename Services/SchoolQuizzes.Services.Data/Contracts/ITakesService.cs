namespace SchoolQuizzes.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Web.ViewModels.Takes;

    public interface ITakesService
    {
        public Task CreateTakeAsync(string userId, int quizId);

        public TakeQuestionAnswerViewModel GetExamQuestion(string userId, int id, int questionNumber = 0);

        public ICollection<UserTakeViewModel> GetUserTakesByUserId(string userId);

        public int GetTakeQuizByUserId(string userId);

        public int GetQuestionsCountByTakeId(int takeId);

        public Take GetTakeById(int takeId);

        public UserTakeDetailViewModel GetTakeDetailsById(int takeId);

        public string GetResult(int takeId);

        public int GetCorrectAnswerCountByTakeId(int takeId);

        public int GetInCorrectAnswerCountByTakeId(int takeId);

        public bool IsUserHasNotFinishedQuiz(string userId);

        public Task FinishQuizAsync(int takeId);

        public Task SaveTakedAnswerAsync(string userId, int questionId, int answerId);
    }
}
