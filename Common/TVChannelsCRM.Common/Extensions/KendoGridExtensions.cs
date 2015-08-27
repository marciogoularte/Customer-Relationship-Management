namespace TVChannelsCRM.Common.Extensions
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections.Generic;

    public static class KendoGridExtensions
    {
        public static List<SelectListItem> EnumToSelectList(Type enumType)
        {
            return Enum
              .GetValues(enumType)
              .Cast<int>()
              .Select(i => new SelectListItem
              {
                  Value = i.ToString(),
                  Text = Enum.GetName(enumType, i),
              })
              .ToList();
        }
    }
}