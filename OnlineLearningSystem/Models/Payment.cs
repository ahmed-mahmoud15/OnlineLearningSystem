using System.Diagnostics.Contracts;

namespace OnlineLearningSystem.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public int Amount { get; set; }
        
        public int StudentId { get; set; }

        public Student Student { get; set; }
    }
}
