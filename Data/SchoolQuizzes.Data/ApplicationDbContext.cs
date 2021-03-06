﻿namespace SchoolQuizzes.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using SchoolQuizzes.Data.Common.Models;
    using SchoolQuizzes.Data.Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(ApplicationDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }

        public virtual DbSet<ClassRoom> ClassRooms { get; set; }

        public virtual DbSet<ClassRoomStudent> ClassRoomStudents { get; set; }

        public virtual DbSet<ClassRoomQuiz> ClassRoomQuizzes { get; set; }

        public virtual DbSet<Take> Takes { get; set; }

        public virtual DbSet<TakedAnswer> TakedAnswers { get; set; }

        public virtual DbSet<Teacher> Teachers { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Difficult> DifficultLevels { get; set; }

        public virtual DbSet<Question> Questions { get; set; }

        public virtual DbSet<QuestionAnswer> QuestionsAnswers { get; set; }

        public virtual DbSet<Quiz> Quizzes { get; set; }

        public virtual DbSet<QuizQuestion> QuizisQuestions { get; set; }

        public virtual DbSet<Rating> Ratings { get; set; }

        public virtual DbSet<Stage> Stages { get; set; }

        public virtual DbSet<Student> Students { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            this.ConfigureUserIdentityRelations(builder);

            EntityIndexesConfiguration.Configure(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            _ = builder.Entity<QuestionAnswer>().HasKey(x => new { x.AnswerId, x.QuestionId });
            _ = builder.Entity<QuizQuestion>().HasKey(x => new { x.QuestionId, x.QuizId });
            _ = builder.Entity<TakedAnswer>().HasKey(x => new { x.QuestionId, x.TakeId });
            _ = builder.Entity<ClassRoomStudent>().HasKey(x => new { x.ClassRoomId, x.StudentId });

            _ = builder.Entity<ApplicationUser>()
                .HasOne<Student>(s => s.Student)
                .WithOne(a => a.ApplicationUser)
                .HasForeignKey<Student>(s => s.ApplicationUserId);

            _ = builder.Entity<ApplicationUser>()
                .HasOne<Teacher>(t => t.Teacher)
                .WithOne(a => a.ApplicationUser)
                .HasForeignKey<Teacher>(t => t.ApplicationUserId);

            _ = builder.Entity<ClassRoom>().HasIndex(c => c.ClassRoomCode).IsUnique();
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletableEntity
        {
            _ = builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        // Applies configurations
        private void ConfigureUserIdentityRelations(ModelBuilder builder)

             => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
