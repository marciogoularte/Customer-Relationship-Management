namespace CRM.Data.Models.Marketing
{
    using Contracts;

    public class SocialPartner : DeletableEntity, IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Website { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public SocialSystemType SocialSystem { get; set; }
    }
}
