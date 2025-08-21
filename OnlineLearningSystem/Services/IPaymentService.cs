using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Services
{
    public interface IPaymentService
    {
        public Task MakePayment(MakePaymentViewModel model);
    }
}
