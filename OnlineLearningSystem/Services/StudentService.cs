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

        public async Task AddStudentAsync(Student student, IFormFile file)
        {
            if (student == null) throw new ArgumentNullException($"Student Is Null");
            if (file == null) throw new ArgumentNullException($"File Is Null");

            HandleProfileImageUpload(student, file);
            await unitOfWork.Students.AddAsync(student);
            await unitOfWork.CompleteAsync();
        }

        public async Task DislikeCourse(int studentId, int courseId)
        {
            Student student = await unitOfWork.Students.GetByIdAsync(studentId);

            if (student == null) { throw new ArgumentNullException($"There is no Student with Id = {studentId}"); }

            Course course = await unitOfWork.Courses.GetByIdAsync(courseId);

            if (course == null) { throw new ArgumentNullException($"There is no Course with Id = {courseId}"); }

            Like oldLike = await unitOfWork.Likes.GetWithConditionAsync(e => e.StudentId == studentId && e.CourseId == courseId);

            if (oldLike == null)
            {
                throw new InvalidOperationException("This student didn't like this course before");
            }

            await unitOfWork.Likes.DeleteAsync(oldLike);
            await unitOfWork.CompleteAsync();
        }

        public async Task EnrollInCourse(int studentId, int courseId)
        {
            Student student = await unitOfWork.Students.GetByIdAsync(studentId);

            if (student == null) { throw new ArgumentNullException($"There is no Student with Id = {studentId}"); }

            Course course = await unitOfWork.Courses.GetByIdAsync(courseId);

            if (course == null) { throw new ArgumentNullException($"There is no Course with Id = {courseId}"); }

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
            Student student = await unitOfWork.Students.GetByIdAsync(studentId);

            if (student == null) { throw new ArgumentNullException($"There is no Student with Id = {studentId}"); }

            Instructor instructor = await unitOfWork.Instructors.GetByIdAsync(instructorId);

            if (instructor == null) { throw new ArgumentNullException($"There is no Instructor with Id = {instructorId}"); }

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

        public async Task LikeCourse(int studentId, int courseId)
        {
            Student student = await unitOfWork.Students.GetByIdAsync(studentId);

            if (student == null) { throw new ArgumentNullException($"There is no Student with Id = {studentId}"); }

            Course course = await unitOfWork.Courses.GetByIdAsync(courseId);

            if (course == null) { throw new ArgumentNullException($"There is no Course with Id = {courseId}"); }

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

            Student student = await unitOfWork.Students.GetByIdAsync(model.StudentId);

            if (student == null) { throw new ArgumentNullException($"There is no Student with Id = {model.StudentId}"); }

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
            Student student = await unitOfWork.Students.GetByIdAsync(studentId);

            if (student == null) { throw new ArgumentNullException($"There is no Student with Id = {studentId}"); }

            Instructor instructor = await unitOfWork.Instructors.GetByIdAsync(instructorId);

            if (instructor == null) { throw new ArgumentNullException($"There is no Instructor with Id = {instructorId}"); }

            Follow oldFollow = await unitOfWork.Follows.GetWithConditionAsync(e => e.StudentId == studentId && e.InstructorId == instructorId);

            if (oldFollow == null)
            {
                throw new InvalidOperationException("This student is not following this instructor");
            }

            await unitOfWork.Follows.DeleteAsync(oldFollow);
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
