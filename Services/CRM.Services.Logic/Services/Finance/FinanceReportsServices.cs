namespace CRM.Services.Logic.Services.Finance
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper.QueryableExtensions;

    using CRM.Data;
    using CRM.Data.Models;
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
                .OrderBy(c => c.Name)
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

                var dealerReportModel = new DealerReportModel
                {
                    UserName = user.UserName,
                    TotalContracts = GetTotalContracts(user.Clients.ToList(), null, null).Count,
                    TotalContractsForPeriod = GetTotalContracts(user.Clients.ToList(), from, to).Count,
                    TotalMonthlyFee = GetTotalMonthlyFee(user.Clients.ToList(), null, null),
                    TotalMonthlyFeeForPeriod = GetTotalMonthlyFee(user.Clients.ToList(), from, to)
                };

                dealers.Add(dealerReportModel);
            }

            var orderedDealers = dealers.OrderBy(d => d.UserName)
                .ThenBy(d => d.TotalContracts)
                .ToList();

            return orderedDealers;
        }

        public List<InvoiceReportModel> ByInvoices(DateTime from, DateTime to)
        {
            var invoices = this.Data.Invoices
                .All()
                .Select(i => new InvoiceReportModel()
                {
                    ClientName = i.ClientContract.Client.Name,
                    DealerName = i.ClientContract.Client.Dealer.UserName,
                    From = i.From,
                    To = i.To,
                    AdditionalInformation= i.AdditionalInformation,
                    TotalValue = i.FixedMonthlyFee,
                    IsPaid = i.IsPaid
                })
                .OrderBy(i => i.IsPaid)
                .ThenBy(i => i.ClientName)
                .ThenBy(i => i.DealerName)
                .ThenBy(i => i.TotalValue)
                .ToList();

            return invoices;
        }

        public List<TvChannelReportModel> ByTvChannels(DateTime from, DateTime to)
        {
            var channels = this.Data.Channels
                .All()
                .ToList();

            var channelsReportModel = channels.Select(channel => new TvChannelReportModel
            {
                ChannelName = channel.Name,
                ProviderName = channel.Provider.Name,
                TotalPaid = GetTotalPaidAndUnpaidForChannels(channel, true),
                TotalUnpaid = GetTotalPaidAndUnpaidForChannels(channel, false),
                TotalRevenuesForSelectedPeriod = GetTotalRevenuesForSelectedPeriodForChannel(channel, from, to)
            })
            .OrderBy(channel => channel.ChannelName)
            .ThenBy(channel => channel.ProviderName)
            .ToList();

            return channelsReportModel;
        }

        private int GetTotalPaidAndUnpaidForChannels(Channel channel, bool isPaid)
        {
            var test = this.Data.ClientContracts.All().ToList();
            var contracts = this.Data.ClientContracts
                .All()
                .Where(c => c.Channels.Select(ch => ch.Id).Contains(channel.Id))
                .ToList();

            var total = contracts
                .Sum(contract => contract
                .Invoices
                .Count(invoice => invoice.IsPaid == isPaid));

            return total;
        }

        private double GetTotalRevenuesForSelectedPeriodForChannel(Channel channel, DateTime from, DateTime to)
        {
            var contracts = this.Data.ClientContracts
                .All()
                .Where(c => c.Channels.Select(ch => ch.Id).Contains(channel.Id))
                .ToList();
            var invoices = new List<Invoice>();

            foreach (var contract in contracts)
            {
                invoices.AddRange(contract.Invoices
                    .Where(invoice => invoice.From > @from && invoice.To < to));
            }

            var total = invoices.Sum(invoice => invoice.FixedMonthlyFee);

            return total;
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
    }
}
