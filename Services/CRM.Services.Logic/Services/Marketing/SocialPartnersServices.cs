namespace CRM.Services.Logic.Services.Marketing
{
    using System.Linq;
    using System.Collections.Generic;

    using CRM.Data;
    using Contracts.Marketing;
    using CRM.Data.Models.Marketing;
    using Data.ViewModels.Marketing.Social;

    public class SocialPartnersServices : ISocialPartnersServices
    {
        private ICRMData Data { get; set; }

        public SocialPartnersServices(ICRMData data)
        {
            this.Data = data;
        }

        public List<string> Index(SocialSystemType type)
        {
            var partnersNames = this.Data.SocialPartners
                .All()
                .Where(p => p.SocialSystem == type)
                .Select(p => p.Name)
                .ToList();

            return partnersNames;
        }

        public SocialPartnerViewModel SocialPartnerInformation(int socialPartnerId, SocialSystemType type)
        {
            var partner = this.Data.SocialPartners
                        .All()
                        .Where(p => p.SocialSystem == type)
                        .Select(SocialPartnerViewModel.FromSocialPartner)
                .FirstOrDefault(p => p.Id == socialPartnerId);

            return partner;
        }

        public List<SocialPartnerViewModel> ReadSocialPartners(string searchboxSocialPartner, SocialSystemType type)
        {
            var partners = this.Data.SocialPartners
                .All()
                .Where(p => p.SocialSystem == type)
                .Select(SocialPartnerViewModel.FromSocialPartner)
                .ToList();

            List<SocialPartnerViewModel> readSocialPartners;

            if (string.IsNullOrEmpty(searchboxSocialPartner) || searchboxSocialPartner == "")
            {
                readSocialPartners = partners;
            }
            else
            {
                readSocialPartners = partners
                .Where(p => p.Name.Contains(searchboxSocialPartner))
                    .ToList();
            }

            return readSocialPartners;
        }

        public SocialPartnerViewModel CreateSocialPartner(SocialPartnerViewModel givenSocialPartner)
        {
            if (givenSocialPartner == null)
            {
                return null;
            }

            var newSocialPartner = new SocialPartner()
            {
                Name = givenSocialPartner.Name,
                Website = givenSocialPartner.Website,
                PhoneNumber = givenSocialPartner.PhoneNumber,
                Email = givenSocialPartner.Email,
                SocialSystem = givenSocialPartner.SocialSystem
            };

            this.Data.SocialPartners.Add(newSocialPartner);
            this.Data.SaveChanges();

            givenSocialPartner.Id = newSocialPartner.Id;

            return givenSocialPartner;
        }

        public SocialPartnerViewModel UpdateSocialPartner(SocialPartnerViewModel givenSocialPartner)
        {
            SocialPartner socialPartnerFromDb = this.Data.SocialPartners
                        .All()
              .FirstOrDefault(p =>
              p.Id == givenSocialPartner.Id
              && p.SocialSystem == givenSocialPartner.SocialSystem);

            if (givenSocialPartner == null || socialPartnerFromDb == null)
            {
                return givenSocialPartner;
            }

            socialPartnerFromDb.Name = givenSocialPartner.Name;
            socialPartnerFromDb.Website = givenSocialPartner.Website;
            socialPartnerFromDb.Email = givenSocialPartner.Email;
            socialPartnerFromDb.PhoneNumber = givenSocialPartner.PhoneNumber;

            this.Data.SaveChanges();

            return givenSocialPartner;
        }

        public SocialPartnerViewModel DestroySocialPartner(SocialPartnerViewModel givenSocialPartner)
        {
            this.Data.SocialPartners.Delete(givenSocialPartner.Id);
            this.Data.SaveChanges();

            return givenSocialPartner;
        }
    }
}