namespace CRM.Services.Logic.Contracts.Finance
{
    using System.Collections.Generic;

    using Base;
    using Data.ViewModels.Finance.Payments;

    public interface IPaymentsServices : IService
    {
        List<string> Index();

        PaymentViewModel PaymentInformation(int paymentId);

        PaymentViewModel PaymentDetails(int paymentId);

        List<PaymentViewModel> ReadPayments(string searchboxPayment, bool showAll);

        PaymentViewModel CreatePayment(PaymentViewModel givenPayment);

        PaymentViewModel UpdatePayment(PaymentViewModel givenPayment);

        PaymentViewModel DestroyPayment(PaymentViewModel givenPayment);

        string GetPaymentById(int paymentId);
    }
}
