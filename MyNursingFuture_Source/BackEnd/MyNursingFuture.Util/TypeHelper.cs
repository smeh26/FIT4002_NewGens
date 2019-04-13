using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.Util
{
    public class ItemType
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Value { get; set; }
    }

    public static class TypeHelper
    {
        public static IEnumerable<ItemType> GetAll<T>()
        {
            Type enumType = typeof(T);

            return enumType.GetEnumValues().Cast<int>().Select(val =>
            {

                var type = Enum.GetName(enumType, val);
                var attributes = enumType.GetField(type).GetCustomAttributes(true);

                var titleAttribute = attributes.OfType<DescriptionAttribute>().FirstOrDefault();
                var title = titleAttribute == null ? string.Empty : titleAttribute.Description;

                var actionAttribute = attributes.OfType<CategoryAttribute>().FirstOrDefault();
                var action = actionAttribute == null ? string.Empty : actionAttribute.IsDefaultAttribute() ? "Default" : actionAttribute.Category;

                return new ItemType() { Description = string.IsNullOrEmpty(title) ? type : title, Category = action, Type = type, Value = val };

            });
        }
        public static ItemType Get<T>(string forType)
        {
            return GetAll<T>().Where(x => x.Type == forType).SingleOrDefault();
        }

    }
}
