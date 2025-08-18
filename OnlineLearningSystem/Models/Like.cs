namespace OnlineLearningSystem.Models
{
    public class Like
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public DateTime LikedDate { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
