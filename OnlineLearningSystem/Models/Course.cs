namespace OnlineLearningSystem.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public DateTime CreationDate { get; set; }
        public int InstructorId { get; set; }
        public int CategoryId { get; set; }

        public Instructor Instructor { get; set; }
        public Category Category { get; set; }

        public ICollection<Like> LikedBy { get; set; } = new List<Like>();
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
