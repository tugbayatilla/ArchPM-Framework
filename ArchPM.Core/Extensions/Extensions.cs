using ArchPM.Core.Enums;
using ArchPM.Core.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace ArchPM.Core.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class Extensions
    {

        /// <summary>
        /// Calculates the profit.
        /// </summary>
        /// <param name="capital">The capital.</param>
        /// <param name="profitPercent">The profit percent.</param>
        /// <returns></returns>
        public static Decimal CalculateProfit(this Decimal capital, Decimal profitPercent)
        {
            Decimal hund = 100M;
            var result = ((capital * profitPercent) / hund) + capital;

            return result;
        }

        /// <summary>
        /// To the message header string.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static String ToMessageHeaderString(this DateTime date)
        {
            var msg = String.Format("[{0:dd-MM-yyyy HH:mm:ss.fffff}][{1}]", DateTime.Now, Thread.CurrentThread.ManagedThreadId);

            return msg;
        }


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

        /// <summary>
        /// Saves the specified key. updates if exist, otherwise insert key
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void Save<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            dictionary.ThrowExceptionIfNull(new ArgumentNullException("dictionary"));

            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        /// <summary>
        /// To the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static T ToObject<T>(this IDictionary<string, object> source) where T : class, new()
        {
            T someObject = new T();
            Type someObjectType = someObject.GetType();

            foreach (KeyValuePair<string, object> item in source)
            {
                someObjectType.GetProperty(item.Key).SetValue(someObject, item.Value, null);
            }

            return someObject;
        }

        /// <summary>
        /// Ases the dictionary.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="bindingAttr">The binding attribute.</param>
        /// <returns></returns>
        public static IDictionary<string, object> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );

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
        /// <returns>
        ///   <c>true</c> if [is] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
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
        /// Gets all exception messages seperated by \r\n
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="showMessageTypeAsHeader">if set to <c>true</c> [show message type as header].</param>
        /// <param name="messageSeperator"></param>
        /// <returns></returns>
        public static String GetAllMessages(this Exception ex, Boolean showMessageTypeAsHeader = true, String messageSeperator = "\r\n")
        {
            String message = "";
            if (showMessageTypeAsHeader)
                message = String.Format("[{0}]:", ex.GetType().Name);

            message += ex.Message;

            if (ex.InnerException != null)
                message += String.Concat(messageSeperator, GetAllMessages(ex.InnerException, showMessageTypeAsHeader));

            return message;
        }

        /// <summary>
        /// Gets all exceptions.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
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
        /// Fors the each.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
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

        #endregion


        /// <summary>
        /// Entities the properties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="predicate">predicate</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">entity</exception>
        [Obsolete("this will be removed version 0.4.0. Use CollectProperties instead!")]
        public static IEnumerable<PropertyDTO> Properties<T>(this T entity, Func<PropertyDTO, Boolean> predicate = null)
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
                entityProperty.Attributes = property.GetCustomAttributes();

                if (predicate != null)
                {
                    if (predicate(entityProperty))
                    {
                        yield return entityProperty;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    yield return entityProperty;
                }

            }
        }

        /// <summary>
        /// Entities the properties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="predicate">predicate</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">entity</exception>
        [Obsolete("this will be removed version 0.4.0. Use CollectProperties instead!")]
        public static IEnumerable<PropertyDTO> PropertiesAll<T>(this T entity, Func<PropertyDTO, Boolean> predicate = null)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            PropertyInfo[] properties = entity.GetType().GetProperties();

            foreach (var property in properties)
            {
                var entityProperty = entity.ConvertPropertyInfoToPropertyDTO(property);
                entityProperty.Attributes = property.GetCustomAttributes();

                if (predicate != null)
                {
                    if (predicate(entityProperty))
                    {
                        yield return entityProperty;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    yield return entityProperty;
                }

            }
        }

        /// <summary>
        /// Collects the properties.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">entityType</exception>
        public static IEnumerable<PropertyDTO> CollectProperties(this Type entityType, Func<PropertyDTO, Boolean> predicate = null)
        {
            if (entityType == null)
                throw new ArgumentNullException("entityType");
            if (entityType.Name == "Void")
                return new List<PropertyDTO>();
            if (entityType.Module.Name == "mscorlib.dll")
                return new List<PropertyDTO>();

            Object entity = null;
            try
            {
                entity = Activator.CreateInstance(entityType);
                return CollectProperties(entity, predicate);
            }
            catch {
                return new List<PropertyDTO>();
            }
        }

        /// <summary>
        /// Collects the properties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">entity</exception>
        public static IEnumerable<PropertyDTO> CollectProperties<T>(this T entity, Func<PropertyDTO, Boolean> predicate = null)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            PropertyInfo[] properties = entity.GetType().GetProperties();

            foreach (var property in properties)
            {
                var entityProperty = entity.ConvertPropertyInfoToPropertyDTO(property);
                entityProperty.Attributes = property.GetCustomAttributes();

                if (predicate != null)
                {
                    if (predicate(entityProperty))
                    {
                        yield return entityProperty;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    yield return entityProperty;
                }

            }
        }


        /// <summary>
        /// The defined list names
        /// </summary>
        public static List<String> DefinedListNames = new List<string>() {
            nameof(ArrayList), "LinkedList`1", nameof(Queue), "Queue`1", nameof(Stack), "Stack`1",
            "ICollection`1", "ICollection", "IEnumerable", "IEnumerable`1", "Enumerable",
            "IReadOnlyCollection`1", "IReadOnlyList`1", "IList`1","IList","List`1", "WhereSelectListIterator`2" };

        /// <summary>
        /// Determines whether this instance is list.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if the specified type is list; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsList(this Type type)
        {
            return
                (type.ReflectedType != null && DefinedListNames.Contains(type.ReflectedType.Name)
              || DefinedListNames.Contains(type.Name));
        }

        /// <summary>
        /// Converts the property information to property dto.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        static PropertyDTO ConvertPropertyInfoToPropertyDTO<T>(this T entity, PropertyInfo property)
        {
            var entityProperty = new PropertyDTO
            {
                Name = property.Name
            };
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
            entityProperty.IsList = entityProperty.ValueTypeOf.IsList();
            if(entityProperty.IsList)
            {
                entityProperty.Nullable = true;
            }
            if(!property.IsGenericNullable() && !entityProperty.IsPrimitive)
            {
                entityProperty.Nullable = true;
            }

            return entityProperty;
        }

        /// <summary>
        /// Determines whether [is generic nullable].
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>
        ///   <c>true</c> if [is generic nullable] [the specified property]; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsGenericNullable(this PropertyInfo property)
        {
            return property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

        /// <summary>
        /// Determines whether [is dot net pirimitive].
        /// </summary>
        /// <param name="systemType">Type of the system.</param>
        /// <param name="acceptNullables">if set to <c>true</c> [accept nullables].</param>
        /// <returns>
        ///   <c>true</c> if [is dot net pirimitive] [the specified system type]; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsDotNetPirimitive(this Type systemType, Boolean acceptNullables = true)
        {
            if (acceptNullables)
            {
                if(systemType.Name == "Nullable`1")
                {
                    var nullableSystemType = systemType.GetGenericArguments()[0];
                    return IsDotNetPirimitive(nullableSystemType, false);
                }
            }

            if (systemType == typeof(String)
                || systemType == typeof(Char)
                || systemType == typeof(Byte)
                || systemType == typeof(Int32)
                || systemType == typeof(Int64)
                || systemType == typeof(Int16)
                || systemType == typeof(float)
                || systemType == typeof(long)
                || systemType == typeof(short)
                || systemType == typeof(Double)
                || systemType == typeof(Decimal)
                || systemType == typeof(DateTime)
                || systemType == typeof(Boolean)
                || systemType == typeof(Guid)
                || systemType == typeof(Enum)
                || systemType == typeof(uint)
                || systemType == typeof(ulong)
                || systemType == typeof(ushort)
                || systemType == typeof(sbyte)
                || IsEnumOrIsBaseEnum(systemType))
            {
                

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether [is enum or is base enum] [the specified type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if [is enum or is base enum] [the specified type]; otherwise, <c>false</c>.
        /// </returns>
        static Boolean IsEnumOrIsBaseEnum(Type type)
        {
            return type.IsEnum || (type.BaseType != null && type.BaseType == typeof(Enum));
        }

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
        /// Tries to convert.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="type">The type.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static Object TryToConvert(this Object obj, Type type, Object defaultValue)
        {
            if (obj == null)
                return defaultValue;

            String nullable = "Nullable`1";

            var fullName = type.FullName;
            var name = type.Name;
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

            return result;
        }

        /// <summary>
        /// Tries the convert to given type
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Object TryToConvert(this Object obj, Type type)
        {
            Object defaultValue = null;
            if (type.IsValueType)
            {
                defaultValue  = Activator.CreateInstance(type);
            }

            return TryToConvert(obj, type, defaultValue);
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
                if (type.IsAbstract) continue;
                if (type.Name.Contains("<") || type.Name.Contains(">")) continue;
                if (type.GetInterfaces().Contains(typeof(T)) || RecursivlyCheckBaseType(type, typeof(T)))
                {
                    Type result = type;
                    if (type.ContainsGenericParameters)
                    {
                        result = type.MakeGenericType(type.GenericTypeArguments);
                    }

                    var provider = (T)Activator.CreateInstance(result, constructorArguments);

                    yield return provider;
                }
            }
        }

        /// <summary>
        /// Recursivlies the type of the check base.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="checkType">Type of the check.</param>
        /// <returns></returns>
        static Boolean RecursivlyCheckBaseType(Type type, Type checkType)
        {
            if (type != null)
            {
                if (type == checkType)
                {
                    return true;
                }
                else
                {
                    return RecursivlyCheckBaseType(type.BaseType, checkType);
                }
            }
            else
            {
                return false;
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
