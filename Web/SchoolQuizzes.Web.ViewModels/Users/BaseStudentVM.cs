namespace SchoolQuizzes.Web.ViewModels.Users
{
    using AutoMapper;
    using AutoMapper.Configuration.Annotations;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class BaseStudentVM : IMapFrom<Student>, IMapTo<Student>
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        [Display(Name = "Клас")]
        public int StageId { get; set; }

        [Ignore]
        public string StageName { get; set; }


        [Ignore]
        public SelectList Stages { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
        }
    }
}
