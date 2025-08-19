namespace OnlineLearningSystem.Models
{
    public class Student : User
    {
        public string? GitHubAccount { get; set; }

        public int Coins { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<Follow> Follows { get; set; } = new List<Follow>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();

    }
}
