namespace CRM.Services.Data.ViewModels.Marketing.Reports
{
    using System.ComponentModel.DataAnnotations;

    public enum ChooseMarketingReportDropdown
    {
        [Display(Description = "By EPG")]
        ByEpg = 9,

        [Display(Description = "By Newsletter")]
        ByNewsLetter = 10
    }
}