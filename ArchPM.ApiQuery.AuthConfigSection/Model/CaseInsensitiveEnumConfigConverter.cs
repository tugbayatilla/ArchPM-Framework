using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.ApiQuery.AuthConfigSection.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Configuration.ConfigurationConverterBase" />
    public class CaseInsensitiveEnumConfigConverter<T> : ConfigurationConverterBase
    {
        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        /// <param name="ci">The ci.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public override object ConvertFrom(
        ITypeDescriptorContext ctx, CultureInfo ci, object data)
        {
            return Enum.Parse(typeof(T), (string)data, true);
        }
    }
}
