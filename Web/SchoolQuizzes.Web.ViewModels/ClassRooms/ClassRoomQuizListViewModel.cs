namespace SchoolQuizzes.Web.ViewModels.ClassRooms
{
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;

    public class ClassRoomQuizListViewModel : IMapFrom<ClassRoomQuiz>
    {
        public int Id { get; set; }

        public int QuizId { get; set; }

        public string Title { get; set; }

        public bool IsExam { get; set; }

        public int TakesCount { get; set; }
    }
}
