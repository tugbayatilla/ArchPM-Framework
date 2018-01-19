using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.Core.Api
{
    /// <summary>
    /// Sample response Codes. 
    /// </summary>
    public sealed class ApiResponseCodes
    {
        /// <summary>
        /// Operation was executed successfully
        /// </summary>
        public const String OK = "200";
        /// <summary>
        /// Operation was executed but you need to check the warnings
        /// </summary>
        public const String OK_WITH_WARNINGS = "210";
        /// <summary>
        /// Operation was NOT executed. Check Errors
        /// </summary>
        public const String ERROR = "900";
        /// <summary>
        /// Operation was NOT executed because of Validations
        /// </summary>
        public const String VALIDATION_ERROR = "910";
        /// <summary>
        /// Operation was NOT executed. Need Authentication
        /// </summary>
        public const String AUTHENTICATION_FAILED = "920";
        /// <summary>
        /// Operation was NOT executed. Need Authorization
        /// </summary>
        public const String AUTHORIZATION_FAILED = "930";
        /// <summary>
        /// Operation was NOT executed. Need Authorization
        /// </summary>
        public const String FATAL_ERROR = "940";

    }
}
