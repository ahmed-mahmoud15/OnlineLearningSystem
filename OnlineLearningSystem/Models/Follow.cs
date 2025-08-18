namespace OnlineLearningSystem.Models
{
    public class Follow
    {
        public int StudentId { get; set; }
        public int InstructorId { get; set; }

        public DateTime FollowDate { get; set; }

        public Student Student { get; set; }

        public Instructor Instructor { get; set; }
    }
}
