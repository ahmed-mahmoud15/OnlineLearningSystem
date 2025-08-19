namespace OnlineLearningSystem.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public DateTime Date { get; set; }

        public int LastViewedLesson { get; set; }

        public float Progress { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
