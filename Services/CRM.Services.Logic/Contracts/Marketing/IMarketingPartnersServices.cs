namespace CRM.Services.Logic.Contracts.Marketing
{
    using System.Collections.Generic;

    using Base;
    using Data.ViewModels.Marketing.Partners;

    public interface IMarketingPartnersServices : IService
    {
        List<string> Index();

        MarketingPartnerViewModel MarketingPartnerInformation(int marketingPartnerId);

        MarketingPartnerViewModel MarketingPartnerDetails(int marketingPartnerId);

        List<MarketingPartnerViewModel> ReadMarketingPartners(string searchboxMarketingPartner);

        MarketingPartnerViewModel CreateMarketingPartner(MarketingPartnerViewModel marketingPartner);

        MarketingPartnerViewModel UpdateMarketingPartner(MarketingPartnerViewModel marketingPartner);

        MarketingPartnerViewModel DestroyMarketingPartner(MarketingPartnerViewModel marketingPartner);

        string GetMarketingPartnerById(int marketingPartnerId);
    }
}