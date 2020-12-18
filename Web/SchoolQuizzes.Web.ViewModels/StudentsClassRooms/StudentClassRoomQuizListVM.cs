using SchoolQuizzes.Data.Models;
using SchoolQuizzes.Services.Mapping;

namespace SchoolQuizzes.Web.ViewModels.StudentsClassRooms
{
    public class StudentClassRoomQuizListVM : IMapFrom<ClassRoomQuiz>
    {
        public int Id { get; set; }

        public int QuizId { get; set; }

        public string Title { get; set; }

        public bool IsExam { get; set; }
    }
}