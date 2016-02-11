using AutoMapper.QueryableExtensions;

namespace CRM.Services.Logic.Services.Marketing
{
    using System.Linq;
    using System.Collections.Generic;

    using CRM.Data;
    using CRM.Data.Models.Marketing;
    using Contracts.Marketing;
    using Data.ViewModels.Marketing.Partners;

    public class MarketingPartnersServices : IMarketingPartnersServices
    {
        private ICRMData Data { get; set; }

        public MarketingPartnersServices(ICRMData data)
        {
            this.Data = data;
        }

        public List<string> Index()
        {
            var partners = this.Data.MarketingPartners
                .All()
                .Select(p => p.Name)
                .ToList();

            return partners;
        }

        public MarketingPartnerViewModel MarketingPartnerInformation(int marketingPartnerId)
        {
            var marketingPartner = this.Data.MarketingPartners
                .All()
                .ProjectTo<MarketingPartnerViewModel>()
                .FirstOrDefault(p => p.Id == marketingPartnerId);

            return marketingPartner;
        }

        public MarketingPartnerViewModel MarketingPartnerDetails(int marketingPartnerId)
        {
            var marketingPartner = this.Data.MarketingPartners
                .All()
                .ProjectTo<MarketingPartnerViewModel>()
                .FirstOrDefault(t => t.Id == marketingPartnerId);

            return marketingPartner;
        }

        public List<MarketingPartnerViewModel> ReadMarketingPartners(string searchboxMarketingPartner, bool showAll)
        {
            List<MarketingPartnerViewModel> marketingPartners;

            if (string.IsNullOrEmpty(searchboxMarketingPartner) || searchboxMarketingPartner == "")
            {
                if (showAll)
                {
                    marketingPartners = this.Data.MarketingPartners
                    .All()
                    .ProjectTo<MarketingPartnerViewModel>()
                    .ToList();
                }
                else
                {
                    marketingPartners = this.Data.MarketingPartners
                    .All()
                    .Where(mp => mp.IsVisible)
                    .ProjectTo<MarketingPartnerViewModel>()
                    .ToList();
                }
            }
            else
            {
                marketingPartners = this.Data.MarketingPartners
                .All()
                .ProjectTo<MarketingPartnerViewModel>()
                .Where(p => p.Name.Contains(searchboxMarketingPartner))
                .ToList();
            }

            return marketingPartners;
        }

        public MarketingPartnerViewModel CreateMarketingPartner(MarketingPartnerViewModel marketingPartner)
        {
            if (marketingPartner == null)
            {
                return null;
            }

            var newMarketingPartner = new MarketingPartner
            {
                Id = marketingPartner.Id,
                Name = marketingPartner.Name,
                Address = marketingPartner.Address,
                PhoneNumber = marketingPartner.PhoneNumber,
                Email = marketingPartner.Email,
                Media = marketingPartner.Media,
                IsVisible = marketingPartner.IsVisible
            };

            this.Data.MarketingPartners.Add(newMarketingPartner);
            this.Data.SaveChanges();

            marketingPartner.Id = newMarketingPartner.Id;

            return marketingPartner;
        }

        public MarketingPartnerViewModel UpdateMarketingPartner(MarketingPartnerViewModel marketingPartner)
        {
            var marketingPartnerFromDb = this.Data.MarketingPartners.All()
                   .FirstOrDefault(p => p.Id == marketingPartner.Id);

            if (marketingPartner == null || marketingPartnerFromDb == null)
            {
                return marketingPartner;
            }

            marketingPartnerFromDb.Id = marketingPartner.Id;
            marketingPartnerFromDb.Name = marketingPartner.Name;
            marketingPartnerFromDb.Address = marketingPartner.Address;
            marketingPartnerFromDb.PhoneNumber = marketingPartner.PhoneNumber;
            marketingPartnerFromDb.Email = marketingPartner.Email;
            marketingPartnerFromDb.Media = marketingPartner.Media;
            marketingPartnerFromDb.IsVisible = marketingPartner.IsVisible;

            this.Data.SaveChanges();

            return marketingPartner;
        }

        public MarketingPartnerViewModel DestroyMarketingPartner(MarketingPartnerViewModel marketingPartner)
        {
            this.Data.MarketingPartners.Delete(marketingPartner.Id);
            this.Data.SaveChanges();

            return marketingPartner;
        }

        public string GetMarketingPartnerById(int marketingPartnerId)
        {
            var marketingPartner = this.Data.MarketingPartners
                .All()
                .Where(t => t.Id == marketingPartnerId)
                .Select(t => t.Name.ToString())
                .FirstOrDefault();

            return marketingPartner;
        }
    }
}
