namespace CRM.Services.Logic.Contracts.Marketing
{
    using System.Collections.Generic;

    using Base;
    using Data.ViewModels.Marketing.Partners;

    public interface IPrsServices : IService
    {
        List<string> Index();

        PrViewModel PrInformation(int prId);

        PrViewModel PrDetails(int prId);

        List<PrViewModel> ReadPrs(string searchboxPr, bool showAll);

        PrViewModel CreatePr(PrViewModel givenPr);

        PrViewModel UpdatePr(PrViewModel givenPr);

        PrViewModel DestroyPr(PrViewModel givenPr);

        string GetPrById(int prId);
    }
}