using System;
using System.Collections.Generic;
using ArchPM.Core.Exceptions;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ArchPM.Core.Extensions.Advanced
{
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectExtension
    {
        ///// <summary>
        ///// Tests if a object is null.
        ///// </summary>
        ///// <typeparam name="T">class inherited from Exception</typeparam>
        ///// <param name="theObj">Object class</param>
        ///// <param name="msg">the message which is going to get</param>
        ///// <param name="args">The arguments.</param>
        //public static void NotNull<T>(this Object theObj, string msg = "", params object[] args) where T : Exception, new()
        //{
        //    if (theObj == null)
        //    {
        //        if (string.IsNullOrEmpty(msg))
        //        {
        //            msg = "A object instance can't be null";
        //        }

        //        var ex = (T)Activator.CreateInstance(typeof(T), String.Format(msg, args));

        //        throw ex;

        //    }
        //}

        ///// <summary>
        ///// Tests if a object is null.
        ///// </summary>
        ///// <typeparam name="T">class inherited from Exception</typeparam>
        ///// <param name="theObj">Object class</param>
        ///// <param name="msg">the message which is going to get</param>
        ///// <param name="args">The arguments.</param>
        //public static void NotNull(this Object theObj, string msg = "", params object[] args)
        //{
        //    NotNull<ArgumentNullException>(theObj, msg, args);
        //}

        ///// <summary>
        ///// Tests if provided array of objects doesnt contain null values.
        ///// Throws ValidationException if it has null value.
        ///// </summary>
        ///// <param name="objects">array of objects</param>
        //public static void NoNullElements(this object[] objects)
        //{
        //    foreach (object obj in objects)
        //    {
        //        NotNull(obj);
        //    }
        //}

        /// <summary>
        /// Convert a byte array to an Object
        /// </summary>
        /// <param name="arrBytes">The arr bytes.</param>
        /// <returns></returns>
        public static Object ToObject(this Byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);

            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Boolean IsEnumOrIsBaseEnum(this Type type)
        {
            return type.IsEnum || (type.BaseType != null && type.BaseType == typeof(Enum));
        }

        /// <summary>
        /// Tests if provided bool parameter is true.
        /// Throws ValidationException with passed message if value is false.
        /// </summary>
        /// <param name="isTrue">bool value</param>
        /// <param name="msg">message to throw if the object is null</param>
        /// <param name="args">The arguments.</param>
        /// <exception cref="ArchPM.Core.Exceptions.ValidationException"></exception>
        public static void IsTrue(this bool isTrue, string msg, params object[] args)
        {
            if (!isTrue)
            {
                throw new ValidationException(String.Format(msg, args));
            }
        }

        /// <summary>
        /// Determines whether the specified expression is numeric.
        /// </summary>
        /// <param name="Expression">The expression.</param>
        /// <returns></returns>
        public static Boolean IsNumeric(this Object Expression)
        {
            double retNum;

            bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        /// <summary>
        /// Tries the convert to given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Expression">The expression.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static T TryConvertTo<T>(this Object Expression, T defaultValue)
        {
            if (Expression == null)
                return defaultValue;

            String nullable = "Nullable`1";

            var fullName = typeof(T).FullName;
            var name = typeof(T).Name;
            Object result = defaultValue;

            try
            {
                if (name == "Decimal" || (name == nullable && fullName.Contains("System.Decimal")))
                {
                    if (IsNumeric(Expression))
                    {
                        result = Convert.ToDecimal(Expression.ToString());
                    }
                }
                else if (name == "Double" || (name == nullable && fullName.Contains("System.Decimal")))
                {
                    if (IsNumeric(Expression))
                    {
                        result = Convert.ToDouble(Expression.ToString());
                    }
                }
                else if (name == "Int16" || (name == nullable && fullName.Contains("System.Int16")))
                {
                    if (IsNumeric(Expression))
                    {
                        result = Convert.ToInt16(Expression.ToString());
                    }
                }
                else if (name == "Int32" || (name == nullable && fullName.Contains("System.Int32")))
                {
                    if (IsNumeric(Expression))
                    {
                        result = Convert.ToInt32(Expression.ToString());
                    }
                }
                else if (name == "Int64" || (name == nullable && fullName.Contains("System.Int64")))
                {
                    if (IsNumeric(Expression))
                    {
                        result = Convert.ToInt64(Expression.ToString());
                    }
                }
                else if (name == "UInt16" || (name == nullable && fullName.Contains("System.UInt16")))
                {
                    if (IsNumeric(Expression))
                    {
                        result = Convert.ToUInt16(Expression.ToString());
                    }
                }
                else if (name == "UInt32" || (name == nullable && fullName.Contains("System.UInt32")))
                {
                    if (IsNumeric(Expression))
                    {
                        result = Convert.ToUInt32(Expression.ToString());
                    }
                }
                else if (name == "UInt64" || (name == nullable && fullName.Contains("System.UInt64")))
                {
                    if (IsNumeric(Expression))
                    {
                        result = Convert.ToUInt64(Expression.ToString());
                    }
                }
                else if (name == "Single" || (name == nullable && fullName.Contains("System.Single")))
                {
                    if (IsNumeric(Expression))
                    {
                        result = Convert.ToSingle(Expression.ToString());
                    }
                }
                else if (name == "Byte" || (name == nullable && fullName.Contains("System.Byte")))
                {
                    Byte temp;
                    Byte.TryParse(Expression.ToString(), out temp);
                    result = temp;
                }
                else if (name == "DateTime" || (name == nullable && fullName.Contains("System.DateTime")))
                {
                    DateTime temp;
                    DateTime.TryParse(Expression.ToString(), out temp);
                    result = temp;
                }
                else if (name == "Boolean" || (name == nullable && fullName.Contains("System.Boolean")))
                {
                    Boolean temp;
                    Boolean.TryParse(Expression.ToString(), out temp);
                    result = temp;
                }
                else if (name == "String" || (name == nullable && fullName.Contains("System.String")))
                {
                    String temp = Convert.ToString(Expression);
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
        /// Convert an object to a byte array
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static byte[] ToByteArray(this Object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }


    }
}
