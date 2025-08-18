namespace OnlineLearningSystem.Models
{
    public class Instructor : User
    {
        public string Experience { get; set; }
        public string LinkedInProfile { get; set; }

        public int YearsOfTeaching { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Follow> FollowedBy { get; set; } = new List<Follow>();
    }
}
