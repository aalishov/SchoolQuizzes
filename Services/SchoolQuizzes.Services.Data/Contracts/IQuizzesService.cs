namespace SchoolQuizzes.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.ModelsDto;
    using SchoolQuizzes.Web.ViewModels.Quizzes;

    public interface IQuizzesService
    {
        public Task CreateAsync(GenerateQuizDto questionDto);

        public string GetGuizNameById(int id);

        public ICollection<Quiz> GetQuizzes();

        public Quiz GetQuizById(int id);

        public int GetQuizQuestionsCountByQuizId(int quizId);

        public DetailsQuizViewModel GetQuizWithQuestionsAndAnswersById(int id);

        public ICollection<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

        public SelectList GetQuizzesByCategoryAndDifficultAsSelectList(int categoryId, int difficultId);
    }
}
