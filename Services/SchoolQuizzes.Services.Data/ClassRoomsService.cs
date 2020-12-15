﻿namespace SchoolQuizzes.Services.Data
{
    using SchoolQuizzes.Data.Common.Repositories;
    using SchoolQuizzes.Data.Models;
    using SchoolQuizzes.Services.Data.Contracts;
    using System;
    using SchoolQuizzes.Services.Mapping;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ClassRoomsService : IClassRoomsService
    {
        private readonly IDeletableEntityRepository<ClassRoom> classRoom;
        private readonly IDeletableEntityRepository<Teacher> teachers;
        private readonly IDeletableEntityRepository<Student> students;
        private readonly IRepository<ClassRoomStudent> roomStudentRepository;

        public ClassRoomsService(IDeletableEntityRepository<ClassRoom> classRoom, IDeletableEntityRepository<Teacher> teachers, IDeletableEntityRepository<Student> students, IRepository<ClassRoomStudent> roomStudentRepository)
        {
            this.classRoom = classRoom;
            this.teachers = teachers;
            this.students = students;
            this.roomStudentRepository = roomStudentRepository;
        }

        public async Task AddStudentToClassRoomAsync(int roomId, int studentId)
        {
            Student student = this.students.All().FirstOrDefault(x => x.Id == studentId);
            ClassRoom classRoom = this.classRoom.All().FirstOrDefault(x => x.Id == roomId);
            await this.roomStudentRepository.AddAsync(new ClassRoomStudent() { ClassRoom = classRoom, Student = student });
            await roomStudentRepository.SaveChangesAsync();
        }
        public async Task CreateClassRoomAsync(string userTeacherId, int stageId, int categoryId)
        {
            var teacher = this.teachers.AllAsNoTracking().FirstOrDefault(x => x.ApplicationUserId == userTeacherId);

            var room = new ClassRoom()
            {
                ClassRoomCode = Guid.NewGuid().ToString(),
                TeacherId = teacher.Id,
                StageId = stageId,
                CategoryId = categoryId,
            };

            await this.classRoom.AddAsync(room);
            await this.classRoom.SaveChangesAsync();
        }

        public ICollection<T> GetRooms<T>(string userId)
        {
            return this.classRoom.AllAsNoTracking()
                .Where(x => x.Teacher.ApplicationUserId == userId)
                .To<T>()
                .ToList();
        }

        public T GerRoomById<T>(int id)
        {
            return this.classRoom.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public ICollection<Student> GetAllStudentsInClassRoom(int classRoomId)
        {
            return this.students.AllAsNoTracking()
                .Where(x => x.ClassRooms.Any(x => x.ClassRoomId == classRoomId))
                .ToList();
        }

        public int GetRoomStageId(int roomId)
        {
            return this.classRoom.AllAsNoTracking().FirstOrDefault(x => x.Id == roomId).StageId;
        }


    }
}
