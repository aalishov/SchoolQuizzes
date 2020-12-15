namespace SchoolQuizzes.Data.Models
{
    using SchoolQuizzes.Data.Common.Models;
    using System.Collections.Generic;

    public class ClassRoom : BaseDeletableModel<int>

    {
        public ClassRoom()
        {
            this.Students = new HashSet<ClassRoomStudent>();
            this.ClassRoomQuizzes = new HashSet<ClassRoomQuiz>();
        }
        public string ClassRoomCode { get; set; }

        public int TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }

        public int StageId { get; set; }

        public virtual Stage Stage { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<ClassRoomStudent> Students { get; set; }

        public virtual ICollection<ClassRoomQuiz> ClassRoomQuizzes { get; set; }
    }
}
