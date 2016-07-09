namespace CRM.Services.Logic.Services.Marketing
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper.QueryableExtensions;

    using CRM.Data;
    using Contracts.Marketing;
    using Data.ViewModels.Marketing.Reports;

    public class MarketingReportsServices : IMarketingReportsServices
    {
        private ICRMData Data { get; }

        public MarketingReportsServices(ICRMData data)
        {
            this.Data = data;
        }

        public List<EpgReportModel> ByEpg(DateTime from, DateTime to)
        {
            var reports = this.Data.Clients
                .All()
                .Where(c => c.WantToReceiveEpg && c.CreatedOn < to && c.CreatedOn > from)
                .ProjectTo<EpgReportModel>()
                .ToList();

            return reports;
        }

        public List<NewsletterReportModel> ByNewsLetter(DateTime from, DateTime to)
        {
            var reports = this.Data.Clients
                .All()
                .Where(c => c.WantToReceiveNews && c.CreatedOn < to && c.CreatedOn > from)
                .ProjectTo<NewsletterReportModel>()
                .ToList();

            return reports;
        }
    }
}