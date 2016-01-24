using CRM.Web.Common.Mappings;

namespace CRM.Services.Data.ViewModels.Users.Profile
{
    using System;

    using Kendo.Mvc.UI;

    using CRM.Data.Models;

    public class SchedulerTaskViewModel : IMapFrom<SchedulerTask>, ISchedulerEvent
    {
        //public static Expression<Func<SchedulerTask, SchedulerTaskViewModel>> FromSchedulerTask
        //{
        //    get
        //    {
        //        return s => new SchedulerTaskViewModel()
        //        {
        //            Id = s.Id,
        //            Title = s.Title,
        //            Start = s.Start,
        //            End = s.End,
        //            StartTimezone = s.StartTimezone,
        //            EndTimezone = s.EndTimezone,
        //            Description = s.Description,
        //            IsAllDay = s.IsAllDay,
        //            RecurrenceRule = s.RecurrenceRule,
        //            RecurrenceException = s.RecurrenceException,
        //            RecurrenceId = s.RecurrenceId,
        //            UserId = s.UserId,
        //            IsFinished = s.IsFinished
        //        };
        //    }
        //}

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

        public bool IsFinished { get; set; }
    }
}