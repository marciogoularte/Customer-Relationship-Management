using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using CRM.Data;
using CRM.Data.Models;
using CRM.Services.Data.ViewModels.Contracts.Trds;
using CRM.Services.Logic.Contracts.Contractors;

namespace CRM.Services.Logic.Services.Contractors
{
    public class TrdsServices : ITrdsServices
    {
        private ICRMData Data { get; set; }

        public TrdsServices(ICRMData data)
        {
            this.Data = data;
        }

        public List<string> GetTrdsData()
        {
            var trdsData = this.Data.Trds
                .All()
                .Select(c => c.Decoder.ToString())
                .ToList();

            return trdsData;
        }

        public List<TrdViewModel> ReadTrds(string searchbox, int clientId)
        {
            List<TrdViewModel> trds;

            if (string.IsNullOrEmpty(searchbox) || searchbox == "")
            {
                trds = this.Data.Trds
                    .All()
                    .ProjectTo<TrdViewModel>()
                    .Where(i => i.ClientId == clientId)
                    .ToList();
            }
            else
            {
                trds = this.Data.Trds
                   .All()
                    .ProjectTo<TrdViewModel>()
                   .Where(i => i.ClientId == clientId && i.Decoder.Contains(searchbox))
                   .ToList();
            }

            return trds;
        }

        public TrdViewModel CreateTrd(TrdViewModel trd, string clientIdString)
        {
            var clientId = int.Parse(clientIdString);
            if (trd == null)
            {
                return trd;
            }

            var newTrd = new Trd
            {
                Decoder = trd.Decoder,
                Sim = trd.Sim,
                Cas = trd.Cas,
                Cam = trd.Cam,
                ClientId = clientId
            };

            this.Data.Trds.Add(newTrd);
            this.Data.SaveChanges();

            trd.Id = newTrd.Id;

            return trd;
        }

        public TrdViewModel TrdDetails(int trdId)
        {
            var trd = this.Data.Trds
                .All()
                .ProjectTo<TrdViewModel>()
                .FirstOrDefault(t => t.Id == trdId);

            return trd;
        }

        public TrdViewModel UpdateTrd(TrdViewModel trd)
        {
            var trdFromDb = this.Data.Trds
                .All()
              .FirstOrDefault(t => t.Id == trd.Id);

            trdFromDb.Decoder = trd.Decoder;
            trdFromDb.Sim = trd.Sim;
            trdFromDb.Cas = trd.Cas;
            trdFromDb.Cam = trd.Cam;

            this.Data.SaveChanges();

            return trd;
        }

        public TrdViewModel DestroyTrd(TrdViewModel trd)
        {
            this.Data.Trds.Delete(trd.Id);
            this.Data.SaveChanges();

            return trd;
        }
    }
}
