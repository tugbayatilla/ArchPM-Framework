using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.ApiQuery
{
    /// <summary>
    /// 
    /// </summary>
    public enum ApiQueryFieldOperators
    {
        /// <summary>
        /// The not
        /// </summary>
        Not = 1,
        /// <summary>
        /// The equal to
        /// </summary>
        EqualTo = 2,
        /// <summary>
        /// The greater than
        /// </summary>
        GreaterThan = 4,
        /// <summary>
        /// The less than
        /// </summary>
        LessThan = 8,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum QueryResponseTypes
    {
        /// <summary>
        /// Collection
        /// </summary>
        AsList,

        /// <summary>
        /// Single Value like int
        /// </summary>
        AsValue,

        /// <summary>
        /// Multiple values - Single Class
        /// </summary>
        AsObject,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ApiDbParameterDirection
    {
        /// <summary>
        /// The input
        /// </summary>
        Input = 1,
        /// <summary>
        /// The output
        /// </summary>
        Output = 2,
        /// <summary>
        /// The input output
        /// </summary>
        InputOutput = 3,
        /// <summary>
        /// The return value
        /// </summary>
        ReturnValue = 6
    }


    /// <summary>
    /// Specifies the data type of a field, a property, or a Parameter object of a .NET
    /// Framework data provider.
    /// </summary>
    public enum ApiDbType
    {
        /// <summary>
        /// The b file
        /// </summary>
        BFile = 101,
        /// <summary>
        /// The BLOB
        /// </summary>
        Blob = 102,
        /// <summary>
        /// The byte
        /// </summary>
        Byte = 103,
        /// <summary>
        /// The character
        /// </summary>
        Char = 104,
        /// <summary>
        /// The clob
        /// </summary>
        Clob = 105,
        /// <summary>
        /// The date
        /// </summary>
        Date = 106,
        /// <summary>
        /// The decimal
        /// </summary>
        Decimal = 107,
        /// <summary>
        /// The double
        /// </summary>
        Double = 108,
        /// <summary>
        /// The long
        /// </summary>
        Long = 109,
        /// <summary>
        /// The long raw
        /// </summary>
        LongRaw = 110,
        /// <summary>
        /// The int16
        /// </summary>
        Int16 = 111,
        /// <summary>
        /// The int32
        /// </summary>
        Int32 = 112,
        /// <summary>
        /// The int64
        /// </summary>
        Int64 = 113,
        /// <summary>
        /// The interval ds
        /// </summary>
        IntervalDS = 114,
        /// <summary>
        /// The interval ym
        /// </summary>
        IntervalYM = 115,
        /// <summary>
        /// The n clob
        /// </summary>
        NClob = 116,
        /// <summary>
        /// The n character
        /// </summary>
        NChar = 117,
        /// <summary>
        /// The n varchar2
        /// </summary>
        NVarchar2 = 119,
        /// <summary>
        /// The raw
        /// </summary>
        Raw = 120,
        /// <summary>
        /// The reference cursor
        /// </summary>
        RefCursor = 121,
        /// <summary>
        /// The single
        /// </summary>
        Single = 122,
        /// <summary>
        /// The time stamp
        /// </summary>
        TimeStamp = 123,
        /// <summary>
        /// The time stamp LTZ
        /// </summary>
        TimeStampLTZ = 124,
        /// <summary>
        /// The time stamp tz
        /// </summary>
        TimeStampTZ = 125,
        /// <summary>
        /// The varchar2
        /// </summary>
        Varchar2 = 126,
        /// <summary>
        /// The XML type
        /// </summary>
        XmlType = 127,
        /// <summary>
        /// The array
        /// </summary>
        Array = 128,
        /// <summary>
        /// The object
        /// </summary>
        Object = 129,
        /// <summary>
        /// The reference
        /// </summary>
        Ref = 130,
        /// <summary>
        /// The binary double
        /// </summary>
        BinaryDouble = 132,
        /// <summary>
        /// The binary float
        /// </summary>
        BinaryFloat = 133
    }
}
