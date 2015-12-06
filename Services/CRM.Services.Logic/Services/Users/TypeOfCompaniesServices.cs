﻿namespace CRM.Services.Logic.Services.Users
{
    using System.Linq;
    using System.Collections.Generic;

    using CRM.Data;
    using CRM.Data.Models;
    using Contracts.Users;
    using Data.ViewModels.Users.TypeOfCompanies;

    public class TypeOfCompaniesServices : ITypeOfCompaniesServices
    {
        private ICRMData Data { get; set; }

        public TypeOfCompaniesServices(ICRMData data)
        {
            this.Data = data;
        }

        public List<string> Index()
        {
            var types = this.Data.TypeOfCompanies
                .All()
                .Select(p => p.Type)
                .ToList();

            return types;
        }

        public TypeOfCompanyViewModel TypeOfCompanyInformation(int typeOfCompanyId)
        {
            var typeOfCompany = this.Data.TypeOfCompanies
                .All()
                .Select(TypeOfCompanyViewModel.FromTypeOfCompany)
                .FirstOrDefault(p => p.Id == typeOfCompanyId);

            return typeOfCompany;
        }

        public TypeOfCompanyViewModel TypeOfCompanyDetails(int typeOfCompanyId)
        {
            var typeOfCompany = this.Data.TypeOfCompanies
                .All()
                .Select(TypeOfCompanyViewModel.FromTypeOfCompany)
                .FirstOrDefault(t => t.Id == typeOfCompanyId);

            return typeOfCompany;
        }

        public List<TypeOfCompanyViewModel> ReadTypeOfCompanies(string searchboxTypeOfCompany)
        {
            List<TypeOfCompanyViewModel> TypeOfCompanies;

            if (string.IsNullOrEmpty(searchboxTypeOfCompany) || searchboxTypeOfCompany == "")
            {
                TypeOfCompanies = this.Data.TypeOfCompanies
                .All()
                .Select(TypeOfCompanyViewModel.FromTypeOfCompany)
                .ToList();
            }
            else
            {
                TypeOfCompanies = this.Data.TypeOfCompanies
                .All()
                .Select(TypeOfCompanyViewModel.FromTypeOfCompany)
                .Where(p => p.Type.Contains(searchboxTypeOfCompany))
                .ToList();
            }

            return TypeOfCompanies;
        }

        public TypeOfCompanyViewModel CreateTypeOfCompany(TypeOfCompanyViewModel typeOfCompany)
        {
            if (typeOfCompany == null)
            {
                return typeOfCompany;
            }

            var newTypeOfCompany = new TypeOfCompany
            {
                Type = typeOfCompany.Type,
                TypeInBulgarian = typeOfCompany.TypeInBulgarian
            };

            this.Data.TypeOfCompanies.Add(newTypeOfCompany);
            this.Data.SaveChanges();

            typeOfCompany.Id = newTypeOfCompany.Id;

            return typeOfCompany;
        }

        public TypeOfCompanyViewModel UpdateTypeOfCompany(TypeOfCompanyViewModel typeOfCompany)
        {
            var typeOfCompanyFromDb = this.Data.TypeOfCompanies.All()
                   .FirstOrDefault(p => p.Id == typeOfCompany.Id);

            if (typeOfCompany == null || typeOfCompanyFromDb == null)
            {
                return typeOfCompany;
            }

            typeOfCompanyFromDb.Type = typeOfCompany.Type;
            typeOfCompanyFromDb.TypeInBulgarian = typeOfCompany.TypeInBulgarian;

            this.Data.SaveChanges();

            return typeOfCompany;
        }

        public TypeOfCompanyViewModel DestroyTypeOfCompany(TypeOfCompanyViewModel typeOfCompany)
        {
            this.Data.Providers.Delete(typeOfCompany.Id);
            this.Data.SaveChanges();

            return typeOfCompany;
        }

        public string GetTypeOfCompanyById(int typeOfCompanyId)
        {
            var typeOfCompany = this.Data.TypeOfCompanies
                .All()
                .Where(t => t.Id == typeOfCompanyId)
                .Select(t => t.Type.ToString())
                .FirstOrDefault();

            return typeOfCompany;
        }
    }
}
