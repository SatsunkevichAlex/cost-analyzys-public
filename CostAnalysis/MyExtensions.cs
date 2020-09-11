using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace CostAnalysis
{
    public static class MyExtensions
    {
        public static List<KeyValuePair<string, double>> TransformShopsToLookup(this string shops)
        {
            if (String.IsNullOrEmpty(shops))
            {
                return new List<KeyValuePair<string, double>>() { new KeyValuePair<string, double>("", 0) };
            }
            List<KeyValuePair<string, double>> result = JsonConvert.DeserializeObject<List<KeyValuePair<string, double>>>(shops);
            return result;
        }

        public static string GetDescription<T>(this T enumerationValue) where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException($"{nameof(enumerationValue)} must by of Enum type", nameof(enumerationValue));
            }

            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return enumerationValue.ToString();
        }

        public static bool CostDateEquals(this DateTime date1, DateTime date2)
        {
            return date1.Year == date2.Year
                && date1.Month == date2.Month
                && date1.Day == date2.Day;
        }
    }
}