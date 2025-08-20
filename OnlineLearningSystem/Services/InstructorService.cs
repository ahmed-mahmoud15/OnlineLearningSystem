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

            Instructor instructor = await CheckAndGetInstructorAsync(model.Id);

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

        private async Task<Student> CheckAndGetStudentAsync(int studentId)
        {
            Student student = await unitOfWork.Students.GetByIdAsync(studentId);

            if (student == null) { throw new ArgumentNullException($"There is no Student with Id = {studentId}"); }

            return student;
        }

        private async Task<Course> CheckAndGetCourseAsync(int courseId)
        {
            Course course = await unitOfWork.Courses.GetByIdAsync(courseId);

            if (course == null) { throw new ArgumentNullException($"There is no Course with Id = {courseId}"); }

            return course;
        }

        private async Task<Instructor> CheckAndGetInstructorAsync(int instructorId)
        {
            Instructor instructor = await unitOfWork.Instructors.GetByIdAsync(instructorId);

            if (instructor == null) { throw new ArgumentNullException($"There is no Instructor with Id = {instructorId}"); }

            return instructor;
        }

        public async Task<InstructorProfileViewModel> GetInstructorProfileAsync(int id)
        {
            Instructor instructor = await CheckAndGetInstructorAsync(id);

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
            Instructor instructor = await CheckAndGetInstructorAsync(instructorId);

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

        public async Task AddCourseAsync(AddCourseViewModel model)
        {
            if(model == null)
            {
                throw new ArgumentNullException("model is null");
            }

            Instructor instructor = await CheckAndGetInstructorAsync(model.InstructorId);

            Course course = new Course() {
                Name = model.Name,
                InstructorId = model.InstructorId,
                CategoryId = model.CategoryId,
                CreationDate = DateTime.UtcNow,
                Description = model.Description,
                Price = model.Price
            };

            await unitOfWork.Courses.AddAsync(course);
            await unitOfWork.CompleteAsync();
        }
    }
}
