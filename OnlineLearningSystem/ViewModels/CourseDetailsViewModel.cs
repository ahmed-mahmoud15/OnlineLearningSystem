namespace OnlineLearningSystem.ViewModels
{
    public class CourseDetailsViewModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get;set; }
        public int InstructorId { get; set; }
        public string InstructorName { get; set; }
        public string CategoryName { get; set; }
        public int Price { get; set; }
        public int CountLikes { get; set; }
        public int CountEnrolledStudents { get; set; }
        public int CountLessons { get; set; }
        public IEnumerable<ShowLessonCourseDetailsViewModel> Lessons { get; set; }
    }
}
