namespace SchoolQuizzes.Data.Models
{
    using SchoolQuizzes.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    public class Teacher : BaseDeletableModel<int>
    {
        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<ClassRoom> ClassRooms { get; set; }
    }
}
