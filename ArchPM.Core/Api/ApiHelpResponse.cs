using ArchPM.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.Core.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiHelpResponse
    {
        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        public String Service { get; set; }
        /// <summary>
        /// Gets or sets the actions.
        /// </summary>
        /// <value>
        /// The actions.
        /// </value>
        public List<ApiHelpAction> Actions { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="ApiHelpResponse"/> class.
        /// </summary>
        public ApiHelpResponse()
        {
            Actions = new List<ApiHelpAction>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiHelpResponse"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public ApiHelpResponse(Type type) : this()
        {
            Init(type);
        }

        /// <summary>
        /// Initializes the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        public void Init(Type type)
        {
            type.ThrowExceptionIfNull("Type must have been initialized!");

            ApiHelpResponse response = this;
            Service = type.Name;

            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance).ToList();
            var typeAttribute = type.GetCustomAttribute(typeof(ApiHelpAttribute));

            //when apiHelp defined on class, get all methods
            //exclude system methods
            if (typeAttribute != null)
            {
                methods = methods.Where(p => p.Module.Name != "mscorlib.dll").ToList();
            }
            else
            {
                //collect only methods having ApiHelpAttribute
                methods = methods.Where(p => p.GetCustomAttributes(typeof(ApiHelpAttribute)).Any()).ToList();
            }

            foreach (var method in methods)
            {
                var action = new ApiHelpAction { Name = method.Name };

                if (method.GetCustomAttributes().FirstOrDefault(p => p is ApiHelpAttribute) is ApiHelpAttribute attr)
                {
                    action.Comment = attr.Comment;
                }

                //input parameters
                foreach (var inParameter in method.GetParameters())
                {
                    Fill(inParameter, action.InputParameters);
                }

                //output parameters
                List<ApiHelpParameter> outargs = new List<ApiHelpParameter>();
                Fill(method.ReturnParameter, outargs);
                action.OutputParameter = outargs.FirstOrDefault();
                //add into the response
                response.Actions.Add(action);
            }
        }

        /// <summary>
        /// Fills the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="args">The parameters.</param>
        internal void Fill(ParameterInfo parameter, List<ApiHelpParameter> args)
        {
            if (parameter == null)
                return;

            var prm = new ApiHelpParameter();
            Type prmType = parameter.ParameterType;
            prmType = SkipTaskType(prmType);

            if (prmType.Name == "Void")
            {
                prm.Name = "Void";
            }

            if (prmType.IsGenericType)
            {
                prm.Name = prmType.Name;
                prm.Type = $"{prmType.Name.Replace("`1", "")}<{prmType.GetGenericArguments()[0].Name}>";
            }

            //value type
            if (prmType.IsDotNetPirimitive())
            {
                prm.Name = parameter.Name;
                prm.Type = prmType.Name;

                //if nullable
                if (prmType.GetGenericArguments().Count() > 0)
                {
                    prm.Type = prmType.GetGenericArguments()[0].Name;
                    //if not string, all nullable valuetypes have ?
                    if (prmType != typeof(String))
                    {
                        prm.Type += "?";
                    }
                }
            }

            //when simple class
            if (!prmType.IsDotNetPirimitive() && !prmType.IsList())
            {
                prm.Name = parameter.Name ?? prmType.Name;
                prm.Type = prmType.Name;
            }

            //when having ApiHelp Attribute
            if (prmType.GetCustomAttributes().FirstOrDefault(p => p is ApiHelpAttribute) is ApiHelpAttribute attr)
            {
                prm.Comment = attr.Comment;
            }
            args.Add(prm);

            //when it is class
            if (!prmType.IsDotNetPirimitive())
            {
                IEnumerable<PropertyDTO> properties = GetPropertyDTOList(prmType);
                foreach (var property in properties)
                {
                    FillRecursively(prm, property);
                }
            }

        }

        IEnumerable<PropertyDTO> GetPropertyDTOList(Type prmType)
        {
            IEnumerable<PropertyDTO> properties = null;
            //when class has ApiHelp, Collect all properties
            if (prmType.GetCustomAttribute(typeof(ApiHelpAttribute)) != null)
            {
                properties = prmType.CollectProperties();
            }
            else
            {
                //collect only properties having ApiHelpAttribute 
                properties = prmType.CollectProperties(p => p.Attributes.Any(x => x is ApiHelpAttribute));
            }

            return properties;
        }

        /// <summary>
        /// Fills the recursively.
        /// </summary>
        /// <param name="prm">The PRM.</param>
        /// <param name="propertyDTO">The property dto.</param>
        internal void FillRecursively(ApiHelpParameter prm, PropertyDTO propertyDTO)
        {
            if (prm.Parameters == null)
            {
                prm.Parameters = new List<ApiHelpParameter>();
            }

            var childPrm = new ApiHelpParameter()
            {
                Name = propertyDTO.Name,
                Type = propertyDTO.ValueType,

            };

            if (propertyDTO.IsPrimitive && propertyDTO.Nullable && propertyDTO.ValueTypeOf != typeof(String))
            {
                childPrm.Type += "?";
            }

            if (propertyDTO.IsList)
            {
                childPrm.Type = $"{propertyDTO.ValueType.Replace("`1", "")}<{propertyDTO.ValueTypeOf.GetGenericArguments()[0].Name}>";
            }
            if (propertyDTO.Attributes.FirstOrDefault(p => p is ApiHelpAttribute) is ApiHelpAttribute attr)
            {
                childPrm.Comment = attr.Comment;
            }
            prm.Parameters.Add(childPrm);

            //recursive same object prevention
            if (prm.Type == childPrm.Type)
                return;

            if (!propertyDTO.IsPrimitive)
            {
                var type = propertyDTO.ValueTypeOf;
                if (propertyDTO.ValueTypeOf.IsList())
                {
                    type = propertyDTO.ValueTypeOf.GetGenericArguments()[0];
                }
                IEnumerable<PropertyDTO> properties = GetPropertyDTOList(type);
                foreach (var dto in properties)
                {
                    FillRecursively(childPrm, dto);
                }
            }
        }

        /// <summary>
        /// Gets the type of the non generic.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        Type SkipTaskType(Type type)
        {
            if (type.Name == "Task`1")
            {
                return SkipTaskType(type.GetGenericArguments()[0]);
            }
            else
            {
                return type;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{nameof(ApiHelpResponse)}=>[{this.Service}]";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ApiHelpAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiHelpAction"/> class.
        /// </summary>
        public ApiHelpAction()
        {
            this.InputParameters = new List<ApiHelpParameter>();
            this.OutputParameter = new ApiHelpParameter();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public String Name { get; set; }
        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public String Comment { get; set; }
        /// <summary>
        /// Gets or sets the input parameters.
        /// </summary>
        /// <value>
        /// The input parameters.
        /// </value>
        public List<ApiHelpParameter> InputParameters { get; set; }
        /// <summary>
        /// Gets or sets the output parameters.
        /// </summary>
        /// <value>
        /// The output parameters.
        /// </value>
        public ApiHelpParameter OutputParameter { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{nameof(ApiHelpAction)}=>[{this.Name}]";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ApiHelpParameter
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public String Type { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public String Comment { get; set; }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public List<ApiHelpParameter> Parameters { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{nameof(ApiHelpParameter)}=>[{this.Name}:{this.Type}]";
        }
    }
}
