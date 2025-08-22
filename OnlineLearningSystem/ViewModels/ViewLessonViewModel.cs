namespace OnlineLearningSystem.ViewModels
{
    public class ViewLessonViewModel
    {
        public int LessonId { get; set; }
        public string LessonName { get; set; }
        public string LessonDescription { get; set; }
        public string FilePath { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public Models.Type LessonType { get; set; }
        public int SequenceNumber { get; set; }
    }
}
