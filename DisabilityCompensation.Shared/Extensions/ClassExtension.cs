using System.Reflection;

namespace DisabilityCompensation.Shared.Extensions
{
    public static class ClassExtension
    {
        public static void MapProperties<TSource, TTarget>(this TSource source, TTarget target) where TSource : class
        {
            PropertyInfo[] sourceProperties = typeof(TSource).GetProperties();
            PropertyInfo[] targetProperties = typeof(TTarget).GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                foreach (var targetProperty in targetProperties)
                {
                    if (targetProperty.Name == sourceProperty.Name &&
                        targetProperty.PropertyType == sourceProperty.PropertyType)
                    {
                        targetProperty.SetValue(target, sourceProperty.GetValue(source));
                        break;
                    }

                    if (targetProperty.Name == sourceProperty.Name &&
                        targetProperty.PropertyType == typeof(int) &&
                        sourceProperty.PropertyType == typeof(bool))
                    {
                        var value = Convert.ToInt32(sourceProperty.GetValue(source));
                        targetProperty.SetValue(target, value);
                        break;
                    }
                }
            }
        }

    }
}
