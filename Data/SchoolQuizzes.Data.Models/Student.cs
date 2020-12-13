namespace SchoolQuizzes.Data.Models
{
    using SchoolQuizzes.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Student : BaseDeletableModel<int>
    {
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public int? StageId { get; set; }

        public virtual Stage Stage { get; set; }
    }
}
