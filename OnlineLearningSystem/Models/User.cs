namespace OnlineLearningSystem.Models
{
    public class User
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string? Bio { get; set; }

        public string? ProfilePhotoPath { get; set; }
    }
}
