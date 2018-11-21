using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.ApiQuery.AuthConfigSection.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Configuration.ConfigurationElementCollection" />
    public class EnvironmentInfoElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </summary>
        /// <returns>
        /// A newly created <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new EnvironmentInfoElement();
        }

        /// <summary>
        /// Gets the element key for a specified configuration element when overridden in a derived class.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to return the key for.</param>
        /// <returns>
        /// An <see cref="T:System.Object" /> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            var env = ((EnvironmentInfoElement)element);
            return $"{env.Env}_{env.Application}";
        }

        /// <summary>
        /// Gets or sets the <see cref="ArchPM.ApiQuery.AuthConfigSection.Model.EnvironmentInfoElement" /> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="ArchPM.ApiQuery.AuthConfigSection.Model.EnvironmentInfoElement" />.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public EnvironmentInfoElement this[int index]
        {
            get
            {
                return (EnvironmentInfoElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        /// <summary>
        /// Gets the <see cref="EnvironmentInfoElement"/> with the specified name.
        /// </summary>
        /// <value>
        /// The <see cref="EnvironmentInfoElement"/>.
        /// </value>
        /// <param name="Name">The name.</param>
        /// <returns></returns>
        new public EnvironmentInfoElement this[string Name]
        {
            get
            {
                return (EnvironmentInfoElement)BaseGet(Name);
            }
        }
    }

}
