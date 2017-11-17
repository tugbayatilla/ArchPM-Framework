using System;
using System.Collections.Generic;

namespace ArchPM.Messaging.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TempObjectPackage<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TempObjectPackage{T}"/> class.
        /// </summary>
        public TempObjectPackage()
        {
            this.Data = new List<T>();
            this.CreateTime = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public List<T> Data { get; set; }

        /// <summary>
        /// Gets the create time.
        /// </summary>
        /// <value>
        /// The create time.
        /// </value>
        public DateTime CreateTime { get; private set; }
    }
}
