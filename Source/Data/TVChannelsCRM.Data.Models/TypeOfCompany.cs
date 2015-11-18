namespace TVChannelsCRM.Data.Models
{
    using Contracts;

    public class TypeOfCompany : DeletableEntity, IEntity
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string TypeInBulgarian { get; set; }
    }
    //public enum TypeOfCompany
    //{
    //    OOD,
    //    EOOD,
    //    KDA,
    //    AD,
    //    KD,
    //    SD
    //}
}