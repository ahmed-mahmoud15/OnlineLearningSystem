using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningSystem.Services;
using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;
        private readonly IPaymentService paymentService;
        private readonly IAccountService accountService;

        public AdminController(IAdminService adminService, IPaymentService paymentService, IAccountService accountService)
        {
            this.adminService = adminService;
            this.paymentService = paymentService;
            this.accountService = accountService;
        }

        public async Task<IActionResult> Dashboard()
        {
            return View(await adminService.AdminDashboard());
        }

        public async Task<IActionResult> ManageStudents()
        {
            return View(await adminService.ManageStudents());
        }

        public async Task<IActionResult> ManageInstructors()
        {
            return View(await adminService.ManageInstructors());
        }

        public async Task<IActionResult> ManageCourses()
        {
            return View(await adminService.ManageCourses());
        }

        public async Task<IActionResult> ManageCategories()
        {
            return View(await adminService.ManageCategories());
        }

        public async Task<IActionResult> BanUser(int id, string returnUrl)
        {
            TempData["AlertMessage"] = "Not Implemented Yet";
            TempData["AlertType"] = "danger";
            return LocalRedirect(returnUrl);
        }

        public async Task<IActionResult> MakePayment(MakePaymentViewModel model, string returnUrl)
        {
            try
            {
                await paymentService.MakePayment(model);
                TempData["AlertMessage"] = "Payment made successfully";
                TempData["AlertType"] = "success";
            }
            catch (ArgumentNullException ex)
            {
                TempData["AlertMessage"] = ex.Message;
                TempData["AlertType"] = "warning";
            }
            catch (ArgumentException ex)
            {
                TempData["AlertMessage"] = ex.Message;
                TempData["AlertType"] = "warning";
            }
            return LocalRedirect(returnUrl);
        }

        public async Task<IActionResult> CreateInstructor([Bind(Prefix = "CreateInstructor")] CreateInstructorViewModel model, string returnUrl)
        {
            try
            {
                await accountService.RegisterInstructor(model);
                TempData["AlertMessage"] = "Instructor Created successfully";
                TempData["AlertType"] = "success";
            }
            catch (ArgumentNullException ex)
            {
                TempData["AlertMessage"] = ex.Message;
                TempData["AlertType"] = "warning";
            }catch(InvalidOperationException ex)
            {
                TempData["AlertMessage"] = ex.Message;
                TempData["AlertType"] = "danger";
            }

            return LocalRedirect(returnUrl);
        }
    }
}
