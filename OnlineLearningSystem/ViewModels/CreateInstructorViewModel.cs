using System.ComponentModel.DataAnnotations;

namespace OnlineLearningSystem.ViewModels
{
    public class CreateInstructorViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


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
        public IFormFile ProfilePhotoPath { get; set; }
    }
}
