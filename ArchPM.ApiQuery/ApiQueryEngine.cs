using ArchPM.Core;
using ArchPM.Core.Api;
using ArchPM.Core.Enums;
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
    public class ApiQueryEngine<Req, Res>
        where Req : ApiQueryRequest
        where Res : new()
    {
        readonly IApiQueryDatabaseProvider databaseProvider;
        public ApiQueryEngine(IApiQueryDatabaseProvider databaseProvider)
        {
            this.databaseProvider = databaseProvider;
        }

        public async Task<IApiResponse<Res>> Execute(Req request)
        {
            return await Task<IApiResponse<Res>>.Factory.StartNew(() =>
            {
                Stopwatch sw = new Stopwatch();
                IApiResponse<Res> result = null;
                try
                {
                    sw.Start();
                    //validate request object
                    request.Validate();

                    SetResponseTypeToRequestIfNotDefined(request);

                    using (DbConnection connection = databaseProvider.CreateConnection())
                    {
                        connection.ConnectionString = databaseProvider.ConnectionString;
                        connection.Open();

                        using (DbCommand command = databaseProvider.GenerateCommand())
                        {
                            command.Connection = connection;
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = request.ProcedureName;

                            //command paramaters
                            var commandParameters = CreateCommandParameters(request).ToArray();
                            command.Parameters.AddRange(commandParameters);

                            Object data = null;
                            var responseType = typeof(Res);
                            switch (request.ResponseType)
                            {
                                case QueryResponseTypes.AsValue:
                                    throw new NotSupportedException("Next version, it is coming...");
                                case QueryResponseTypes.AsObject:
                                    command.ExecuteNonQuery();
                                    data = ObjectManager.FillObject(responseType, command);
                                    break;
                                case QueryResponseTypes.AsList:
                                    command.ExecuteNonQuery();
                                    data = ObjectManager.FillSingleList(responseType, command);
                                    break;
                                default:
                                    throw new Exception($"Unknown ResponseType: {request.ResponseType}");
                            }

                            result = ApiResponse<Res>.CreateSuccessResponse((Res)data);

                        }
                    }

                }
                catch (Exception ex)
                {
                    result = ApiResponse<Res>.CreateException(ex);
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

        void SetResponseTypeToRequestIfNotDefined(Req request)
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

        public List<OracleParameter> CreateCommandParameters(Req request)
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
                    var resAttribute = Utils.GetApiQueryFieldAttributeOnClass<Res>(); //list??
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
                //retValue.ParameterName must be prodecure or function name and be the first parameters
                if (request.ResponseType == QueryResponseTypes.AsValue)
                {
                    var retValue = result.Cast<OracleParameter>().FirstOrDefault(p => p.Direction == ParameterDirection.ReturnValue);
                    if (retValue != null)
                    {
                        result.Remove(retValue);
                        result.Insert(0, retValue);//must be first 
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at CreateCommandParameters!", ex);
            }

            return result;
        }

        void AddParameterToResultList(List<OracleParameter> result, PropertyDTO prm)
        {
            //already filtered and can be only one queryFieldAttribute
            var attr = prm.Attributes.First() as ApiQueryFieldAttribute;

            var dbType = OracleDbType.Varchar2;
            if (!attr.DbType.HasValue)
            {
                if (prm.IsPrimitive)
                {
                    dbType = Utils.ConvertFromSystemTypeToOracleDbType(prm.ValueType);
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
