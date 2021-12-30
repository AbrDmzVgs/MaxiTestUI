using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class UrlHelper
    {
        public static string ToQueryString(this object request)
        {
            if (request == null || request.GetType() == typeof(string))
                return "";
            StringBuilder query = request.ToQueryStringBuilder();

            if (query.Length > 0)
                query[0] = '?';

            return query.ToString();
        }

        private static StringBuilder ToQueryStringBuilder(this object obj, string prefix = "")
        {
            var stringBuilder = new StringBuilder();

            foreach (var p in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => !Attribute.IsDefined(x, typeof(SkipPropertyAttribute))).ToArray())
            {
                if (p.GetValue(obj, new object[0]) != null)
                {
                    var value = p.GetValue(obj, new object[0]);

                    if (p.PropertyType.IsArray && value.GetType() == typeof(DateTime[]))
                        foreach (var item in value as DateTime[])
                            stringBuilder.Append(string.Format("&{0}{1}={2}", prefix, p.Name, item.ToString("yyyy-MM-ddTHH:mm:ss.fff")));

                    else if (p.PropertyType.IsArray)
                        foreach (var item in value as Array)
                            stringBuilder.Append(string.Format("&{0}{1}={2}", prefix, p.Name, item));

                    else if (p.PropertyType == typeof(string))
                        stringBuilder.Append(string.Format("&{0}{1}={2}", prefix, p.Name, value));

                    else if ((p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(Nullable<DateTime>)) && !value.Equals(Activator.CreateInstance(p.PropertyType)))
                        stringBuilder.Append(string.Format("&{0}{1}={2}", prefix, p.Name, ((DateTime)value).ToString("yyyy-MM-ddTHH:mm:ss.fff")));

                    else if (p.PropertyType.IsValueType && !value.Equals(Activator.CreateInstance(p.PropertyType)))
                        stringBuilder.Append(string.Format("&{0}{1}={2}", prefix, p.Name, value));

                    else if (p.PropertyType.IsClass)
                        stringBuilder.Append(value.ToQueryStringBuilder(string.Format("&{0}{1}.", prefix, p.Name)));
                }
            }

            return stringBuilder;
        }
    }
    public class SkipPropertyAttribute : Attribute
    {
    }
}
