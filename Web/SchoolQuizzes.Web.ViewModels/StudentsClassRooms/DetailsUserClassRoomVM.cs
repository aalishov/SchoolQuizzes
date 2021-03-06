﻿namespace SchoolQuizzes.Web.ViewModels.StudentsClassRooms
{
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;

    using System.Collections.Generic;

    public class DetailsUserClassRoomVM : IMapFrom<ClassRoom>
    {
        public int Id { get; set; }

        public string StageName { get; set; }

        public string CategoryName { get; set; }

        public string TeacherApplicationUserUserName { get; set; }

        public ICollection<StudentClassRoomQuizListVM> ClassRoomQuizzes { get; set; }
    }
}
