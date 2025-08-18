namespace OnlineLearningSystem.Models
{
    public class Student
    {
        public string GitHubAccount { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<Follow> Follows { get; set; } = new List<Follow>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();

    }
}
