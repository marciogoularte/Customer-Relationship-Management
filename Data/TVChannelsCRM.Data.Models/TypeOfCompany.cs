namespace TVChannelsCRM.Data.Models
{
<<<<<<< HEAD
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
=======
    public enum TypeOfCompany
    {
        OOD,
        EOOD,
        KDA,
        AD,
        KD,
        SD
    }
>>>>>>> 3ac377d6b1c3e2b22f0a38e1c651a753c80d53c8
}