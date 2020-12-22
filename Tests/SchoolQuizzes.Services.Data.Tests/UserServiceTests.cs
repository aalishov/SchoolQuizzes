namespace SchoolQuizzes.Services.Data.Tests
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using SchoolQuizzes.Data;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Data.Repositories;
    using SchoolQuizzes.Services.Mapping;
    using SchoolQuizzes.Web.ViewModels;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("Serial")]
    public class UserServiceTests
    {
        public UserServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task TestAddTeacher()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestAddTeachersDb").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureDeleted();
            using var repository = new EfDeletableEntityRepository<BaseTeacher>(dbContext);
            var service = new UsersService(repository, null, null);

            dbContext.Teachers.Add(new BaseTeacher() { Id = 1, ApplicationUserId = "one" });
            dbContext.Teachers.Add(new BaseTeacher() { Id = 2, ApplicationUserId = "two" });
            dbContext.Teachers.Add(new BaseTeacher() { Id = 3, ApplicationUserId = "three" });
            await dbContext.SaveChangesAsync();

            await service.AddTeacher(new ApplicationUser() { Id = "newUser" });

            Assert.Equal(4, dbContext.Teachers.Count());
        }

        [Fact]
        public async Task TestAddStudent()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestAddStudentsDb").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureDeleted();
            using var repository = new EfDeletableEntityRepository<Student>(dbContext);
            var service = new UsersService(null, repository, null);

            dbContext.Students.Add(new Student() { Id = 1, ApplicationUserId = "one" });
            dbContext.Students.Add(new Student() { Id = 2, ApplicationUserId = "two" });
            dbContext.Students.Add(new Student() { Id = 3, ApplicationUserId = "three" });
            await dbContext.SaveChangesAsync();

            await service.AddStudent(new ApplicationUser() { Id = "newUser" });

            Assert.Equal(4, dbContext.Students.Count());
        }

        [Fact]
        public void TestGetStudentByUserId()
        {

            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseInMemoryDatabase(databaseName: "TestGetStudentsDb").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureDeleted();
            using var repository = new EfDeletableEntityRepository<Student>(dbContext);
            var service = new UsersService(null, repository, null);

            dbContext.Students.Add(new Student() { Id = 1, ApplicationUserId = "one" });
            dbContext.Students.Add(new Student() { Id = 2, ApplicationUserId = "two" });
            dbContext.Students.Add(new Student() { Id = 3, ApplicationUserId = "three" });
            dbContext.SaveChangesAsync().GetAwaiter().GetResult();

            var actual = service.GetStudentByUserId<BaseStudent>("two");

            Assert.Equal(2, actual.Id);

            actual = service.GetStudentByUserId<BaseStudent>("nive");
            Assert.Null(actual);
        }

        [Fact]
        public void TestGetAllStudentsByStageId()
        {

            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseInMemoryDatabase(databaseName: "TestGetAllStudentsDb").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureDeleted();
            using var repository = new EfDeletableEntityRepository<Student>(dbContext);
            var service = new UsersService(null, repository, null);

            ICollection<ClassRoomStudent> rooms = new List<ClassRoomStudent>() { new ClassRoomStudent { StudentId = 1, ClassRoomId = 1 } };
            dbContext.Students.Add(new Student() { Id = 1, ClassRooms = rooms, ApplicationUserId = "one", StageId = 2 });
            dbContext.Students.Add(new Student() { Id = 2, ApplicationUserId = "two", StageId = 1 });
            dbContext.Students.Add(new Student() { Id = 3, ApplicationUserId = "three", StageId = 2 });
            dbContext.Students.Add(new Student() { Id = 4, ApplicationUserId = "five", StageId = 3 });
            dbContext.Students.Add(new Student() { Id = 5, ApplicationUserId = "six", StageId = 2 });
            dbContext.SaveChangesAsync().GetAwaiter().GetResult();


            var actual = service.GetAllStudentsByStageId<BaseStudent>(1, 1);
            Assert.Equal(1, actual.Count);

            actual = service.GetAllStudentsByStageId<BaseStudent>(1, 8);
            Assert.Equal(0, actual.Count);
        }
    }

    public class BaseStudent : IMapFrom<Student>
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
    }

}
