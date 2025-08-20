namespace OnlineLearningSystem.ViewModels
{
    public class ShowCourseInInstructorProfileViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CountLikes { get; set; }

        public int CountEnrolledStudents { get; set; }

        public int CountLessons { get; set; }
    }
}
