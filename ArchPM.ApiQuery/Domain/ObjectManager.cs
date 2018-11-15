using ArchPM.Core.Extensions;
using Oracle.DataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.ApiQuery
{
    /// <summary>
    /// 
    /// </summary>
    public class ObjectManager
    {
        /// <summary>
        /// Fills the single list.
        /// </summary>
        /// <param name="listType">Type of the list.</param>
        /// <param name="dbCommand">The database command.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// FillSingleList
        /// </exception>
        public static Object FillSingleList(Type listType, DbCommand dbCommand)
        {
            try
            {
                if (!(listType.IsList()))
                {
                    throw new Exception($"{listType.Name} is not defined as List, must return List");
                }

                var recordType = listType.GetGenericArguments()[0];
                IList resultList = (IList)Activator.CreateInstance(listType);

                DataSet ds = new DataSet();
                OracleDataAdapter adapter = new OracleDataAdapter(dbCommand as OracleCommand);
                adapter.Fill(ds);

                //get name of the cursor name defined on the class
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //set command return value to response 

                    var record = Activator.CreateInstance(recordType); 
                    var responseProperties = record.CollectProperties(p => p.Attributes.Any(x => x is ApiQueryFieldAttribute));
                    foreach (var responseProperty in responseProperties)
                    {
                        var attr = responseProperty.Attributes.Where(p => p is ApiQueryFieldAttribute).First() as ApiQueryFieldAttribute;
                        var value = dr[attr.Name] == (Object)DBNull.Value
                            ? null
                            : dr[attr.Name].TryToConvert(responseProperty.ValueTypeOf);

                        // set value to property
                        PropertyInfo propertyInfo = record.GetType().GetProperty(responseProperty.Name);
                        propertyInfo.SetValue(record, value, null);
                    }
                    resultList.Add(record);
                }

                return resultList;

            }
            catch (Exception ex)
            {
                throw new Exception($"[{ nameof(FillSingleList) }] {MethodBase.GetCurrentMethod().Name} failed!", ex);
            }
        }

        /// <summary>
        /// Fills the object.
        /// </summary>
        /// <param name="resultType">Type of the result.</param>
        /// <param name="dbCommand">The database command.</param>
        /// <returns></returns>
        /// <exception cref="Exception">FillObject</exception>
        public static Object FillObject(Type resultType, DbCommand dbCommand)
        {
            try
            {
                var result = Activator.CreateInstance(resultType);

                //collect only properties having ApiQueryFieldAttribute
                var responseProperties = result.CollectProperties(p => p.Attributes.Any(x => x is ApiQueryFieldAttribute));
                foreach (var responseProperty in responseProperties)
                {
                    //can be a single ApiQueryFieldAttribute attribute so we can use First method
                    var attr = responseProperty.Attributes.Where(p => p is ApiQueryFieldAttribute).First() as ApiQueryFieldAttribute;

                    //when property is primitive - int, string etc.
                    if (responseProperty.IsPrimitive)
                    {
                        //collect value from dbcommand handles null values and if not returns default value of given type
                        var val = dbCommand.Parameters[attr.Name].Value;
                        var value = val == (Object)DBNull.Value || val.ToString() == "null"
                            ? null
                            : val.ToString().TryToConvert(responseProperty.ValueTypeOf);

                        // set value to property
                        PropertyInfo propertyInfo = result.GetType().GetProperty(responseProperty.Name);
                        propertyInfo.SetValue(result, value, null);
                    }
                    else //when property is complex
                    {
                        //when property is list
                        if (responseProperty.ValueTypeOf.IsList())
                        {
                            //define list type
                            var listType = responseProperty.ValueTypeOf;

                            var data = ObjectManager.FillSingleList(listType, dbCommand);
                            // set value to property
                            PropertyInfo propertyInfo = result.GetType().GetProperty(responseProperty.Name);
                            propertyInfo.SetValue(result, Convert.ChangeType(data, propertyInfo.PropertyType), null);
                        }
                        else //when property is class
                        {
                            var objectType = responseProperty.ValueTypeOf;

                            var data = ObjectManager.FillClassPropertyInClass(objectType, dbCommand);

                            // set value to property
                            PropertyInfo propertyInfo = result.GetType().GetProperty(responseProperty.Name);
                            propertyInfo.SetValue(result, Convert.ChangeType(data, propertyInfo.PropertyType), null);
                        }
                    }


                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"[{ nameof(FillObject) }] {MethodBase.GetCurrentMethod().Name} failed!", ex);
            }
        }

        /// <summary>
        /// Returns the value.
        /// </summary>
        /// <param name="resultType">Type of the result.</param>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="returnValueName">Name of the return value.</param>
        /// <returns></returns>
        /// <exception cref="Exception">FillObject</exception>
        public static Object ReturnValue(Type resultType, DbCommand dbCommand, String returnValueName)
        {
            try
            {

                var result = Activator.CreateInstance(resultType);

                if (resultType.IsPrimitive)
                {
                    var parameterName = ApiQueryUtils.GetProcedureName(returnValueName);

                    //collect value from dbcommand handles null values and if not returns default value of given type
                    var val = dbCommand.Parameters[parameterName].Value;
                    var value = val == (Object)DBNull.Value || val.ToString() == "null"
                        ? null
                        : val.ToString().TryToConvert(resultType);

                    result = value;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"[{ nameof(FillObject) }] {MethodBase.GetCurrentMethod().Name} failed!", ex);
            }
        }



        /// <summary>
        /// Fill class property in class
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="dbCommand">The database command.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// FillClassPropertyInClass
        /// </exception>
        internal static Object FillClassPropertyInClass(Type objectType, DbCommand dbCommand)
        {
            try
            {
                if (!objectType.IsClass)
                {
                    throw new Exception($"{objectType.Name} must be a class!");
                }

                var record = Activator.CreateInstance(objectType);

                DataSet ds = new DataSet();
                OracleDataAdapter adapter = new OracleDataAdapter(dbCommand as OracleCommand);
                adapter.Fill(ds);

                //get name of the cursor name defined on the class
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //set command return value to response 
                    var responseProperties = record.CollectProperties(p => p.Attributes.Any(x => x is ApiQueryFieldAttribute));
                    foreach (var responseProperty in responseProperties)
                    {
                        var attr = responseProperty.Attributes.Where(p => p is ApiQueryFieldAttribute).First() as ApiQueryFieldAttribute;

                        var value = dr[attr.Name] == (Object)DBNull.Value
                            ? null
                            : dr[attr.Name].TryToConvert(responseProperty.ValueTypeOf);

                        // set value to property
                        PropertyInfo propertyInfo = record.GetType().GetProperty(responseProperty.Name);
                        propertyInfo.SetValue(record, value, null);
                    }
                }

                return record;
            }
            catch (Exception ex)
            {
                throw new Exception($"[{ nameof(FillClassPropertyInClass) }] {MethodBase.GetCurrentMethod().Name} failed!", ex);
            }
        }

        /// <summary>
        /// Fills the values in class.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="dbCommand">The database command.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// FillValuesInClass
        /// </exception>
        internal static Object FillValuesInClass(Type objectType, DbCommand dbCommand)
        {
            try
            {
                if (!objectType.IsClass)
                {
                    throw new Exception($"{objectType.Name} must be a class!");
                }

                //set command return value to response 
                var record = Activator.CreateInstance(objectType);
                var responseProperties = record.CollectProperties(p => p.Attributes.Any(x => x.GetType() == typeof(ApiQueryFieldAttribute)));
                foreach (var responseProperty in responseProperties)
                {
                    var attr = responseProperty.Attributes.Where(p => p is ApiQueryFieldAttribute).First() as ApiQueryFieldAttribute;
                    var val = dbCommand.Parameters[attr.Name].Value;
                    var value = val == (Object)DBNull.Value
                        ? null
                        : val.ToString().TryToConvert(responseProperty.ValueTypeOf);

                    // set value to property
                    PropertyInfo propertyInfo = record.GetType().GetProperty(responseProperty.Name);
                    //propertyInfo.SetValue(res, Convert.ChangeType(value, propertyInfo.PropertyType), null);
                    propertyInfo.SetValue(record, value, null);
                }

                return record;
            }
            catch (Exception ex)
            {
                throw new Exception($"[{ nameof(FillValuesInClass) }] {MethodBase.GetCurrentMethod().Name} failed!", ex);
            }
        }

    }
}
