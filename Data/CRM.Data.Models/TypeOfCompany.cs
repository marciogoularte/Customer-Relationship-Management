namespace CRM.Data.Models
{
    using Contracts;

    public class TypeOfCompany : DeletableEntity, IEntity
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string TypeInBulgarian { get; set; }
    }
}