using System.Collections.Specialized;

namespace OnlineLearningSystem.ViewModels
{
    public class ShowStudentAdminInfoViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public string StudentEmail { get; set; }

        public string PhotoPath { get; set; }

        public int Coins { get; set; }

        public int NumberOfCoursesEnrolled { get; set; }
    }
}
