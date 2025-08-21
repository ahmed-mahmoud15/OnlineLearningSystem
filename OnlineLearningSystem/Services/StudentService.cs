using OnlineLearningSystem.Common_Functionalities;
using OnlineLearningSystem.Models;
using OnlineLearningSystem.Repositories;
using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork unitOfWork;

        public StudentService(IUnitOfWork unit)
        {
            this.unitOfWork = unit;
        }

        public async Task EditStudentAsync(EditStudentViewModel model)
        {
            if(model == null) throw new ArgumentNullException("model is Null" );

            Student student = await CheckEntity.CheckAndGetStudentAsync(model.Id, unitOfWork);

            if (model.NewProfilePhoto != null)
            {
                FileHandler.HandleProfileImageUpload(student, model.NewProfilePhoto);
            }
            else
            {
                student.ProfilePhotoPath = model.OldProfilePhotoPath;
            }

            student.BirthDate = model.BirthDate;
            student.FirstName = model.FirstName;
            student.LastName = model.LastName;
            student.GitHubAccount = model.GithubAccount;
            student.Bio = model.Bio;

            unitOfWork.Students.Update(student);
            await unitOfWork.CompleteAsync();
        }

        public async Task<StudentProfileViewModel> GetStudentProfileAsync(int id)
        {
            Student student = await CheckEntity.CheckAndGetStudentAsync(id, unitOfWork);

            student = await unitOfWork.Students.GetWithEnrollmentsAsync(id);

            List<ShowCourseInStudentProfileViewModel> myCourses = student.Enrollments.Select(e => new ShowCourseInStudentProfileViewModel()
            {
                EnrollDate = e.Date,
                Description = e.Course.Description,
                Id = e.Course.Id,
                Name = e.Course.Name,
                Prgress = e.Progress
            }).ToList();

            StudentProfileViewModel model = new StudentProfileViewModel()
            {
                Age = DateTime.Now.Year - student.BirthDate.Year,
                Balance = student.Coins,
                Bio = student.Bio,
                Github = student.GitHubAccount,
                Id = id,
                Name = student.FirstName + " " + student.LastName,
                ProfilePhoto = student.ProfilePhotoPath,
                Courses = myCourses
            };
            return model;
        }

        public async Task<EditStudentViewModel> GetStudentEditAsync(int studentId)
        {
            Student student = await CheckEntity.CheckAndGetStudentAsync(studentId, unitOfWork);

            EditStudentViewModel model = new EditStudentViewModel()
            {
                Bio = student.Bio,
                BirthDate = student.BirthDate,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Id = studentId,
                GithubAccount = student.GitHubAccount,
                OldProfilePhotoPath = student.ProfilePhotoPath,
                NewProfilePhoto = null
            };

            return model;            
        }
    }
}
