namespace CRM.Common.Extensions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using System.ComponentModel.DataAnnotations;

    public static class EnumExtensions
    {
        public static string GetDescriptionOfEnum(Enum value)
        {
            var type = value.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException($"Type '{type}' is not Enum");
            }

            var members = type.GetMember(value.ToString());
            if (members.Length == 0)
            {
                throw new ArgumentException($"Member '{value}' not found in type '{type.Name}'");
            }

            var member = members[0];
            var attributes = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            if (attributes.Length == 0)
            {
                throw new ArgumentException($"'{type.Name}.{value}' doesn't have DisplayAttribute");
            }

            var attribute = (DisplayAttribute)attributes[0];
            return attribute.Description;
        }

        private static readonly SelectListItem[] SingleEmptyItem = { new SelectListItem { Text = string.Empty, Value = string.Empty } };

        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            return htmlHelper.EnumDropDownListFor(expression, null);
        }

        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var enumType = GetNonNullableModelType(metadata);
            var values = Enum.GetValues(enumType).Cast<TEnum>();

            var items =
                values.Select(value => new SelectListItem
                {
                    Text = GetName(value),
                    Value = value.ToString(),
                    Selected = value.Equals(metadata.Model)
                });

            if (metadata.IsNullableValueType)
            {
                items = SingleEmptyItem.Concat(items);
            }

            return htmlHelper.DropDownListFor(expression, items, htmlAttributes);
        }

        private static string GetName<TEnum>(TEnum value)
        {
            var displayAttribute = GetDisplayAttribute(value);

            return displayAttribute == null ? value.ToString() : displayAttribute.GetName();
        }

        private static DisplayAttribute GetDisplayAttribute<TEnum>(TEnum value)
        {
            return value.GetType()
                        .GetField(value.ToString())
                        .GetCustomAttributes(typeof(DisplayAttribute), false)
                        .Cast<DisplayAttribute>()
                        .FirstOrDefault();
        }

        private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            var realModelType = modelMetadata.ModelType;
            var underlyingType = Nullable.GetUnderlyingType(realModelType);

            return underlyingType ?? realModelType;
        }
    }
}
