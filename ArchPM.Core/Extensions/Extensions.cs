using ArchPM.Core.Exceptions;
using ArchPM.Core.Extensions.Advanced;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.Core.Extensions
{
    public static class Extensions
    {
        #region Dictionary

        /// <summary>
        /// Finds the key by value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">the value is not found in the dictionary</exception>
        public static TKey FindKeyByValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value)
        {
            dictionary.ThrowExceptionIfNull(new ArgumentNullException("dictionary"));

            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
                if (value.Equals(pair.Value))
                    return pair.Key;

            throw new Exception("the value is not found in the dictionary");
        }

        #endregion

        #region Enums

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static String GetDescription(this Enum obj)
        {
            String result = String.Empty;
            if (obj != null)
            {
                Type type = obj.GetType();
                String name = obj.ToString();
                result = EnumManager.GetEnumDescription(type, name);
            }
            return result;
        }

        /// <summary>
        /// Get value as String
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static String GetValueAsString(this Enum obj)
        {
            Type type = obj.GetType();

            return Convert.ToString((Int32)type.GetField(obj.ToString()).GetValue(obj));
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static String GetName(this Enum obj)
        {
            return Enum.GetName(obj.GetType(), obj);
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static Int32 GetValue(this Enum obj)
        {
            return Convert.ToInt32(obj);
        }


        /// <summary>
        /// Iterates all enum items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="action">The action.</param>
        public static void Foreach<T>(this Enum obj, Action<T> action)
        {
            var enumNames = Enum.GetNames(typeof(T));

            foreach (var enumName in enumNames)
            {
                T enumRole = (T)Enum.Parse(typeof(T), enumName);
                action(enumRole);
            }
        }

        /// <summary>
        /// To the array.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static IEnumerable<Enum> ToArray(this Enum obj)
        {
            foreach (Enum value in Enum.GetValues(obj.GetType()))
                if (obj.HasFlag(value))
                    yield return value;
        }

        /// <summary>
        /// Determines whether [has] [the specified value].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static Boolean Has<T>(this System.Enum type, T value)
        {
            try
            {
                return (((Int32)(Object)type & (Int32)(Object)value) == (Int32)(Object)value);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether [is] [the specified value].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static Boolean Is<T>(this System.Enum type, T value)
        {
            try
            {
                return (Int32)(Object)type == (Int32)(Object)value;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Exception

        /// <summary>
        /// Returns the number of milliseconds since Jan 1, 1970
        /// This method can be used for Json Datetime
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        public static String GetAllMessages(this Exception ex, Boolean showMessageTypeAsHeader = true)
        {
            String message = "";
            if (showMessageTypeAsHeader)
                message = String.Format("[{0}]:", ex.GetType().Name);

            message += ex.Message;

            if (ex.InnerException != null)
                message += String.Concat("\r\n ", GetAllMessages(ex.InnerException, showMessageTypeAsHeader));

            return message;
        }

        public static IEnumerable<Exception> GetAllExceptions(this Exception ex)
        {
            if (ex != null)
                yield return ex;
            if (ex.InnerException != null)
            {
                foreach (var item in GetAllExceptions(ex.InnerException))
                {
                    yield return item;
                }

            }

            yield break;
        }


        #endregion

        #region List

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="projection"></param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="projection"></param>
        public static void ModifyEach<T>(this IList<T> source, Func<T, T> projection)
        {
            for (int i = 0; i < source.Count; i++)
            {
                source[i] = projection(source[i]);
            }
        }

        /// <summary>
        /// Tests if provided list of objects is not null or empty.
        /// Throws ValidationException if it is null or is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objects">list of objects</param>
        /// <exception cref="ArchPM.Core.Exceptions.ValidationException">The list can't be empty</exception>
        public static void NotEmpty<T>(this IList<T> objects)
        {
            if (objects == null || objects.Count == 0)
            {
                throw new ValidationException("The list can't be empty");
            }
        }

        ///// <summary>
        ///// Tests if provided list of objects doesnt contain null values.
        ///// Throws ValidationException if it has null value.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="objects">list of objects</param>
        //public static void NoNullElements<T>(this IList<T> objects)
        //{
        //    foreach (object obj in objects)
        //    {
        //        obj.NotNull();
        //    }
        //}



        #endregion


        /// <summary>
        /// Entities the properties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">entity</exception>
        public static IEnumerable<PropertyDTO> Properties<T>(this T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            PropertyInfo[] properties = entity.GetType().GetProperties();

            foreach (var property in properties)
            {
                //ignore if list, interface, class
                if (property.PropertyType.IsList()
                 || property.PropertyType.IsInterface
                 || (property.PropertyType != typeof(String) && property.PropertyType.IsClass))
                {
                    continue;
                }

                var entityProperty = entity.ConvertPropertyInfoToPropertyDTO(property);

                yield return entityProperty;
            }
        }

        static List<String> listNames = new List<string>() { "IEnumerable`1", "Enumerable", "List`1", "WhereSelectListIterator`2" };
        static Boolean IsList(this Type type)
        {
            return
                (type.ReflectedType != null && listNames.Contains(type.ReflectedType.Name)
              || listNames.Contains(type.Name));
        }

        static PropertyDTO ConvertPropertyInfoToPropertyDTO<T>(this T entity, PropertyInfo property)
        {
            var entityProperty = new PropertyDTO();

            entityProperty.Name = property.Name;
            try
            {
                entityProperty.Value = property.GetValue(entity, null);
            }
            catch
            {
                entityProperty.Value = null;
            }
            entityProperty.ValueType = property.PropertyType.Name;
            entityProperty.ValueTypeOf = property.PropertyType;
            entityProperty.Nullable = false;

            if (property.IsGenericNullable())
            {
                entityProperty.ValueType = Nullable.GetUnderlyingType(property.PropertyType).Name;
                entityProperty.ValueTypeOf = Nullable.GetUnderlyingType(property.PropertyType);
                entityProperty.Nullable = true;
            }

            //when datetime gets the default value
            if (entityProperty.Value != null && entityProperty.ValueTypeOf == typeof(DateTime) && (DateTime)entityProperty.Value == default(DateTime))
            {
                entityProperty.Value = null;
                entityProperty.Nullable = true;
            }
            //String is reference type, it can be null
            if (entityProperty.ValueTypeOf == typeof(String))
            {
                entityProperty.Nullable = true;
            }
            entityProperty.IsPrimitive = entityProperty.ValueTypeOf.IsDotNetPirimitive();
            entityProperty.IsEnum = IsEnumOrIsBaseEnum(entityProperty.ValueTypeOf);

            return entityProperty;
        }

        public static Boolean IsGenericNullable(this PropertyInfo property)
        {
            return property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

        public static Boolean IsDotNetPirimitive(this Type systemType)
        {
            if (systemType == typeof(String)
                || systemType == typeof(Int32)
                || systemType == typeof(Int64)
                || systemType == typeof(Int16)
                || systemType == typeof(float)
                || systemType == typeof(Decimal)
                || systemType == typeof(DateTime)
                || systemType == typeof(Boolean)
                || systemType == typeof(Guid)
                || systemType == typeof(Enum)
                || IsEnumOrIsBaseEnum(systemType))
                return true;
            else
                return false;
        }

        static Boolean IsEnumOrIsBaseEnum(Type type)
        {
            return type.IsEnum || (type.BaseType != null && type.BaseType == typeof(Enum));
        }
    }
}
