namespace TVChannelsCRM.Data.Models
{
    using System.ComponentModel;

    public enum Tier
    {
        [Description("Basic Analogue")]
        BasicAnalogue,

        [Description("Economy(Lifeline) Analogue")]
        EconomyAnalogue,

        [Description("Extended Basix")]
        ExtendedBasix,

        [Description("Basic Digital")]
        BasicDigital,

        [Description("Extra Basic")]
        ExtraBasic,

        [Description("Premium")]
        Premium,

        [Description("Tematic")]
        Tematic
    }
}
