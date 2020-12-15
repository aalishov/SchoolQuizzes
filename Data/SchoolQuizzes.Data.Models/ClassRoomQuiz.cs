namespace SchoolQuizzes.Data.Models
{
    using SchoolQuizzes.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ClassRoomQuiz : BaseDeletableModel<int>
    {
        public ClassRoomQuiz()
        {
            this.Takes = new HashSet<Take>();
        }
        public int ClassRoomId { get; set; }

        public virtual ClassRoom ClassRoom { get; set; }

        public string Title { get; set; }

        public int QuizId { get; set; }

        public Quiz Quiz { get; set; }

        public bool IsExam { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Take> Takes { get; set; }
    }
}
