using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using CRM.Data.Models;
using CRM.Web.Common.Mappings;

namespace CRM.Services.Data.ViewModels.Contracts.Discussions
{
    public class DiscussionViewModel : IMapFrom<Discussion>
    {
        //public static Expression<Func<Discussion, DiscussionViewModel>> FromDiscussion
        //{
        //    get
        //    {
        //        return d => new DiscussionViewModel()
        //        {
        //            Id = d.Id,
        //            Date = d.Date,
        //            SubjectOfDiscussion = d.SubjectOfDiscussion,
        //            Summary = d.Summary,
        //            Type = d.Type,
        //            NextDiscussionDate = d.NextDiscussionDate,
        //            NextDiscussionNote = d.NextDiscussionNote,
        //            NextDiscussionType = d.NextDiscussionType,
        //            Comments = d.Comments,
        //            UserId = d.UserId,
        //            ClientId = d.ClientId,
        //            ProviderId = d.ProviderId
        //        };
        //    }
        //}

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DisplayName("Subject of activity")]
        public string SubjectOfDiscussion { get; set; }

        [UIHint("TextAreaEditor")]
        public string Summary { get; set; }

        [Required]
        [UIHint("DiscussionTypeEditor")]
        public DiscussionType Type { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Next activity (date)")]
        public DateTime? NextDiscussionDate { get; set; }

        [DisplayName("Next activity (note)")]
        public string NextDiscussionNote { get; set; }

        [DisplayName("Next activity (type)")]
        public DiscussionType NextDiscussionType { get; set; }

        [UIHint("TextAreaEditor")]
        public string Comments { get; set; }

        [ScaffoldColumn(false)]
        public string UserId { get; set; }

        [ScaffoldColumn(false)]
        public int? ClientId { get; set; }

        [ScaffoldColumn(false)]
        public int? ProviderId { get; set; }

        public bool IsEmpty()
        {
            return this.Summary == null && string.IsNullOrEmpty(this.Summary) &&
                   this.NextDiscussionNote == null && string.IsNullOrEmpty(this.NextDiscussionNote) &&
                   this.NextDiscussionType == DiscussionType.None &&
                   this.Comments == null && string.IsNullOrEmpty(this.Comments);
        }
    }
}