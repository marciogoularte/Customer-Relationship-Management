namespace CRM.Services.Logic.Contracts.Marketing
{
    using System.Collections.Generic;

    using Base;
    using CRM.Data.Models.Marketing;
    using Data.ViewModels.Marketing.Social;
    
    public interface ISocialPartnersServices : IService
    {
        List<string> Index(SocialSystemType type);

        SocialPartnerViewModel SocialPartnerInformation(int socialPartnerId, SocialSystemType type);

        List<SocialPartnerViewModel> ReadSocialPartners(string searchboxSocialPartner, SocialSystemType type);

        SocialPartnerViewModel CreateSocialPartner(SocialPartnerViewModel givenSocialPartner);

        SocialPartnerViewModel UpdateSocialPartner(SocialPartnerViewModel givenSocialPartner);

        SocialPartnerViewModel DestroySocialPartner(SocialPartnerViewModel givenSocialPartner);
    }
}
