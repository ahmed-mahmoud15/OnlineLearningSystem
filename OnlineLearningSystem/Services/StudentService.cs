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
            Student student = await CheckAndGetStudentAsync(studentId);
            Course course = await CheckAndGetCourseAsync(courseId);

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

            Student student = await CheckAndGetStudentAsync(model.Id);

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

        public async Task EnrollInCourse(int studentId, int courseId)
        {
            Student student = await CheckAndGetStudentAsync(studentId);
            Course course = await CheckAndGetCourseAsync(courseId);

            Enrollment checkPrevEnrollment = await unitOfWork.Enrollments.GetWithConditionAsync(e => e.StudentId == studentId && e.CourseId == courseId);

            if (checkPrevEnrollment != null)
            {
                throw new InvalidOperationException("This student already enrolled in this course before");
            }

            if(student.Coins < course.Price)
            {
                throw new InvalidOperationException("Student doesn't have enough Coins to proceed in this enrollment");
            }

            Enrollment enrollment = new Enrollment()
            {
                CourseId = courseId,
                StudentId = studentId,
                Date = DateTime.UtcNow,
                Progress = 0,
                LastViewedLesson = 0
            };

            student.Coins -= course.Price;

            
            unitOfWork.Students.Update(student);
            await unitOfWork.Enrollments.AddAsync(enrollment);

            await unitOfWork.CompleteAsync();
        }

        public async Task FollowInstructor(int studentId, int instructorId)
        {
            Student student = await CheckAndGetStudentAsync(studentId);
            Instructor instructor = await CheckAndGetInstructorAsync(instructorId);

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
            Student student = await CheckAndGetStudentAsync(id);

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
            Student student = await CheckAndGetStudentAsync(studentId);
            Course course = await CheckAndGetCourseAsync(courseId);

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

            Student student = await CheckAndGetStudentAsync(model.StudentId);

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
            Student student = await CheckAndGetStudentAsync(studentId);
            Instructor instructor = await CheckAndGetInstructorAsync(instructorId);

            Follow oldFollow = await unitOfWork.Follows.GetWithConditionAsync(e => e.StudentId == studentId && e.InstructorId == instructorId);

            if (oldFollow == null)
            {
                throw new InvalidOperationException("This student is not following this instructor");
            }

            await unitOfWork.Follows.DeleteAsync(oldFollow);
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

        public async Task<EditStudentViewModel> GetStudentEditAsync(int studentId)
        {
            Student student = await CheckAndGetStudentAsync(studentId);

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
