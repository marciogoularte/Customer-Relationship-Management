namespace CRM.Services.Logic.Services.Finance
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using CRM.Data;
    using Contracts.Finance;
    using Data.ViewModels.Finance.Reports;

    public class ReportsServices : IReportsServices
    {
        private ICRMData Data { get; }

        public ReportsServices(ICRMData data)
        {
            this.Data = data;
        }

        public List<SearchedItemDropDown> GetClients()
        {
            var clients = this.Data.Clients
                .All()
                .Select(c => new SearchedItemDropDown()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

            return clients;
        }

        public List<SearchedItemDropDown> GetProviders()
        {
            var providers = this.Data.Providers
                .All()
                .Select(p => new SearchedItemDropDown()
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToList();

            return providers;
        }

        public List<SearchedItemDropDown> GetMonths()
        {
            var months = new List<SearchedItemDropDown>()
            {
                new SearchedItemDropDown(0, "January"),
                new SearchedItemDropDown(1, "February"),
                new SearchedItemDropDown(2, "March"),
                new SearchedItemDropDown(3, "April"),
                new SearchedItemDropDown(4, "May"),
                new SearchedItemDropDown(5, "June"),
                new SearchedItemDropDown(6, "July"),
                new SearchedItemDropDown(7, "August"),
                new SearchedItemDropDown(8, "September"),
                new SearchedItemDropDown(9, "October"),
                new SearchedItemDropDown(10, "November"),
                new SearchedItemDropDown(10, "December")
            };

            return months;
        }

        public List<SearchedItemDropDown> GetYears()
        {
            var currentYear = DateTime.Now.Year;
            var years = new List<SearchedItemDropDown>();
            var counter = 0;
                
            for (int i = 1950; i < currentYear; i++)
            {
                var searchedItem = new SearchedItemDropDown(counter, i.ToString());
                years.Add(searchedItem);

                counter++;
            }

            return years;
        } 

        
        //getDealers();
        //getUnpaidInvoices();
        //getPaidInvoices();
        //getTvChannels();
        //getQ();
        //getEpg();
        //getNewslerters();
    }
}
