namespace CRM.Services.Logic.Contracts.Contractors
{
    using System.Collections.Generic;

    using Base;
    using Data.ViewModels.Contracts.TypeOfCompanies;

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