namespace CRM.Services.Logic.Contracts.Users
{
    using System.Collections.Generic;

    using Base;
    using Data.ViewModels.Users.TypeOfCompanies;

    public interface ITypeOfCompaniesServices : IService
    {
        List<string> Index();

        TypeOfCompanyViewModel TypeOfCompanyInformation(int typeOfCompanyId);

        TypeOfCompanyViewModel TypeOfCompanyDetails(int typeOfCompanyId);

        List<TypeOfCompanyViewModel> ReadTypeOfCompanies(string searchboxTypeOfCompany);

        TypeOfCompanyViewModel CreateTypeOfCompany(TypeOfCompanyViewModel typeOfCompany);

        TypeOfCompanyViewModel UpdateTypeOfCompany(TypeOfCompanyViewModel typeOfCompany);

        TypeOfCompanyViewModel DestroyTypeOfCompany(TypeOfCompanyViewModel typeOfCompany);

        string GetTypeOfCompanyById(int typeOfCompanyId);
    }
}