namespace OnlineLearningSystem.ViewModels
{
    public class ShowCoursesInHomeViewModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public string CategoryName { get; set; }

        public string InstructorName { get; set; }

        public int InstructorId { get; set; }

        public int NumberOfLessons { get; set; }

        public int NumberOfLikes { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
