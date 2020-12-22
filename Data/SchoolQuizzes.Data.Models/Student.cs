namespace SchoolQuizzes.Data.Models
{
    using SchoolQuizzes.Data.Common.Models;

    using System.Collections.Generic;

    public class Student : BaseDeletableModel<int>
    {
        public Student()
        {
            this.ClassRooms = new HashSet<ClassRoomStudent>();
        }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public int? StageId { get; set; }

        public virtual Stage Stage { get; set; }

        public virtual ICollection<ClassRoomStudent> ClassRooms { get; set; }

    }
}
