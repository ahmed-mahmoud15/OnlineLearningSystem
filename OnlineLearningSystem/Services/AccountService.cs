using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using OnlineLearningSystem.Models;
using OnlineLearningSystem.Repositories;
using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Services
{
    public class AccountService : IAccountService
    {
        private readonly IStudentService studentService;
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<IdentityUser> userManager;

        public AccountService(IStudentService studentService, IUnitOfWork unit, UserManager<IdentityUser> userManager)
        {
            this.studentService = studentService;
            this.unitOfWork = unit;
            this.userManager = userManager;
        }

        public Task<User> GetUserByIdentityIdAsync(string identityId)
        {
            return unitOfWork.Users.GetByIdentityId(identityId);
        }

        public Task RegisterAdmin(User user, IFormFile file)
        {
            throw new NotImplementedException();
        }


        // need to add usermanager to unit of work??
        public async Task RegisterInstructor(CreateInstructorViewModel model)
        {
            if (model == null) throw new ArgumentNullException("model is Null");

            IdentityUser user = new IdentityUser()
            {
                Email = model.Email,
                UserName = model.Email
            };

            IdentityResult result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Can't create this instructor");
            }

            await userManager.AddToRoleAsync(user, "Instructor");

            var userId = await userManager.GetUserIdAsync(user);

            Instructor instructor = new Instructor()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Bio = model.Bio,
                BirthDate = model.BirthDate,
                LinkedInProfile = model.LinkedInAccount,
                Experience = model.Experience,
                IdentityId = userId
            };

            HandleProfileImageUpload(instructor, model.ProfilePhotoPath);

            await unitOfWork.Instructors.AddAsync(instructor);
            await unitOfWork.CompleteAsync();

            await userManager.AddClaimAsync(user, new Claim("UserId", instructor.Id.ToString()));
        }

        public async Task RegisterStudent(Student student, IFormFile file)
        {
            if (student == null) throw new ArgumentNullException($"Student Is Null");
            if (file == null) throw new ArgumentNullException($"File Is Null");

            HandleProfileImageUpload(student, file);
            await unitOfWork.Students.AddAsync(student);
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
