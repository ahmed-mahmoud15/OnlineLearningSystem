using OnlineLearningSystem.Models;

namespace OnlineLearningSystem.Common_Functionalities
{
    public class FileHandler
    {
        public static void HandleProfileImageUpload(User user, IFormFile profileImageFile)
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

        public static void HandleAttachment(Lesson lesson, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    $"wwwroot/uploads/Course_{lesson.CourseId}");

                Directory.CreateDirectory(uploadsFolder);

                var safeName = Path.GetFileName(file.FileName);
                var fileName = $"{lesson.SequenceNumber}_{safeName}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fs);
                }

                // ✅ correct: root-relative URL to static file
                lesson.FilePath = $"/uploads/Course_{lesson.CourseId}/{fileName}";
            }
        }

    }
}
