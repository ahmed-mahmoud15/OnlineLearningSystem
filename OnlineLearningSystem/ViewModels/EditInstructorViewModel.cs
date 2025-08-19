using System.ComponentModel.DataAnnotations;

namespace OnlineLearningSystem.ViewModels
{
    public class EditInstructorViewModel
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

        [Display(Name = "LinkedIn")]
        public string? LinkedInAccount { get; set; }

        [Display(Name = "Experience")]
        public string? Experience { get; set; }

        [Display(Name = "Profile Photo Path")]
        public IFormFile ProfilePhoto { get; set; }
    }
}
