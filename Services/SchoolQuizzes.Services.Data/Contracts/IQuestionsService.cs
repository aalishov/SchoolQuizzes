namespace SchoolQuizzes.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using SchoolQuizzes.Services.Data.ModelsDto;

    public interface IQuestionsService
    {
        Task CreateAsync(CreateQuestionDto questionDto);
    }
}
 