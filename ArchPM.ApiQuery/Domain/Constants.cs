﻿using System;
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
        Not = 1,
        EqualTo = 2,
        GreaterThan = 4,
        LessThan = 8,
    }

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

    public enum ApiDbParameterDirection
    {
        Input = 1,
        Output = 2,
        InputOutput = 3,
        ReturnValue = 6
    }

    //
    // Summary:
    //     Specifies the data type of a field, a property, or a Parameter object of a .NET
    //     Framework data provider.
    public enum ApiDbType
    {
        BFile = 101,
        Blob = 102,
        Byte = 103,
        Char = 104,
        Clob = 105,
        Date = 106,
        Decimal = 107,
        Double = 108,
        Long = 109,
        LongRaw = 110,
        Int16 = 111,
        Int32 = 112,
        Int64 = 113,
        IntervalDS = 114,
        IntervalYM = 115,
        NClob = 116,
        NChar = 117,
        NVarchar2 = 119,
        Raw = 120,
        RefCursor = 121,
        Single = 122,
        TimeStamp = 123,
        TimeStampLTZ = 124,
        TimeStampTZ = 125,
        Varchar2 = 126,
        XmlType = 127,
        Array = 128,
        Object = 129,
        Ref = 130,
        BinaryDouble = 132,
        BinaryFloat = 133
    }
}
