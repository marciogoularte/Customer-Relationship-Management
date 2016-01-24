using System.Collections.Generic;
using CRM.Services.Data.ViewModels.Contracts.TypeOfCompanies;
using CRM.Services.Logic.Base;

namespace CRM.Services.Logic.Contracts.Contractors
{
    public interface ITypeOfCompaniesServices : IService
    {
        List<string> Index();

        TypeOfCompanyViewModel TypeOfCompanyInformation(int TypeOfCompany);

        TypeOfCompanyViewModel TypeOfCompanyDetails(int TypeOfCompany);

        List<TypeOfCompanyViewModel> ReadTypeOfCompanies(string searchboxTypeOfCompany);

        TypeOfCompanyViewModel CreateTypeOfCompany(TypeOfCompanyViewModel typeOfCompany);

        TypeOfCompanyViewModel UpdateTypeOfCompany(TypeOfCompanyViewModel typeOfCompany);

        TypeOfCompanyViewModel DestroyTypeOfCompany(TypeOfCompanyViewModel typeOfCompany);

        string GetTypeOfCompanyById(int TypeOfCompany);
    }
}