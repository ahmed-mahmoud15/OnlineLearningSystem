using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using OnlineLearningSystem.Services;
using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Controllers
{
    
    public class LessonController : Controller
    {
        private readonly ILessonService lessonService;

        public LessonController(ILessonService lessonService)
        {
            this.lessonService = lessonService;
        }

        [HttpGet]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> CreateLesson(int courseId)
        {
            CreateLessonViewModel model = new CreateLessonViewModel() {
                CourseId = courseId
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> CreateLesson(CreateLessonViewModel model)
        {
            try
            {
                await lessonService.CreateLesson(model);
            }catch(Exception ex)
            {
                TempData["AlertMessage"] = ex.Message;
                TempData["AlertType"] = "warning";
                return View(model);
            }
            return RedirectToAction("CourseDetails", "Course", new { id = model.CourseId });
        }

        [HttpGet]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> EditLesson(int lessonId)
        {
            return View(await lessonService.GetEditLessonView(lessonId));
        }

        [HttpPost]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> EditLesson(EditLessonViewModel model)
        {
            try
            {
                await lessonService.EditLesson(model);
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = ex.Message;
                TempData["AlertType"] = "warning";
                return View(model);
            }
            return RedirectToAction("CourseDetails", "Course", new { id = model.CourseId });
        }

        [HttpGet]
        [Authorize(Roles = "Student,Instructor")]
        public async Task<IActionResult> ViewLesson(int courseId, int seqNum)
        {
            ViewLessonViewModel model;
            try
            {
                model = await lessonService.ViewLesson(courseId, seqNum);
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = ex.Message;
                TempData["AlertType"] = "warning";
                return RedirectToAction("CourseDetails", "Course", new { id = courseId });
            }
            return View(model);
        }
    }
}
