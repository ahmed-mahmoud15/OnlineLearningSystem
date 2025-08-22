using System.ComponentModel.DataAnnotations;

namespace OnlineLearningSystem.ViewModels
{
    public class ShowCourseInStudentProfileViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Course Name")]
        public string Name { get; set; }
        [Display(Name = "Course Description")]
        public string Description { get; set; }
        [Display(Name = "Enrolled At")]
        public DateTime EnrollDate { get; set; }
        [Display(Name = "My Progress")]
        public float Prgress { get; set; }
    }
}
