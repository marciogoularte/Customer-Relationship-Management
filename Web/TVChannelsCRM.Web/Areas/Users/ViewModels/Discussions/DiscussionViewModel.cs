namespace TVChannelsCRM.Web.Areas.Users.ViewModels.Discussions
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.ComponentModel.DataAnnotations;

    using Data.Models;

    public class DiscussionViewModel
    {
        public static Expression<Func<Discussion, DiscussionViewModel>> FromDiscussion
        {
            get
            {
                return d => new DiscussionViewModel()
                {
                    Id = d.Id,
                    Date = d.Date,
                    SubjectOfDiscussion = d.SubjectOfDiscussion,
                    Summary = d.Summary,
                    Comments = d.Comments,
                    UserId = d.UserId,
                    ClientId = d.ClientId,
                    ProviderId = d.ProviderId
                };
            }
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DisplayName("Subject of discussion")]
        public string SubjectOfDiscussion { get; set; }

        [UIHint("TextAreaEditor")]
        public string Summary { get; set; }

        [UIHint("TextAreaEditor")]
        public string Comments { get; set; }

        [ScaffoldColumn(false)]
        public string UserId { get; set; }

        [ScaffoldColumn(false)]
        public int? ClientId { get; set; }

        [ScaffoldColumn(false)]
        public int? ProviderId { get; set; }
    }
}