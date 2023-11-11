using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Data;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace MOEWiki.Parser2
{
    public static class ObjectExtensions
    {
        public static T Clone<T>(this T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            if (ReferenceEquals(source, null))
            {
                return default(T);
            }

            var formatter = new BinaryFormatter();
            var stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        public static T CreateObject<T>(this NameValueCollection args) where T : new()
        {
            var result = new T();
            foreach (string key in args.Keys)
            {
                var property = typeof(T).GetProperty(key, BindingFlags.Public | BindingFlags.Instance);
                if (property != null) property.SetValue(result, args[key].To(property.PropertyType, null), null);
            }

            return result;
        }

        public static bool HasValue(this object obj)
        {
            if (obj is string)
            {
                return !string.IsNullOrEmpty(obj.ToString());
            }
            else
            {
                return obj != null;
            }
        }

        public static T To<T>(this object obj)
        {
            return obj.To(default(T));
        }

        public static T ToEnum<T>(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return default(T);
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static T To<T>(this object obj, T defaultValue)
        {
            if (obj == null || obj == DBNull.Value || Equals(obj, string.Empty))
            {
                return defaultValue;
            }
            if (obj is T)
            {
                return (T)obj;
            }
            var type = typeof(T);
            if (type == typeof(string))
            {
                return (T)obj;
            }
            var underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
            {
                return To(obj, defaultValue, underlyingType);
            }
            return To(obj, defaultValue, type);
        }

        public static object To(this object obj, Type type, object defaultValue)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return defaultValue;
            }
            if (obj.GetType() == type)
            {
                return obj;
            }
            if (type == typeof(string))
            {
                return obj;
            }
            var underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
            {
                return To(obj, defaultValue, underlyingType);
            }
            return To(obj, defaultValue, type);
        }

        public static object To(this object obj, Type type)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return new object();
            }
            if (obj.GetType() == type)
            {
                return obj;
            }
            if (type == typeof(string))
            {
                return obj;
            }
            var underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
            {
                return To(obj, new object(), underlyingType);
            }
            return To(obj, new object(), type);
        }

        private static T To<T>(object obj, T defaultValue, Type type)
        {
            if (type.IsEnum)
            {
                try
                {
                    if (obj is decimal || obj is long || Enum.IsDefined(type, obj))
                    {
                        return (T)Enum.Parse(type, obj.ToString());
                    }
                    else if (obj is string)
                    {
                        return (T)Enum.Parse(type, (string)obj);
                    }
                }
                catch
                {
                    return defaultValue;
                }

                return defaultValue;
            }
            try
            {
                if (type == typeof(Guid))
                {
                    return (T)(object)new Guid(obj.ToString());
                }
                else
                {
                    return (T)Convert.ChangeType(obj, type);
                }
            }
            catch
            {
                return defaultValue;
            }
        }

        public static bool ToBool(this object obj, bool defaultValue = false)
        {
            bool boolValue;
            int intValue;

            if (obj == null || obj == DBNull.Value)
                return defaultValue;

            if (obj is bool)
                return (bool)obj;

            if (bool.TryParse(obj.ToString(), out boolValue))
                return boolValue;

            return int.TryParse(obj.ToString(), out intValue) && (intValue != 0);
        }

        public static decimal ToDecimal(this object obj)
        {
            if (obj != null)
            {
                decimal decValue;
                if (obj is decimal)
                {
                    return (decimal)obj;
                }
                if (
                    decimal.TryParse(
                        obj.ToString().Replace(CultureInfo.InvariantCulture.NumberFormat.CurrencyDecimalSeparator,
                                               CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator),
                        out decValue))
                {
                    return decValue;
                }
            }
            return 0M;
        }
        public static decimal? ToDecimalNull(this object obj)
        {
            if (obj == null) return null;
            if (obj is decimal) return (decimal)obj;

            decimal decValue;

            if (decimal.TryParse(obj.ToString().Replace(CultureInfo.InvariantCulture.NumberFormat.CurrencyDecimalSeparator,
                                                        CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator),
                                 out decValue))
            {
                return decValue;
            }

            return null;
        }

        public static float ToSingle(this object obj)
        {
            if (obj != null)
            {
                float sinValue;
                if (obj is float)
                {
                    return (float)obj;
                }
                if (
                    float.TryParse(
                        obj.ToString().Replace(CultureInfo.InvariantCulture.NumberFormat.CurrencyDecimalSeparator,
                                               CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator),
                        out sinValue))
                {
                    return sinValue;
                }
            }
            return 0f;
        }

        public static double ToDouble(this object obj)
        {
            if (obj != null)
            {
                double dbValue;
                if (obj is double)
                {
                    return (double)obj;
                }
                if (
                    double.TryParse(
                        obj.ToString().Replace(CultureInfo.InvariantCulture.NumberFormat.CurrencyDecimalSeparator,
                                               CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator),
                        out dbValue))
                {
                    return dbValue;
                }
            }
            return 0.0;
        }

        public static int ToInt(this object obj)
        {
            if ((obj != null) && (obj != DBNull.Value))
            {
                int intValue;
                if (obj is int)
                {
                    return (int)obj;
                }
                if (obj is bool)
                {
                    return (bool)obj ? 1 : 0;
                }
                if (int.TryParse(obj.ToString(), out intValue))
                {
                    return intValue;
                }
            }
            return 0;
        }

        public static string ToStr(this object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return string.Empty;
            }
            if (obj is string)
            {
                return (string)obj;
            }
            return obj.ToString();
        }

        public static byte[] ToBiteArray(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            if (obj is Stream)
            {
                Stream stream = obj as Stream;
                byte[] buffer = new byte[stream.Length];
                using (MemoryStream ms = new MemoryStream())
                {
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    return ms.ToArray();
                }
            }
            return new byte[0];
        }

        public static DateTime? ToDateTime(this object obj)
        {
            if (obj == null) return null;
            if (obj == DBNull.Value) return null;
            if (obj is DateTime) return (DateTime)obj;

            DateTime result;
            if (DateTime.TryParse(obj.ToString(), out result))
                return result;

            return null;
        }

        public static string ToYesNo(this object obj)
        {
            if (obj == null) return "";
            if (obj == DBNull.Value) return "";

            if (obj is bool) return (bool)obj ? "Да" : "Нет";
            if (obj is int) return Convert.ToBoolean((int)obj) ? "Да" : "Нет";

            bool boolVal;
            if (bool.TryParse(obj.ToString().Trim(), out boolVal))
                return boolVal ? "Да" : "Нет";

            return "";
        }

        public static Guid ToGuid(this object obj)
        {
            if (obj == null) return Guid.Empty;
            if (obj == DBNull.Value) return Guid.Empty;
            if (obj is Guid) return (Guid)obj;

            Guid result;
            if (Guid.TryParse(obj.ToString(), out result))
                return result;

            return Guid.Empty;
        }

        /// <summary>
        /// Получаем все свойства объекта
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="outType"></param>
        public static List<PropertyInfo> Properties(this object obj)
        {
            return obj.GetType()
                      .GetProperties()
                      .Where(w => w.CanWrite)
                      .ToList();
        }

        public static void SetValueByPropertyName(this object obj, string propName, object value)
        {
            if (value == null || value.GetType() == typeof(DBNull))
                return;

            var prop = obj.GetType()
                          .GetProperty(propName);

            if (prop == null)
                return;

            if (prop.PropertyType == typeof(Guid) || prop.PropertyType == typeof(Guid?))
                prop.SetValue(obj, Guid.Parse(value.ToString()), null);
            else if (prop.PropertyType == typeof(Int16) || prop.PropertyType == typeof(Int16?))
                prop.SetValue(obj, Convert.ToInt16(value), null);
            else if (prop.PropertyType == typeof(Int32) || prop.PropertyType == typeof(Int32?))
                prop.SetValue(obj, Convert.ToInt32(value), null);
            else if (prop.PropertyType == typeof(Int64) || prop.PropertyType == typeof(Int64?))
                prop.SetValue(obj, Convert.ToInt64(value), null);
            else if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
                prop.SetValue(obj, DateTime.Parse(value.ToString()), null);
            else if (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?))
            {
                if (value.GetType() == typeof(bool))
                    prop.SetValue(obj, value, null);
                else if (value.GetType() == typeof(Int16) || value.GetType() == typeof(Int16?))
                    prop.SetValue(obj, bool.Parse(Convert.ToInt16(value) == 0 ? "False" : "True"), null);
                else if (value.GetType() == typeof(Int32) || value.GetType() == typeof(Int32?))
                    prop.SetValue(obj, bool.Parse(Convert.ToInt32(value) == 0 ? "False" : "True"), null);
                else if (value.GetType() == typeof(Int64) || value.GetType() == typeof(Int64?))
                    prop.SetValue(obj, bool.Parse(Convert.ToInt64(value) == 0 ? "False" : "True"), null);
                else
                    prop.SetValue(obj, bool.Parse(value.ToString()), null);
            }
            else
                prop.SetValue(obj, value, null);
        }

        public static object GetValueByPropertyName(this object obj, string propName)
        {
            var prop = obj.GetType()
                          .GetProperty(propName);

            if (prop == null)
                return null;

            return prop.GetValue(obj, null);
        }

        public static T Map<T>(this object source, params object[] args) where T : class
        {
            if (source == null)
                return null;

            T result;

            if (args?.Any() ?? false)
                result = (T)Activator.CreateInstance(typeof(T), args);
            else
                result = (T)Activator.CreateInstance(typeof(T));

            source.MapTo(result);

            return result;
        }

        public static void MapTo(this object source, object dest)
        {
            foreach (var prop in source.GetType().GetProperties())
            {

                var destProp = dest.GetType()
                                   .GetProperties()
                                   .Where(w => w.CanWrite && w.Name == prop.Name)
                                   .FirstOrDefault();

                if (destProp == null)
                    continue;

                if (prop.PropertyType != destProp.PropertyType)
                    continue;

                var valSource = prop.GetValue(source, null);
                var valDest = destProp.GetValue(dest, null);

                if (valSource != valDest)
                    dest.SetValueByPropertyName(prop.Name, valSource);
            }
        }

        public static T RowMap<T>(this DataRow source) where T : class
        {
            T result = (T)Activator.CreateInstance(typeof(T));
            foreach (var prop in result.GetType().GetProperties())
                if (source.Table.Columns.Contains(prop.Name))
                    result.SetValueByPropertyName(prop.Name, source[prop.Name]);

            return result;
        }
    }
}
