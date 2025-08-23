using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OnlineLearningSystem.Services;

[Authorize]
public class FollowController : Controller
{
    private readonly IFollowService followService;
    private readonly IStudentService studentService;

    public FollowController(IFollowService followService, IStudentService studentService)
    {
        this.followService = followService;
        this.studentService = studentService;
    }

    [HttpPost]
    public async Task<IActionResult> Follow(int instructorId, string returnUrl = null)
    {
        var userId = int.Parse(User.FindFirst("UserId")?.Value);
        try
        {
            await followService.FollowInstructor(userId, instructorId);
            TempData["AlertMessage"] = "You have successfully followed the instructor.";
            TempData["AlertType"] = "success";
        }
        catch (ArgumentNullException ex)
        {
            TempData["AlertMessage"] = ex.Message;
            TempData["AlertType"] = "warning";
        }
        catch (InvalidOperationException ex) {
            TempData["AlertMessage"] = ex.Message;
            TempData["AlertType"] = "danger";
        }
        
        return LocalRedirect(returnUrl);
    }

    [HttpPost]
    public async Task<IActionResult> Unfollow(int instructorId, string returnUrl = null)
    {
        var userId = int.Parse(User.FindFirst("UserId")?.Value);
        try
        {
            await followService.UnfollowInstructor(userId, instructorId);
            TempData["AlertMessage"] = "You have successfully unfollowed the instructor.";
            TempData["AlertType"] = "success";
        }
        catch (ArgumentNullException ex)
        {
            TempData["AlertMessage"] = ex.Message;
            TempData["AlertType"] = "warning";
        }
        catch (InvalidOperationException ex)
        {
            TempData["AlertMessage"] = ex.Message;
            TempData["AlertType"] = "danger";
        }
        return LocalRedirect(returnUrl);
    }

    [HttpGet]
    public async Task<IActionResult> GetFollowings()
    {
        int studentId = int.Parse(User.FindFirst("UserId")?.Value);
        return View(await studentService.GetStudentFollowing(studentId));
    }
}
