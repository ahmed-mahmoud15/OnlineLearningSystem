using Microsoft.AspNetCore.Identity;
using OnlineLearningSystem.Models;
using OnlineLearningSystem.Repositories;
using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Services
{
    public class InstructorService : IInstructorService
    {

        private readonly IUnitOfWork unitOfWork;

        public InstructorService(IUnitOfWork unit)
        {
            this.unitOfWork = unit;
        }

        public async Task EditInstructorAsync(EditInstructorViewModel model)
        {
            if (model == null) throw new ArgumentNullException("model is Null");

            Instructor instructor = await unitOfWork.Instructors.GetByIdAsync(model.Id);

            if (instructor == null) { throw new ArgumentNullException($"There is no Instructor with Id = {model.Id}"); }
            if (model.ProfilePhoto == null) { throw new ArgumentNullException("Image file is null"); }
            HandleProfileImageUpload(instructor, model.ProfilePhoto);

            instructor.BirthDate = model.BirthDate;
            instructor.FirstName = model.FirstName;
            instructor.LastName = model.LastName;
            instructor.LinkedInProfile = model.LinkedInAccount;
            instructor.Experience = model.Experience;
            instructor.Bio = model.Bio;

            unitOfWork.Instructors.Update(instructor);
            await unitOfWork.CompleteAsync();
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
