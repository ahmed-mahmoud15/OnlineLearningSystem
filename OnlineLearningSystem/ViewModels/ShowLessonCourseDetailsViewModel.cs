namespace OnlineLearningSystem.ViewModels
{
    public class ShowLessonCourseDetailsViewModel
    {
        public int LessonId { get; set; }
        public int CourseId { get; set; }
        public int InstructorId { get; set; }
        public int SequenceNumber { get; set; }
        public string LessonTitle { get; set; }
        public string LessonType { get; set; }
    }
}
