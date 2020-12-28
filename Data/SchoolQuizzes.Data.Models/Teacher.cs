﻿namespace SchoolQuizzes.Data.Models
{
    using SchoolQuizzes.Data.Common.Models;
    using SchoolQuizzes.Services.Mapping;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Teacher : BaseDeletableModel<int>, IMapFrom<Teacher>, IMapTo<Teacher>
    {
        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<ClassRoom> ClassRooms { get; set; }

            }
}
