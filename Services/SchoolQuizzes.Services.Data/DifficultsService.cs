namespace SchoolQuizzes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;

    public class DifficultsService : IDifficultsService
    {
        private readonly IDeletableEntityRepository<Difficult> difficultRepository;

        public DifficultsService(IDeletableEntityRepository<Difficult> difficultRepository)
        {
            this.difficultRepository = difficultRepository;
        }

        public ICollection<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.difficultRepository.AllAsNoTracking()
                  .Select(x => new { x.Id, x.Name })
                  .ToList()
                  .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name))
                  .ToList();
        }

        public string GetDifficultNameById(int id)
        {
            return this.difficultRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id).Name;
        }
    }
}
