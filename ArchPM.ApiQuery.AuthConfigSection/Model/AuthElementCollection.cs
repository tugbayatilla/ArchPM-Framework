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
    public class AuthElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </summary>
        /// <returns>
        /// A newly created <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new AuthElement();
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
            var auth = ((AuthElement)element);
            return $"{auth.Env}_{auth.Application}_{auth.Service}_{auth.Action}_{auth.Username}";
        }

        /// <summary>
        /// Gets or sets the <see cref="AuthElement"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="AuthElement"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public AuthElement this[int index]
        {
            get
            {
                return (AuthElement)BaseGet(index);
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
        /// Gets the <see cref="AuthElement"/> with the specified name.
        /// </summary>
        /// <value>
        /// The <see cref="AuthElement"/>.
        /// </value>
        /// <param name="Name">The name.</param>
        /// <returns></returns>
        new public AuthElement this[string Name]
        {
            get
            {
                return (AuthElement)BaseGet(Name);
            }
        }
    }
}
