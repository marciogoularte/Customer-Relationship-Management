namespace CRM.Data.Models
{
    using System;

    using Contracts;

    public class Campaign : DeletableEntity, IEntity
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Activities { get; set; }

        public double Budget { get; set; }

        public string Results { get; set; }

        // TODO:
       //  - Партньори по кампанията
       //- провайдъри 
       //- клиенти
    }
}
