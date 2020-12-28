namespace SchoolQuizzes.Services.Data.Contracts
{
    using SchoolQuizzes.Web.ViewModels.Administration.Dashboard;

    using System.Threading.Tasks;

    public interface IGetCountService
    {
        DashboardIndexViewModel GetCounts();

        public  Task<int> GetTeachersCountAsync();

        public  Task<int> GetStudentsCountAsync();

        public Task<int> GetAdminsCountAsync();

        public int GetCorrectAnswerCount();

        public int GetInCorrectAnswerCount();
    }
}
