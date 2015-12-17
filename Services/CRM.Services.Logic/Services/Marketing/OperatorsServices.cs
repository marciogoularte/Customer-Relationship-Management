namespace CRM.Services.Logic.Services.Marketing
{
    using System.Linq;
    using System.Collections.Generic;

    using CRM.Data;
    using CRM.Data.Models.Marketing;
    using Data.ViewModels.Marketing.Partners;
    using Contracts.Marketing;

    public class OperatorsServices : IOperatorsServices
    {
        private ICRMData Data { get; set; }

        public OperatorsServices(ICRMData data)
        {
            this.Data = data;
        }

        public List<string> Index()
        {
            var operators = this.Data.Operators
                .All()
                .Select(p => p.Name)
                .ToList();

            return operators;
        }

        public OperatorViewModel OperatorInformation(int operatorId)
        {
            var operatorInfo = this.Data.Operators
                .All()
                .Select(OperatorViewModel.FromOperator)
                .FirstOrDefault(p => p.Id == operatorId);

            return operatorInfo;
        }

        public OperatorViewModel OperatorDetails(int operatorId)
        {
            var operatorDetails = this.Data.Operators
                .All()
                .Select(OperatorViewModel.FromOperator)
                .FirstOrDefault(t => t.Id == operatorId);

            return operatorDetails;
        }

        public List<OperatorViewModel> ReadOperators(string searchboxOperator)
        {
            List<OperatorViewModel > readOperators;

            if (string.IsNullOrEmpty(searchboxOperator) || searchboxOperator == "")
            {
                readOperators = this.Data.Operators
                .All()
                .Select(OperatorViewModel.FromOperator)
                .ToList();
            }
            else
            {
                readOperators = this.Data.Operators
                .All()
                .Select(OperatorViewModel.FromOperator)
                .Where(p => p.Name.Contains(searchboxOperator))
                .ToList();
            }

            return readOperators;
        }

        public OperatorViewModel CreateOperator(OperatorViewModel givenOperator)
        {
            if (givenOperator == null)
            {
                return null;
            }

            var newoperator = new Operator
            {
                Name = givenOperator.Name,
                Address = givenOperator.Address,
                PhoneNumber = givenOperator.PhoneNumber,
                Email = givenOperator.Email,
                Media = givenOperator.Media
            };

            this.Data.Operators.Add(newoperator);
            this.Data.SaveChanges();

            givenOperator.Id = newoperator.Id;

            return givenOperator;
        }

        public OperatorViewModel UpdateOperator(OperatorViewModel givenOperator)
        {
            var operatorFromDb = this.Data.Operators
                .All()
              .FirstOrDefault(p => p.Id == givenOperator.Id);

            if (givenOperator == null || operatorFromDb == null)
            {
                return givenOperator;
            }

            operatorFromDb.Name = givenOperator.Name;
            operatorFromDb.Address = givenOperator.Address;
            operatorFromDb.PhoneNumber = givenOperator.PhoneNumber;
            operatorFromDb.Email = givenOperator.Email;
            operatorFromDb.Media = givenOperator.Media;

            this.Data.SaveChanges();

            return givenOperator;
        }

        public OperatorViewModel DestroyOperator(OperatorViewModel givenOperator)
        {
            this.Data.Operators.Delete(givenOperator.Id);
            this.Data.SaveChanges();

            return givenOperator;
        }

        public string GetOperatorById(int operatorId)
        {
            var operatorById = this.Data.Operators
                .All()
                .Where(t => t.Id == operatorId)
                .Select(t => t.Name.ToString())
                .FirstOrDefault();

            return operatorById;
        }
    }
}
