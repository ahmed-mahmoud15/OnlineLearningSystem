namespace OnlineLearningSystem.ViewModels
{
    public class ShowCourseInStudentProfileViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime EnrollDate { get; set; }

        public float Prgress { get; set; }
    }
}
