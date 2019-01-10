using ArchPM.Core;
using ArchPM.Core.Exceptions;
using ArchPM.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.ApiQuery
{
    /// <summary>
    /// Api Query Request Base Class
    /// </summary>
    public abstract class ApiQueryRequest
    {
        /// <summary>
        /// Gets the name of the procedure.
        /// </summary>
        /// <value>
        /// The name of the procedure.
        /// </value>
        public abstract String ProcedureName { get; }
        /// <summary>
        /// Gets or sets the type of the response.
        /// </summary>
        /// <value>
        /// The type of the response.
        /// </value>
        public virtual QueryResponseTypes? ResponseType { get; set; }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <exception cref="ValidationException"></exception>
        public virtual void Validate()
        {
            var properties = this.CollectProperties(p => p.Attributes.Any(x => x is ApiQueryFieldThrowExceptionWhenAttribute));

            foreach (var property in properties)
            {
                var validationAttributes = property.Attributes
                    .Where(p => p is ApiQueryFieldThrowExceptionWhenAttribute)
                    .Cast<ApiQueryFieldThrowExceptionWhenAttribute>();


                foreach (var attr in validationAttributes)
                {
                    ParameterExpression paramExp = Expression.Parameter(property.ValueTypeOf, property.Name);
                    Expression realParamExp = paramExp;

                    ConstantExpression valueExp = Expression.Constant(attr.Value);
                    //property definition
                    if (!String.IsNullOrEmpty(attr.On))
                    {
                        //if value is null, you can't change value's property so we need to continue
                        if(property.Value == null)
                        {
                            continue;
                            //throw new ValidationException($"{property.Name} must have a value before checking {attr.On}!");
                        }

                        realParamExp = Expression.Property(paramExp, attr.On);
                    }

                    //EqualTo
                    if (attr.Operator.HasFlag(ApiQueryFieldOperators.EqualTo) 
                        && !attr.Operator.Has(ApiQueryFieldOperators.GreaterThan)
                        && !attr.Operator.Has(ApiQueryFieldOperators.LessThan)) // =
                    {
                        realParamExp = Expression.Equal(realParamExp, valueExp);
                    }

                    //GreaterThan and Equal To
                    if (attr.Operator.Has(ApiQueryFieldOperators.GreaterThan) 
                        && attr.Operator.Has(ApiQueryFieldOperators.EqualTo)) 
                    {
                        realParamExp = Expression.GreaterThanOrEqual(realParamExp, valueExp);
                    }

                    //only greater than >
                    if (attr.Operator.Has(ApiQueryFieldOperators.GreaterThan)
                        && !attr.Operator.Has(ApiQueryFieldOperators.EqualTo)
                        && !attr.Operator.Has(ApiQueryFieldOperators.LessThan)) 
                    {
                        realParamExp = Expression.GreaterThan(realParamExp, valueExp);
                    }

                    //only less than <
                    if (attr.Operator.Has(ApiQueryFieldOperators.LessThan) 
                        && !attr.Operator.Has(ApiQueryFieldOperators.EqualTo)
                        && !attr.Operator.Has(ApiQueryFieldOperators.GreaterThan)) 
                    {
                        realParamExp = Expression.LessThan(realParamExp, valueExp);
                    }

                    //LessThan and EqualTo
                    if (attr.Operator.Has(ApiQueryFieldOperators.LessThan) 
                        && attr.Operator.Has(ApiQueryFieldOperators.EqualTo)) 
                    {
                        realParamExp = Expression.LessThanOrEqual(realParamExp, valueExp);
                    }

                    paramExp.ThrowExceptionIfNull($"Use at least one of, {nameof(ApiQueryFieldOperators.EqualTo)}, {nameof(ApiQueryFieldOperators.LessThan)}, {nameof(ApiQueryFieldOperators.GreaterThan)}");

                    //Not
                    if (attr.Operator.Has(ApiQueryFieldOperators.Not)) // !
                    {
                        realParamExp = Expression.Not(realParamExp);
                    }

                    //var result = Expression.Lambda<Func<Object, bool>>(realParamExp, new ParameterExpression[] { paramExp }).Compile();
                    var resultLambda = Expression.Lambda(realParamExp, new ParameterExpression[] { paramExp }).Compile();
                    Boolean result = (Boolean)resultLambda.DynamicInvoke(property.Value);

                    if (result)
                    {
                        if(String.IsNullOrEmpty(attr.ErrorMesssage))
                        {
                            attr.ErrorMesssage = $"Failed because of " + Expression.Lambda(realParamExp).Body.ToString();
                        }

                        throw new ValidationException(attr.ErrorMesssage);
                    }
                }

            }

        }

        /// <summary>
        /// Executes just before Validate method.
        /// </summary>
        public virtual void PreValidate()
        {
        }

    }
}
