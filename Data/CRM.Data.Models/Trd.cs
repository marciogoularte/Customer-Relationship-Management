namespace CRM.Data.Models
{
    using Contracts;
    
    public class Trd : DeletableEntity, IEntity
    {
        public int Id { get; set; }

        public string Decoder { get; set; }

        public string Sim { get; set; }

        public string Cas { get; set; }

        public string Cam { get; set; }

        public int? ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}
