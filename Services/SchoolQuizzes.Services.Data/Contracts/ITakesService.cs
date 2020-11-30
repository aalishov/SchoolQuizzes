namespace SchoolQuizzes.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Web.ViewModels.Takes;

    public interface ITakesService
    {
        public TakeQuestionAnswerViewModel GetExamQuestion(string userId, int id, int questionNumber = 0);

        public Task CreateTakeAsync(string userId, int quizId);

        public Task FinishQuizAsync( int takeId);

        public Task SaveTakedAnswerAsync(string userId, int questionId, int answerId);

        public ICollection<UserTakeViewModel> GetUserTakesByUserId(string userId);

        public string GetResult(int takeId);

        public bool IsUserHasNotFinishedQuiz(string userId);

        public Take GetTakeById(int takeId);
    }
}
