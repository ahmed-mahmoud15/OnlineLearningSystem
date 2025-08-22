using System.ComponentModel.DataAnnotations;

namespace OnlineLearningSystem.ViewModels
{
    public class EditLessonViewModel
    {
        public int LessonId { get; set; }
        public int CourseId { get; set; }

        [Display(Name = "Title")]
        [Required]
        public string LessonTitle { get; set; }

        [Display(Name = "Description")]
        public string? LessonDescription { get; set; }

        public int SequenceNumber { get; set; }

        [Display(Name = "Type")]
        [Required]
        public Models.Type LessonType { get; set; }

        [Display(Name = "Upload File")]
        [Required]
        public IFormFile? LessonFile { get; set; }

        public string PriviousFile { get; set; }
    }
}
