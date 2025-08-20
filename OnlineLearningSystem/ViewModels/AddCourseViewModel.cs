using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.ViewModels
{
    public class AddCourseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int InstructorId { get; set; }
        public int CategoryId { get; set; }
    }
}
