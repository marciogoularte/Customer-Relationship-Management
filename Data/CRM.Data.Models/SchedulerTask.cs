namespace CRM.Data.Models
{
    using System;

    using Kendo.Mvc.UI;

    using Contracts;

    public class SchedulerTask : DeletableEntity, IEntity, ISchedulerEvent
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string StartTimezone { get; set; }

        public string EndTimezone { get; set; }

        public string Description { get; set; }

        public bool IsAllDay { get; set; }

        public string RecurrenceRule { get; set; }

        public string RecurrenceException { get; set; }

        public string RecurrenceId { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public bool IsFinished { get; set; }
    }
}
