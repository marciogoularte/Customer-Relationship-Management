using AutoMapper.QueryableExtensions;

namespace CRM.Services.Logic.Services.Marketing
{
    using System.Linq;
    using System.Collections.Generic;

    using CRM.Data;
    using CRM.Data.Models.Marketing;
    using Data.ViewModels.Marketing.Partners;
    using Contracts.Marketing;

    public class PrsServices : IPrsServices
    {
        private ICRMData Data { get; set; }

        public PrsServices(ICRMData data)
        {
            this.Data = data;
        }

        public List<string> Index()
        {
            var prs = this.Data.Prs
                .All()
                .Select(p => p.Name)
                .ToList();

            return prs;
        }

        public PrViewModel PrInformation(int prId)
        {
            var prInfo = this.Data.Prs
                .All()
                .ProjectTo<PrViewModel>()
                .FirstOrDefault(p => p.Id == prId);

            return prInfo;
        }

        public PrViewModel PrDetails(int prId)
        {
            var prDetails = this.Data.Prs
                .All()
                .ProjectTo<PrViewModel>()
                .FirstOrDefault(t => t.Id == prId);

            return prDetails;
        }

        public List<PrViewModel> ReadPrs(string searchboxPr, bool showAll)
        {
            List<PrViewModel> readPrs;

            if (string.IsNullOrEmpty(searchboxPr) || searchboxPr == "")
            {
                if (showAll)
                {
                    readPrs = this.Data.Prs
                    .All()
                    .ProjectTo<PrViewModel>()
                    .ToList();
                }
                else
                {
                    readPrs = this.Data.Prs
                    .All()
                    .Where(pr => pr.IsVisible)
                    .ProjectTo<PrViewModel>()
                    .ToList();
                }
            }
            else
            {
                readPrs = this.Data.Prs
                .All()
                .ProjectTo<PrViewModel>()
                .Where(p => p.Name.Contains(searchboxPr))
                .ToList();
            }

            return readPrs;
        }

        public PrViewModel CreatePr(PrViewModel givenPr)
        {
            if (givenPr == null)
            {
                return null;
            }

            var newPr = new Pr
            {
                Name = givenPr.Name,
                Address = givenPr.Address,
                PhoneNumber = givenPr.PhoneNumber,
                Email = givenPr.Email,
                Media = givenPr.Media,
                IsVisible = givenPr.IsVisible
            };

            this.Data.Prs.Add(newPr);
            this.Data.SaveChanges();

            givenPr.Id = newPr.Id;

            return givenPr;
        }

        public PrViewModel UpdatePr(PrViewModel givenPr)
        {
            var prFromDb = this.Data.Prs
                .All()
              .FirstOrDefault(p => p.Id == givenPr.Id);

            if (givenPr == null || prFromDb == null)
            {
                return givenPr;
            }

            prFromDb.Name = givenPr.Name;
            prFromDb.Address = givenPr.Address;
            prFromDb.PhoneNumber = givenPr.PhoneNumber;
            prFromDb.Email = givenPr.Email;
            prFromDb.Media = givenPr.Media;
            prFromDb.IsVisible = givenPr.IsVisible;

            this.Data.SaveChanges();

            return givenPr;
        }

        public PrViewModel DestroyPr(PrViewModel givenPr)
        {
            this.Data.Prs.Delete(givenPr.Id);
            this.Data.SaveChanges();

            return givenPr;
        }

        public string GetPrById(int prId)
        {
            var prById = this.Data.Prs
                .All()
                .Where(t => t.Id == prId)
                .Select(t => t.Name.ToString())
                .FirstOrDefault();

            return prById;
        }
    }
}
