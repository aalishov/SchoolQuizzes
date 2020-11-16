namespace SchoolQuizzes.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.ModelsDto;

    public interface IQuizzesService
    {
        Task CreateAsync(GenerateQuizDto questionDto);

        ICollection<Quiz> GetQuizzes();

        Quiz GetQuizById(int id);
    }
}
