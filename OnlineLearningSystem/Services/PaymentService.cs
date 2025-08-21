using OnlineLearningSystem.Common_Functionalities;
using OnlineLearningSystem.Models;
using OnlineLearningSystem.Repositories;
using OnlineLearningSystem.ViewModels;

namespace OnlineLearningSystem.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork unitOfWork;

        public PaymentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task MakePayment(MakePaymentViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("Model is null");
            }

            Student student = await CheckEntity.CheckAndGetStudentAsync(model.StudentId, unitOfWork);

            if (model.Amount <= 0)
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
    }
}
