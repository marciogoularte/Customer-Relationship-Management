namespace CRM.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum Tier
    {
        [Display(Name = "Basic Analogue")]
        BasicAnalogue,

        [Display(Name = "Economy(Lifeline) Analogue")]
        EconomyAnalogue,

        [Display(Name = "Extended Basix")]
        ExtendedBasix,

        [Display(Name = "Basic Digital")]
        BasicDigital,

        [Display(Name = "Extra Basic")]
        ExtraBasic,

        [Display(Name = "Premium")]
        Premium,

        [Display(Name = "Tematic")]
        Tematic
    }
}
