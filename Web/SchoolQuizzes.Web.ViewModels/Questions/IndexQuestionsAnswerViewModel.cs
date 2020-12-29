using SchoolQuizzes.Data.Models;
using SchoolQuizzes.Services.Mapping;

namespace SchoolQuizzes.Web.ViewModels.Questions
{
    public class IndexQuestionsAnswerViewModel : IMapFrom<QuestionAnswer>
    {
        public int AnswerId { get; set; }

        public string AnswerValue { get; set; }
    }
}