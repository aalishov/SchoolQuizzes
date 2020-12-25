using SchoolQuizzes.Data.Models;
using SchoolQuizzes.Services.Mapping;

namespace SchoolQuizzes.Web.ViewModels.ClassRooms
{
    public class TakeDetailVM : IMapFrom<Take>
    {
        public int Id { get; set; }

        public string UserUsername { get; set; }

        public string Result { get; set; }
    }
}