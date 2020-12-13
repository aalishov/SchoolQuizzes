namespace SchoolQuizzes.Data.Models
{
    using SchoolQuizzes.Data.Common.Models;
    using System.Collections.Generic;

    public class ClassRoom : BaseDeletableModel<int>

    {
        public ClassRoom()
        {
            this.Students = new HashSet<Student>();
        }
        public string ClassRoomCode { get; set; }

        public int TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }

        public int StageId { get; set; }

        public virtual Stage Stage { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
