using OnlineLearningSystem.Models;
using OnlineLearningSystem.Repositories;

namespace OnlineLearningSystem.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task AddStudentAsync(Student student, IFormFile file)
        {
            if (student == null) throw new ArgumentNullException($"Student Is Null");

            HandleProfileImageUpload(student, file);
            await studentRepository.AddAsync(student);
            await studentRepository.SaveAsync();
        }

        private void HandleProfileImageUpload(User user, IFormFile profileImageFile)
        {
            if (profileImageFile != null && profileImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{user.Id}_{Path.GetFileName(profileImageFile.FileName)}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    profileImageFile.CopyTo(fileStream);
                }

                user.ProfilePhotoPath = "/images/" + fileName;
            }
        }
    }
}
