namespace OnlineLearningSystem.ViewModels
{
    public class ShowCoursesInfoViewModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public string CategoryName { get; set; }

        public string InstructorName { get; set; }

        public int InstructorId { get; set; }

        public int NumberOfLessons { get; set; }

        public int NumberOfLikes { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Price { get; set; }
    }
}
