namespace SchoolQuizzes.Web.ViewModels.ClassRooms
{
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ClassRoomQuizDetails : IMapFrom<ClassRoomQuiz>
    {
        public ClassRoomQuizDetails()
        {
            this.Takes = new List<TakeDetailVM>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsExam { get; set; }

        public ICollection<TakeDetailVM> Takes { get; set; }
    }
}
