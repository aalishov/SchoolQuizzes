namespace SchoolQuizzes.Services.Data.Contracts
{
    using SchoolQuizzes.Web.ViewModels.Quizzes;

    using System.IO;

    public interface IExportService
    {

        public MemoryStream ExportQuizQuestions(DetailsQuizViewModel model);
    }
}
