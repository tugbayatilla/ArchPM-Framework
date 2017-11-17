using System;
using System.Collections.Generic;
using System.Reflection;

using System.Linq;

namespace ArchPM.Core.Extensions.Advanced
{
    /// <summary>
    /// 
    /// </summary>
    public static class ReflectionExtensions
    {

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="entity"></param>
        ///// <param name="attributes"></param>
        ///// <returns></returns>
        //public static IEnumerable<PropertyInfo> GetPropertyInfosByAttributes<T>(this T entity, params Type[] attributeTypes)
        //{
        //    foreach (var property in entity.GetType().GetProperties())
        //    {
        //        if (property.GetCustomAttributes().Any(p => attributeTypes.Contains(p.GetType())))
        //            yield return property;
        //    }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="entity"></param>
        ///// <param name="attributeTypes"></param>
        ///// <returns></returns>
        //public static IEnumerable<PropertyDTO> GetPropertiesByAttributes<T>(this T entity, params Type[] attributeTypes)
        //{
        //    foreach (var property in entity.GetType().GetProperties())
        //    {
        //        if (property.GetCustomAttributes().Any(p => attributeTypes.Contains(p.GetType())))
        //            yield return ConvertPropertyInfoToPropertyDTO(entity, property);
        //    }
        //}

       

       

        ///// <summary>
        ///// Convert Enum Value to Int32 Value
        ///// </summary>
        ///// <param name="property">The property.</param>
        //public static void ConvertEnumValueToInt32OnPropertyValue(this PropertyDTO property)
        //{
        //    //var value = property.Value == null ? DBNull.Value : property.Value;
        //    var value = property.Value;
        //    if (value != null && property.ValueTypeOf.IsEnum)
        //    {
        //        value = Convert.ToInt32(property.Value);
        //    }

        //    property.Value = value;
        //}
    }
}
