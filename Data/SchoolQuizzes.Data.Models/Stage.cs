namespace SchoolQuizzes.Data.Models
{
    using SchoolQuizzes.Data.Common.Models;

    using System.Collections.Generic;

    public class Stage : BaseDeletableModel<int>
    {
        public Stage()
        {
            this.Students = new HashSet<Student>();
            this.ClassRooms = new HashSet<ClassRoom>();
            this.Questions = new HashSet<Question>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Student>  Students { get; set; }

        public virtual ICollection<ClassRoom> ClassRooms { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
