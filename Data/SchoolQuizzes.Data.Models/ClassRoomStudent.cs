namespace SchoolQuizzes.Data.Models
{
    public class ClassRoomStudent
    {
        public int ClassRoomId { get; set; }

        public virtual ClassRoom ClassRoom { get; set; }

        public int StudentId { get; set; }

        public virtual Student Student { get; set; }
    }
}
