using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Reflection;

namespace Abnaki.Windows
{
    /// <summary>
    /// Reflection - querying Types
    /// </summary>
    public class AbnakiReflection
    {
        /// <summary>
        /// Label attribute of enum, or by default the ordinary string
        /// </summary>
        public static string LabelOfEnum(object enumValue)
        {
            LabelAttribute attr = AttributeOfEnum<LabelAttribute>(enumValue);
            if (attr == null)
                return enumValue.ToString();

            return attr.Label;
        }

        /// <summary>
        /// LabelAttribute Detail of enum, or else null
        /// </summary>
        public static string DetailOfEnum(object enumValue)
        {
            LabelAttribute attr = AttributeOfEnum<LabelAttribute>(enumValue);
            if (attr == null)
                return null;

            return attr.Detail;
        }

        /// <returns>null if no T exists
        /// </returns>
        static T AttributeOfEnum<T>(object enumValue) where T : Attribute
        {
            FieldInfo field = enumValue.GetType().GetField(enumValue.ToString());
            return field.GetCustomAttribute<T>();
        }
    }
}
