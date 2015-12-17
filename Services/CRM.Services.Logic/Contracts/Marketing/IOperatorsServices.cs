namespace CRM.Services.Logic.Contracts.Marketing
{
    using System.Collections.Generic;

    using Base;
    using Data.ViewModels.Marketing.Partners;

    public interface IOperatorsServices : IService
    {
        List<string> Index();

        OperatorViewModel OperatorInformation(int operatorId);

        OperatorViewModel OperatorDetails(int operatorId);

        List<OperatorViewModel> ReadOperators(string searchboxOperator);

        OperatorViewModel CreateOperator(OperatorViewModel givenOperator);

        OperatorViewModel UpdateOperator(OperatorViewModel givenOperator);

        OperatorViewModel DestroyOperator(OperatorViewModel givenOperator);

        string GetOperatorById(int operatorId);
    }
}