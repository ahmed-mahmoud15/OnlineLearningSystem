using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OnlineLearningSystem.Common_Functionalities;
using OnlineLearningSystem.Models;
using OnlineLearningSystem.Repositories;
using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Services
{
    public class LessonService : ILessonService
    {
        private readonly IUnitOfWork unitOfWork;

        public LessonService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateLesson(CreateLessonViewModel model)
        {
            if(model == null)
            {
                throw new ArgumentNullException("Model is null");
            }

            Course course = await CheckEntity.CheckAndGetCourseAsync(model.CourseId, unitOfWork);
            course = await unitOfWork.Courses.GetWithLessonsAsync(model.CourseId);

            int totalNumOfLessons = course.Lessons.Count;

            Lesson lesson = new Lesson() { 
                CourseId = model.CourseId,
                Title = model.LessonTitle,
                Description = model.LessonDescription,
                SequenceNumber = totalNumOfLessons + 1,
                Type = model.LessonType
            };
            FileHandler.HandleAttachment(lesson, model.LessonFile);

            await unitOfWork.Lessons.AddAsync(lesson);
            await unitOfWork.CompleteAsync();
        }

        public async Task EditLesson(EditLessonViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("Model is null");
            }

            Course course = await CheckEntity.CheckAndGetCourseAsync(model.CourseId, unitOfWork);
            course = await unitOfWork.Courses.GetWithLessonsAsync(model.CourseId);

            Lesson lesson = course.Lessons.FirstOrDefault(e => e.Id == model.LessonId);
            
            lesson.Title = model.LessonTitle;
            lesson.Description = model.LessonDescription;
            lesson.Type = model.LessonType;

            if(model.LessonFile != null)
            {
                FileHandler.HandleAttachment(lesson, model.LessonFile);
            }

            unitOfWork.Lessons.Update(lesson);
            await unitOfWork.CompleteAsync();
        }

        public async Task<EditLessonViewModel> GetEditLessonView(int lessonId)
        {
            Lesson lesson = await unitOfWork.Lessons.GetLessonWithCourse(lessonId);
            if (lesson == null)
            {
                throw new Exception($"No Lesson with Id {lessonId}");
            }

            EditLessonViewModel model = new EditLessonViewModel()
            {
                CourseId = lesson.CourseId,
                LessonId = lessonId,
                LessonTitle = lesson.Title,
                LessonDescription = lesson.Description,
                LessonType = lesson.Type,
                PriviousFile = lesson.FilePath,
                SequenceNumber = lesson.SequenceNumber,
                LessonFile = null
            };
            return model;
        }

        public async Task<ViewLessonViewModel> ViewLesson(int courseId, int seqNum)
        {
            Course course = await CheckEntity.CheckAndGetCourseAsync(courseId, unitOfWork);
            course = await unitOfWork.Courses.GetWithLessonsAsync(courseId);

            Lesson lesson = course.Lessons.Where(e => e.SequenceNumber == seqNum).FirstOrDefault();

            if (lesson == null) {
                throw new InvalidOperationException($"There is no lesson #{seqNum} in {course.Name}");
            }

            ViewLessonViewModel model = new ViewLessonViewModel() {
                CourseId = courseId,
                CourseName = course.Name,
                FilePath = lesson.FilePath,
                SequenceNumber = seqNum,
                LessonDescription = lesson.Description,
                LessonId = lesson.Id,
                LessonName = lesson.Title,
                LessonType = lesson.Type
            };
            return model;
        }
    }
}
