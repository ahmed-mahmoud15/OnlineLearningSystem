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

        public async Task DislikeCourse(int studentId, int courseId)
        {
            Student student = await CheckEntity.CheckAndGetStudentAsync(studentId, unitOfWork);
            Course course = await CheckEntity.CheckAndGetCourseAsync(courseId, unitOfWork);

            Like oldLike = await unitOfWork.Likes.GetWithConditionAsync(e => e.StudentId == studentId && e.CourseId == courseId);

            if (oldLike == null)
            {
                throw new InvalidOperationException("This student didn't like this course before");
            }

            await unitOfWork.Likes.DeleteAsync(oldLike);
            await unitOfWork.CompleteAsync();
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

        public async Task FollowInstructor(int studentId, int instructorId)
        {
            Student student = await CheckEntity.CheckAndGetStudentAsync(studentId, unitOfWork);
            Instructor instructor = await CheckEntity.CheckAndGetInstructorAsync(instructorId, unitOfWork);

            Follow oldFollow = await unitOfWork.Follows.GetWithConditionAsync(e => e.StudentId == studentId && e.InstructorId == instructorId);

            if(oldFollow != null)
            {
                throw new InvalidOperationException("This student is already following this instructor");
            }

            Follow follow = new Follow() { 
                FollowDate = DateTime.UtcNow,
                InstructorId = instructorId,
                StudentId = studentId
            };

            await unitOfWork.Follows.AddAsync(follow);
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

        public async Task LikeCourse(int studentId, int courseId)
        {
            Student student = await CheckEntity.CheckAndGetStudentAsync(studentId, unitOfWork);
            Course course = await CheckEntity.CheckAndGetCourseAsync(courseId, unitOfWork);

            Like oldLike = await unitOfWork.Likes.GetWithConditionAsync(e => e.StudentId == studentId && e.CourseId == courseId);

            if (oldLike != null) {
                throw new InvalidOperationException("This student liked this course before");
            }

            Like like = new Like() {
                CourseId = courseId,
                StudentId = studentId,
                LikedDate = DateTime.UtcNow
            };

            await unitOfWork.Likes.AddAsync(like);
            await unitOfWork.CompleteAsync();
        }

        public async Task MakePayment(MakePaymentViewModel model)
        {
            if (model == null) {
                throw new ArgumentNullException("Model is null");
            }

            Student student = await CheckEntity.CheckAndGetStudentAsync(model.StudentId, unitOfWork);

            if(model.Amount <= 0)
            {
                throw new ArgumentException("Amount can't be zero or less");
            }

            Payment payment = new Payment()
            {
                Amount = model.Amount,
                StudentId = model.StudentId
            };

            student.Coins += model.Amount;

            unitOfWork.Students.Update(student);
            await unitOfWork.Payments.AddAsync(payment);
            await unitOfWork.CompleteAsync();
        }

        public async Task UnfollowInstructor(int studentId, int instructorId)
        {
            Student student = await CheckEntity.CheckAndGetStudentAsync(studentId, unitOfWork);
            Instructor instructor = await CheckEntity.CheckAndGetInstructorAsync(instructorId, unitOfWork);

            Follow oldFollow = await unitOfWork.Follows.GetWithConditionAsync(e => e.StudentId == studentId && e.InstructorId == instructorId);

            if (oldFollow == null)
            {
                throw new InvalidOperationException("This student is not following this instructor");
            }

            await unitOfWork.Follows.DeleteAsync(oldFollow);
            await unitOfWork.CompleteAsync();
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
