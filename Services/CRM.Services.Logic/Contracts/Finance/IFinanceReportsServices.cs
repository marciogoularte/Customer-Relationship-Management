namespace CRM.Services.Logic.Contracts.Finance
{
    using System;
    using System.Collections.Generic;

    using Base;
    using Data.ViewModels.Finance.Reports;

    public interface IFinanceReportsServices : IService
    {
        List<ClientReportModel> ByClient(DateTime from, DateTime to);

        List<DealerReportModel> ByDealer(DateTime from, DateTime to);

        //List<InvoiceReportModel> ByInvoices(DateTime from, DateTime to);

        //List<TvChannelReportModel> ByTvChannels(DateTime from, DateTime to);

        //List<DateReportModel> ByDate(DateTime from, DateTime to);
    }
}
