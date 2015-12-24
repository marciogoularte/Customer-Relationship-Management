using AutoMapper.QueryableExtensions;

namespace CRM.Services.Logic.Services.Finance
{
    using System.Linq;
    using System.Collections.Generic;

    using Contracts.Finance;
    using CRM.Data;
    using CRM.Data.Models.Finance;
    using Data.ViewModels.Finance.Payments;

    public class PaymentsServices : IPaymentsServices
    {
        private ICRMData Data { get; }

        public PaymentsServices(ICRMData data)
        {
            this.Data = data;
        }

        public List<string> Index()
        {
            var payments = this.Data.Payments
                .All()
                .Select(p => p.Payer)
                .ToList();

            return payments;
        }

        public PaymentViewModel PaymentInformation(int paymentId)
        {
            var paymentInfo = this.Data.Payments
                .All()
                .ProjectTo<PaymentViewModel>()
                .FirstOrDefault(p => p.Id == paymentId);

            return paymentInfo;
        }

        public PaymentViewModel PaymentDetails(int paymentId)
        {
            var paymentDetails = this.Data.Payments
                .All()
                .ProjectTo<PaymentViewModel>()
                .FirstOrDefault(t => t.Id == paymentId);

            return paymentDetails;
        }

        public List<PaymentViewModel> ReadPayments(string searchboxPayment)
        {
            List<PaymentViewModel> readPayments;

            if (string.IsNullOrEmpty(searchboxPayment) || searchboxPayment == "")
            {
                readPayments = this.Data.Payments
                .All()
                .ProjectTo<PaymentViewModel>()
                .ToList();
            }
            else
            {
                readPayments = this.Data.Payments
                .All()
                .ProjectTo<PaymentViewModel>()
                .Where(p => p.Payer.Contains(searchboxPayment))
                .ToList();
            }

            return readPayments;
        }

        public PaymentViewModel CreatePayment(PaymentViewModel givenPayment)
        {
            if (givenPayment == null)
            {
                return null;
            }

            var newPayment = new Payment
            {
                Date = givenPayment.Date,
                Expense = givenPayment.Expense,
                Payer = givenPayment.Payer,
                Invoice = givenPayment.Invoice,
                Amount = givenPayment.Amount
            };

            this.Data.Payments.Add(newPayment);
            this.Data.SaveChanges();

            givenPayment.Id = newPayment.Id;

            return givenPayment;
        }

        public PaymentViewModel UpdatePayment(PaymentViewModel givenPayment)
        {
            var paymentFromDb = this.Data.Payments
                .All()
              .FirstOrDefault(p => p.Id == givenPayment.Id);

            if (givenPayment == null || paymentFromDb == null)
            {
                return givenPayment;
            }

            paymentFromDb.Date = givenPayment.Date;
            paymentFromDb.Expense = givenPayment.Expense;
            paymentFromDb.Payer = givenPayment.Payer;
            paymentFromDb.Invoice = givenPayment.Invoice;
            paymentFromDb.Amount = givenPayment.Amount;

            this.Data.SaveChanges();

            return givenPayment;
        }

        public PaymentViewModel DestroyPayment(PaymentViewModel givenPayment)
        {
            this.Data.Payments.Delete(givenPayment.Id);
            this.Data.SaveChanges();

            return givenPayment;
        }

        public string GetPaymentById(int paymentId)
        {
            var paymentById = this.Data.Payments
                .All()
                .Where(t => t.Id == paymentId)
                .Select(t => t.Payer.ToString())
                .FirstOrDefault();

            return paymentById;
        }
    }
}
