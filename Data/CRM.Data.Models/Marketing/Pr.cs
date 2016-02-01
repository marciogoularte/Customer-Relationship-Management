namespace CRM.Data.Models.Marketing
{
    using Contracts;

    public class Pr : DeletableEntity, IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Media { get; set; }

        public bool IsVisible { get; set; }
    }
}
