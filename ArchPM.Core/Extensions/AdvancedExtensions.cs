﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace ArchPM.Core.Extensions.Advanced
{
    /// <summary>
    /// 
    /// </summary>
    public static class AdvancedExtensions
    {
        /// <summary>
        /// Determines whether the specified expression is numeric.
        /// </summary>
        /// <param name="obj">The expression.</param>
        /// <returns></returns>
        public static Boolean IsNumeric(this Object obj)
        {
            bool isNum = Double.TryParse(Convert.ToString(obj), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out double retNum);
            return isNum;
        }

        /// <summary>
        /// Tries the convert to given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The expression.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static T TryToConvert<T>(this Object obj, T defaultValue)
        {
            if (obj == null)
                return defaultValue;

            String nullable = "Nullable`1";

            var fullName = typeof(T).FullName;
            var name = typeof(T).Name;
            Object result = defaultValue;

            try
            {
                if (name == "Decimal" || (name == nullable && fullName.Contains("System.Decimal")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToDecimal(obj.ToString());
                    }
                }
                else if (name == "Double" || (name == nullable && fullName.Contains("System.Decimal")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToDouble(obj.ToString());
                    }
                }
                else if (name == "Int16" || (name == nullable && fullName.Contains("System.Int16")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToInt16(obj.ToString());
                    }
                }
                else if (name == "Int32" || (name == nullable && fullName.Contains("System.Int32")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToInt32(obj.ToString());
                    }
                }
                else if (name == "Int64" || (name == nullable && fullName.Contains("System.Int64")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToInt64(obj.ToString());
                    }
                }
                else if (name == "UInt16" || (name == nullable && fullName.Contains("System.UInt16")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToUInt16(obj.ToString());
                    }
                }
                else if (name == "UInt32" || (name == nullable && fullName.Contains("System.UInt32")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToUInt32(obj.ToString());
                    }
                }
                else if (name == "UInt64" || (name == nullable && fullName.Contains("System.UInt64")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToUInt64(obj.ToString());
                    }
                }
                else if (name == "Single" || (name == nullable && fullName.Contains("System.Single")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToSingle(obj.ToString());
                    }
                }
                else if (name == "Byte" || (name == nullable && fullName.Contains("System.Byte")))
                {
                    Byte.TryParse(obj.ToString(), out byte temp);
                    result = temp;
                }
                else if (name == "DateTime" || (name == nullable && fullName.Contains("System.DateTime")))
                {
                    DateTime.TryParse(obj.ToString(), out DateTime temp);
                    result = temp;
                }
                else if (name == "Boolean" || (name == nullable && fullName.Contains("System.Boolean")))
                {
                    Boolean.TryParse(obj.ToString(), out bool temp);
                    result = temp;
                }
                else if (name == "String" || (name == nullable && fullName.Contains("System.String")))
                {
                    String temp = Convert.ToString(obj);
                    if (!String.IsNullOrEmpty(temp))
                        result = temp;
                }
            }
            catch
            {
                result = defaultValue;
            }

            return (T)result;
        }

        /// <summary>
        /// Gets the provider.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="currentAssembly">The current assembly.</param>
        /// <param name="constructorArguments">The constructor arguments.</param>
        /// <returns></returns>
        public static IEnumerable<T> GetProvider<T>(this Assembly currentAssembly, params Object[] constructorArguments)
        {
            var types = currentAssembly.GetTypes();
            foreach (var type in types)
            {
                if (!type.IsClass) continue;
                if (type.GetInterfaces().Contains(typeof(T)) || type.BaseType.Name == typeof(T).Name)
                {
                    Type result = type;
                    if (type.ContainsGenericParameters)
                    {
                        result = type.MakeGenericType(typeof(T).GenericTypeArguments);
                    }

                    var provider = (T)Activator.CreateInstance(result, constructorArguments);

                    yield return provider;
                }
            }
        }

        /// <summary>
        /// Gets the provider types.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="currentAssembly">The current assembly.</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetProviderTypes<T>(this Assembly currentAssembly)
        {
            var types = currentAssembly.GetTypes();
            foreach (var type in types)
            {
                if (!type.IsClass) continue;
                if (type.IsAbstract) continue;
                if (type.GetInterfaces().Contains(typeof(T)) || type.BaseType.Name == typeof(T).Name)
                {
                    yield return type;
                }
            }
        }

        /// <summary>
        /// Create and instance and cast
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="constructorArguments"></param>
        /// <returns></returns>
        public static T CreateInstanceAndCast<T>(this Type type, params Object[] constructorArguments)
        {
            if (type.ContainsGenericParameters)
            {
                type = type.MakeGenericType(typeof(T).GenericTypeArguments);
            }
            var instance = (T)Activator.CreateInstance(type, constructorArguments);
            return instance;
        }

        /// <summary>
        /// Adds the property.
        /// </summary>
        /// <param name="expando">The expando.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyValue">The property value.</param>
        public static void AddProperty(this ExpandoObject expando, String propertyName, Object propertyValue)
        {
            var expandoDict = expando as IDictionary<String, Object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }

        /// <summary>
        /// Gets the constants.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static IEnumerable<FieldInfo> GetConstants(this Type type)
        {
            var fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            return fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly);
        }

        /// <summary>
        /// Gets the constants values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static IEnumerable<T> GetConstantsValues<T>(this Type type) where T : class
        {
            var fieldInfos = GetConstants(type);

            return fieldInfos.Select(fi => fi.GetRawConstantValue() as T);
        }
    }
}
