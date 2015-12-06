namespace CRM.Services.Data.ViewModels.Users.Feedbacks
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class FeedbackViewModel
    {
        [Required]
        public string From { get; set; }

        [Required]
        public string Title { get; set; }

        public string Table { get; set; }

        [DisplayName("Link")]
        public string TableLink { get; set; }

        [Required]
        public string Message { get; set; }
    }
}