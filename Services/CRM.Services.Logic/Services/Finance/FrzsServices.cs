namespace CRM.Services.Logic.Services.Finance
{
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper.QueryableExtensions;

    using Contracts.Finance;
    using CRM.Data;
    using CRM.Data.Models.Finance;
    using Data.ViewModels.Finance.Frz;

    public class FrzsServices : IFrzsServices
    {
        private ICRMData Data { get; }

        public FrzsServices(ICRMData data)
        {
            this.Data = data;
        }

        public List<string> Index()
        {
            var frzs = this.Data.Frzs
                .All()
                .Select(p => p.EmployeeName)
                .ToList();

            return frzs;
        }

        public FrzViewModel FrzInformation(int frzId)
        {
            var frzInfo = this.Data.Frzs
                .All()
                .ProjectTo<FrzViewModel>()
                .FirstOrDefault(p => p.Id == frzId);

            return frzInfo;
        }

        public FrzViewModel FrzDetails(int frzId)
        {
            var frzDetails = this.Data.Frzs
                .All()
                .ProjectTo<FrzViewModel>()
                .FirstOrDefault(t => t.Id == frzId);

            return frzDetails;
        }

        public List<FrzViewModel> ReadFrzs(string searchboxFrz, bool showAll)
        {
            List<FrzViewModel> readFrzs;

            if (string.IsNullOrEmpty(searchboxFrz) || searchboxFrz == "")
            {
                if (showAll == false)
                {
                    readFrzs = this.Data.Frzs
                        .All()
                        .Where(frz => frz.IsVisible)
                        .ProjectTo<FrzViewModel>()
                        .ToList();
                }
                else
                {
                    readFrzs = this.Data.Frzs
                    .All()
                    .ProjectTo<FrzViewModel>()
                    .ToList();
                }
            }
            else
            {
                readFrzs = this.Data.Frzs
                .All()
                .ProjectTo<FrzViewModel>()
                .Where(p => p.EmployeeName.Contains(searchboxFrz))
                .ToList();
            }

            return readFrzs;
        }

        public FrzViewModel CreateFrz(FrzViewModel givenFrz)
        {
            if (givenFrz == null)
            {
                return null;
            }

            var newFrz = new Frz
            {
                EmployeeName = givenFrz.EmployeeName,
                NumberOfContract = givenFrz.NumberOfContract,
                DateOfContract = givenFrz.DateOfContract,
                Salary = givenFrz.Salary,
                BankAccount = givenFrz.BankAccount,
                IsVisible = givenFrz.IsVisible
            };

            this.Data.Frzs.Add(newFrz);
            this.Data.SaveChanges();

            givenFrz.Id = newFrz.Id;

            return givenFrz;
        }

        public FrzViewModel UpdateFrz(FrzViewModel givenFrz)
        {
            var frzFromDb = this.Data.Frzs
                .All()
              .FirstOrDefault(p => p.Id == givenFrz.Id);

            if (givenFrz == null || frzFromDb == null)
            {
                return givenFrz;
            }

            frzFromDb.EmployeeName = givenFrz.EmployeeName;
            frzFromDb.NumberOfContract = givenFrz.NumberOfContract;
            frzFromDb.DateOfContract = givenFrz.DateOfContract;
            frzFromDb.Salary = givenFrz.Salary;
            frzFromDb.BankAccount = givenFrz.BankAccount;
            frzFromDb.IsVisible = givenFrz.IsVisible;

            this.Data.SaveChanges();

            return givenFrz;
        }

        public FrzViewModel DestroyFrz(FrzViewModel givenFrz)
        {
            this.Data.Frzs.Delete(givenFrz.Id);
            this.Data.SaveChanges();

            return givenFrz;
        }

        public string GetFrzById(int frzId)
        {
            var frzById = this.Data.Frzs
                .All()
                .Where(t => t.Id == frzId)
                .Select(t => t.EmployeeName.ToString())
                .FirstOrDefault();

            return frzById;
        }
    }
}
