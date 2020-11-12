namespace SchoolQuizzes.Services.Data.Contracts
{
    using SchoolQuizzes.Services.Data.ModelsDto;

    public interface IGetCountService
    {
        CountDto GetCounts();
    }
}
