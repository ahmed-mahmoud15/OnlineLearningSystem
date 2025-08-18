using System.Reflection;

namespace OnlineLearningSystem.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public int CourseId { get; set; }

        public string Title { get; set; }

        public int SequenceNumber { get; set; }

        public Type Type { get; set; }

        public string FilePath { get; set; }

        public Course Course { get; set; }
    }
}
