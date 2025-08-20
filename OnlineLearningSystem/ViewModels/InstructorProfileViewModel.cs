namespace OnlineLearningSystem.ViewModels
{
    public class InstructorProfileViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }

        public int YearsOfExperience { get; set; }

        public string Bio { get; set; }
        public string Experiences { get; set; }

        public string ProfilePhoto { get; set; }

        public string LinkedIn { get; set; }

        public List<ShowCourseInInstructorProfileViewModel> Courses { get; set; }
    }
}
