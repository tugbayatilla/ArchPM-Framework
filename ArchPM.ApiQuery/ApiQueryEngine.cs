using ArchPM.Core;
using ArchPM.Core.Api;
using ArchPM.Core.Enums;
using ArchPM.Core.Exceptions;
using ArchPM.Core.Extensions;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.ApiQuery
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Req">The type of the eq.</typeparam>
    /// <typeparam name="Res">The type of the es.</typeparam>
    public class ApiQueryEngine<Req, Res>
        where Req : ApiQueryRequest
        where Res : new()
    {
        /// <summary>
        /// The database provider
        /// </summary>
        readonly IApiQueryDatabaseProvider databaseProvider;
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiQueryEngine{Req, Res}"/> class.
        /// </summary>
        /// <param name="databaseProvider">The database provider.</param>
        public ApiQueryEngine(IApiQueryDatabaseProvider databaseProvider)
        {
            this.databaseProvider = databaseProvider;
        }

        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<ApiResponse<Res>> Execute(Req request)
        {
            return await Task<ApiResponse<Res>>.Factory.StartNew(() =>
            {
                Stopwatch sw = new Stopwatch();
                ApiResponse<Res> result = null;
                try
                {
                    sw.Start();

                    request.ThrowExceptionIfNull($"Request '{typeof(Req).Name}' cannot be null");
                    //validate request object
                    request.Validate();


                    SetResponseTypeToRequestIfNotDefined(request);

                    using (DbConnection connection = databaseProvider.CreateConnection())
                    {
                        connection.ConnectionString = databaseProvider.ConnectionString;
                        connection.Open();
                        using (DbTransaction transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                using (DbCommand command = databaseProvider.GenerateCommand())
                                {
                                    command.Connection = connection;
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.CommandText = request.ProcedureName;
                                    command.Transaction = transaction;

                                    //command paramaters
                                    var commandParameters = CreateCommandParameters(request).ToArray();
                                    command.Parameters.AddRange(commandParameters);

                                    //execute command
                                    command.ExecuteNonQuery();

                                    Object data = null;
                                    var responseType = typeof(Res);
                                    switch (request.ResponseType)
                                    {
                                        case QueryResponseTypes.AsValue:
                                            data = ObjectManager.ReturnValue(responseType, command, request.ProcedureName);
                                            break;
                                        case QueryResponseTypes.AsObject:
                                            data = ObjectManager.FillObject(responseType, command);
                                            break;
                                        case QueryResponseTypes.AsList:
                                            data = ObjectManager.FillSingleList(responseType, command);
                                            break;
                                        default:
                                            throw new Exception($"Unknown ResponseType: {request.ResponseType}");
                                    }

                                    //commit transaction
                                    transaction.Commit();
                                    result = ApiResponse<Res>.CreateSuccess((Res)data);

                                }
                            }
                            catch (Exception ex1)
                            {
                                transaction.Rollback();
                                throw ex1;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    result = ApiResponse<Res>.CreateFail(ex);
                }
                finally
                {
                    sw.Stop();
                    result.ET = sw.ElapsedMilliseconds;
                    sw = null;
                }

                return result;
            });
        }

        /// <summary>
        /// Sets the response type to request if not defined.
        /// </summary>
        /// <param name="request">The request.</param>
        internal void SetResponseTypeToRequestIfNotDefined(Req request)
        {
            //set request response type if not exist
            if (!request.ResponseType.HasValue)
            {
                if (typeof(Res).IsList())
                {
                    request.ResponseType = QueryResponseTypes.AsList;
                }
                else if (typeof(Res).IsPrimitive)
                {
                    request.ResponseType = QueryResponseTypes.AsValue;
                }
                else
                {
                    request.ResponseType = QueryResponseTypes.AsObject;
                }
            }
        }

        /// <summary>
        /// Creates the command parameters.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <exception cref="Exception">CreateCommandParameters</exception>
        internal List<OracleParameter> CreateCommandParameters(Req request)
        {
            var result = new List<OracleParameter>();

            try
            {
                Res response = new Res();
                var inputParameters = request.PropertiesAll(p => p.Attributes.Any(x => x is ApiQueryFieldAttribute));
                var outputParameters = response.PropertiesAll(p => p.Attributes.Any(x => x is ApiQueryFieldAttribute));

                foreach (var prm in inputParameters)
                {
                    AddParameterToResultList(result, prm);
                }

                if (request.ResponseType == QueryResponseTypes.AsObject)
                {
                    foreach (var prm in outputParameters)
                    {
                        AddParameterToResultList(result, prm);
                    }
                }

                if (request.ResponseType == QueryResponseTypes.AsList)
                {
                    var resAttribute = ApiQueryUtils.GetApiQueryFieldAttributeOnClass<Res>(); //list??
                    resAttribute.ThrowExceptionIfNull($"{nameof(OutputApiQueryFieldAttribute)} must be used on {typeof(Res).GetGenericArguments()[0].Name}!");
                    var oracleParameter = new OracleParameter
                    {
                        ParameterName = resAttribute.Name,
                        OracleDbType = OracleDbType.RefCursor,
                        Direction = (ParameterDirection)resAttribute.Direction,
                        Size = resAttribute.Size ?? 0
                    };

                    result.Add(oracleParameter);
                }

                //single value return
                //must be first: retValue.ParameterName must be prodecure or function name and be the first parameters
                if (request.ResponseType == QueryResponseTypes.AsValue)
                {
                    var responseType = typeof(Res);
                    var oracleParameter = new OracleParameter
                    {
                        ParameterName = ApiQueryUtils.GetProcedureName(request.ProcedureName),
                        OracleDbType = ApiQueryUtils.ConvertFromSystemTypeToOracleDbType(responseType.Name),
                        Direction = ParameterDirection.ReturnValue,
                    };
                    result.Insert(0, oracleParameter);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed at {nameof(CreateCommandParameters)}!", ex);
            }

            return result;
        }

        /// <summary>
        /// Adds the parameter to result list.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="prm">The PRM.</param>
        internal void AddParameterToResultList(List<OracleParameter> result, PropertyDTO prm)
        {
            //already filtered and can be only one queryFieldAttribute
            var attr = prm.Attributes.Where(p => p is ApiQueryFieldAttribute).First() as ApiQueryFieldAttribute;

            var dbType = OracleDbType.Varchar2;
            if (!attr.DbType.HasValue)
            {
                if (prm.IsPrimitive)
                {
                    dbType = ApiQueryUtils.ConvertFromSystemTypeToOracleDbType(prm.ValueType);
                }
                else
                {
                    dbType = OracleDbType.RefCursor;
                }
            }

            var oracleParameter = new OracleParameter
            {
                ParameterName = attr.Name,
                OracleDbType = dbType,
                Direction = (ParameterDirection)attr.Direction,
                Value = prm.Value ?? (Object)DBNull.Value
            };

            oracleParameter.IsNullable = prm.Nullable;

            if (prm.ValueTypeOf == typeof(String))
            {
                if (prm.Value != null)
                    oracleParameter.Size = ((String)prm.Value).Length;
                else
                    oracleParameter.Size = attr.Size ?? 4000; //oracle max varchar2 lenght
            }

            result.Add(oracleParameter);
        }

    }
}
