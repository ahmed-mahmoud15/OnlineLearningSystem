using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Services
{
    public interface ILessonService
    {
        public Task CreateLesson(CreateLessonViewModel model);
        public Task EditLesson(EditLessonViewModel model);
        public Task<EditLessonViewModel> GetEditLessonView(int lessonId);

        public Task<ViewLessonViewModel> ViewLesson(int courseId, int seqNum);
    }
}
