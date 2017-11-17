﻿using System;

namespace ArchPM.Core.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Exception" />
    /// <seealso cref="ArchPM.Core.Exceptions.IArchPMException" />
    public class DataLayerException : Exception, IArchPMException
    {
       // /// <summary>
       // /// Initializes a new instance of the <see cref="BusinessException"/> class.
       // /// </summary>
       // /// <param name="message">The message.</param>
       // /// <param name="args">The arguments.</param>
       //public DataLayerException(String message, params Object[] args)
       //     : base(String.Format(message, args))
       // {

       // }

       /// <summary>
       /// Initializes a new instance of the <see cref="BusinessException"/> class.
       /// </summary>
       /// <param name="message">The message that describes the error.</param>
        public DataLayerException(String message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public DataLayerException(String message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        public DataLayerException()
        {

        }
    }
}
