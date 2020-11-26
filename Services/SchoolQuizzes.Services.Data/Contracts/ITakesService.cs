namespace SchoolQuizzes.Services.Data.Contracts
{
    using SchoolQuizzes.Web.ViewModels.Takes;

    public interface ITakesService
    {
        public TakeQuestionAnswerViewModel GetExamQuestion(int id, int questionNumber=0);
    }
}
