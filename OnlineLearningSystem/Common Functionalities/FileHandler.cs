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
    }
}
