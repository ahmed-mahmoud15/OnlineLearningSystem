using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using OnlineLearningSystem.Common_Functionalities;
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

            Instructor instructor = await CheckEntity.CheckAndGetInstructorAsync(model.Id, unitOfWork);

            if (model.NewProfilePhoto != null)
            {
                FileHandler.HandleProfileImageUpload(instructor, model.NewProfilePhoto);
            }
            else
            {
                instructor.ProfilePhotoPath = model.OldProfilePhotoPath;
            }

            instructor.BirthDate = model.BirthDate;
            instructor.FirstName = model.FirstName;
            instructor.LastName = model.LastName;
            instructor.LinkedInProfile = model.LinkedInAccount;
            instructor.Experience = model.Experience;
            instructor.YearsOfTeaching = model.YearsofExperience;
            instructor.Bio = model.Bio;

            unitOfWork.Instructors.Update(instructor);
            await unitOfWork.CompleteAsync();
        }

        
        public async Task<InstructorProfileViewModel> GetInstructorProfileAsync(int id)
        {
            Instructor instructor = await CheckEntity.CheckAndGetInstructorAsync(id, unitOfWork);

            instructor = await unitOfWork.Instructors.GetWithCoursesAsync(id);

            List<ShowCourseInInstructorProfileViewModel> myCourses = instructor.Courses.Select(e => new ShowCourseInInstructorProfileViewModel()
            {
                Id = e.Id,
                Name = e.Name,
                CountLessons = e.Lessons.Count,
                CountEnrolledStudents = e.Enrollments.Count,
                CountLikes = e.LikedBy.Count,
            }).ToList();

            InstructorProfileViewModel model = new InstructorProfileViewModel()
            {
                Age = DateTime.Now.Year - instructor.BirthDate.Year,
                Bio = instructor.Bio,
                LinkedIn = instructor.LinkedInProfile,
                Experiences = instructor.Experience,
                YearsOfExperience = instructor.YearsOfTeaching,
                Id = id,
                Name = instructor.FirstName + " " + instructor.LastName,
                ProfilePhoto = instructor.ProfilePhotoPath,
                Courses = myCourses
            };
            return model;
        }

        public async Task<EditInstructorViewModel> GetInstructorEditAsync(int instructorId)
        {
            Instructor instructor = await CheckEntity.CheckAndGetInstructorAsync(instructorId, unitOfWork);

            EditInstructorViewModel model = new EditInstructorViewModel()
            {
                Bio = instructor.Bio,
                BirthDate = instructor.BirthDate,
                FirstName = instructor.FirstName,
                LastName = instructor.LastName,
                Id = instructorId,
                OldProfilePhotoPath = instructor.ProfilePhotoPath,
                Experience = instructor.Experience,
                YearsofExperience = instructor.YearsOfTeaching,
                LinkedInAccount = instructor.LinkedInProfile,
                NewProfilePhoto = null
            };

            return model;
        }

        public async Task<IEnumerable<ShowInstructorInfoViewModel>> GetInstructorsInfoAsync()
        {
            IList<ShowInstructorInfoViewModel> model = new List<ShowInstructorInfoViewModel>();

            foreach (var instructor in await unitOfWork.Instructors.GetAllWithCoursesFollowersAsync())
            {
                model.Add(new ShowInstructorInfoViewModel()
                {
                    Id = instructor.Id,
                    Name = instructor.FirstName + " " + instructor.LastName,
                    ProfilePhoto = instructor.ProfilePhotoPath,
                    YearsOfExperience = instructor.YearsOfTeaching,
                    NumberOfCourses = instructor.Courses.Count,
                    NumberOfFollowers = instructor.FollowedBy.Count
                });
            }
            
            return model;
        }

        public async Task<IEnumerable<Instructor>> GetAllInstructorsWithIdentityCourses()
        {
            return await unitOfWork.Instructors.GetAllWithIdentityCoursesAsync();
        }

        public async Task<int> CountInstructors()
        {
            return await unitOfWork.Instructors.GetTotalNumberOfInstructorsAsync();
        }
    }
}
