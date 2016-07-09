namespace CRM.Services.Logic.Contracts.Marketing
{
    using System;
    using System.Collections.Generic;

    using Base;
    using Data.ViewModels.Marketing.Reports;

    public interface IMarketingReportsServices : IService
    {
        List<EpgReportModel> ByEpg(DateTime from, DateTime to);

        List<NewsletterReportModel> ByNewsLetter(DateTime from, DateTime to);
    }
}
