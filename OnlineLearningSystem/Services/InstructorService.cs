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
    }
}
