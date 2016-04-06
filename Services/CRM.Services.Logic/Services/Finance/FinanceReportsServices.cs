using CRM.Data.Models;

namespace CRM.Services.Logic.Services.Finance
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper.QueryableExtensions;

    using CRM.Data;
    using Contracts.Finance;
    using Data.ViewModels.Finance.Reports;

    public class FinanceReportsServices : IFinanceReportsServices
    {
        private ICRMData Data { get; }

        public FinanceReportsServices(ICRMData data)
        {
            this.Data = data;
        }

        public List<ClientReportModel> ByClient(DateTime from, DateTime to)
        {
            // TODO: Check specification
            // TODO: from/to

            var clients = this.Data.Clients
                .All()
                .Where(c => c.Contracts.Count != 0)
                .ProjectTo<ClientReportModel>()
                .ToList();

            return clients;
        }

        public List<DealerReportModel> ByDealer(DateTime from, DateTime to)
        {
            var identityUserRoles = this.Data.IdentityRoles
                .All()
                .FirstOrDefault(r => r.Name == "Dealer")
                .Users;

            var dealers = new List<DealerReportModel>();

            foreach (var identityUserRole in identityUserRoles)
            {
                var user = this.Data.Users
                    .All()
                    .FirstOrDefault(u => u.Id == identityUserRole.UserId);

                var dealerReportModel = new DealerReportModel();
                dealerReportModel.UserName = user.UserName;
                dealerReportModel.TotalContracts = GetTotalContracts(user.Clients.ToList(), null, null).Count;
                dealerReportModel.TotalContractsForPeriod = GetTotalContracts(user.Clients.ToList(), from, to).Count;
                dealerReportModel.TotalMonthlyFee = GetTotalMonthlyFee(user.Clients.ToList(), null, null);
                dealerReportModel.TotalMonthlyFeeForPeriod = GetTotalMonthlyFee(user.Clients.ToList(), from, to);

                dealers.Add(dealerReportModel);
            }

            return dealers;
        }

        private List<ClientContract> GetTotalContracts(List<Client> clients, DateTime? from, DateTime? to)
        {
            var clientContracts = new List<ClientContract>();

            foreach (var client in clients)
            {
                foreach (var clientContract in client.Contracts)
                {
                    if (from != null && to != null)
                    {
                        if (clientContract.CreatedOn < to && clientContract.CreatedOn > from)
                        {
                            clientContracts.Add(clientContract);
                        }
                    }
                    else
                    {
                        clientContracts.Add(clientContract);
                    }
                }
            }

            return clientContracts;
        }

        private double GetTotalMonthlyFee(List<Client> clients, DateTime? from, DateTime? to)
        {
            var contracts = this.GetTotalContracts(clients, from, to);
            var totalMonthlyFee = contracts.Sum(contract => contract.MonthlyFee);

            return totalMonthlyFee;
        }

        //public List<InvoiceReportModel> ByInvoices(DateTime @from, DateTime to)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<TvChannelReportModel> ByTvChannels(DateTime @from, DateTime to)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<DateReportModel> ByDate(DateTime @from, DateTime to)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
