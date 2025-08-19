using System.ComponentModel.DataAnnotations;

namespace OnlineLearningSystem.ViewModels
{
    public class EditStudentViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Bio")]
        public string? Bio { get; set; }

        [Display(Name = "GitHub")]
        public string? GithubAccount { get; set; }

        [Display(Name = "Profile Photo Path")]
        public IFormFile ProfilePhoto { get; set; }
    }
}
