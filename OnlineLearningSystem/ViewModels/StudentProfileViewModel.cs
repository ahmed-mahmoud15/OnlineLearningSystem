namespace OnlineLearningSystem.ViewModels
{
    public class StudentProfileViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }

        public int Balance { get; set; }

        public string Bio {  get; set; }

        public string ProfilePhoto  { get; set; }

        public string Github { get; set; }

        public List<ShowCourseInStudentProfileViewModel> Courses { get; set; }
    }
}
