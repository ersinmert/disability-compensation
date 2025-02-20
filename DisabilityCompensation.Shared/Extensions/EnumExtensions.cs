using System.ComponentModel;
using System.Reflection;

namespace DisabilityCompensation.Shared.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field?.GetCustomAttribute<DescriptionAttribute>();

            return attribute?.Description ?? value.ToString();
        }

        public static T GetEnumByDescription<T>(this string description) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                var attribute = field.GetCustomAttribute<DescriptionAttribute>();
                if (attribute != null && attribute.Description == description)
                {
                    return (T)field.GetValue(null)!;
                }
            }

            throw new ArgumentException($"'{description}' açıklamasına sahip bir enum bulunamadı.", nameof(description));
        }
    }
}
