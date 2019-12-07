using System;
using System.Collections.Generic;
using SparkplugB;

namespace SparkplugB
{
    public class DataTypeClass
    {
        public static Dictionary<Type, uint> DataTypeLookUp =
            new Dictionary<Type, uint>
            {
                {typeof(Int16),6 },
                {typeof(Int32),7 },
                {typeof(Int64),8 },
                {typeof(UInt16),6 },
                {typeof(UInt32),7 },
                {typeof(UInt64),8 },
                {typeof(float),9 },
                {typeof(double),10 },
                {typeof(bool),11 },
                {typeof(string),12 },
                {typeof(DateTime),13 },
                {typeof(Guid),15 },
                {typeof(byte[]),17 },
                {typeof(Template),19 },
            };
                
    }

    public enum DataType
    {
        Int8 = 1,
        Int16 = 2,
        Int32 = 3,
        Int64 = 4,
        UInt8 = 5,
        UInt16 = 6,
        UInt32 = 7,
        UInt64 = 8,
        Float = 9,
        Double = 10,
        Boolean = 11,
        String = 12,
        DateTime = 13,
        Text = 14,
        // Additional Metric Types
        UUID = 15,
        DataSet = 16,
        Bytes = 17,
        File = 18,
        Template = 19,

        // Additional PropertyValue Types
        PropertySet = 20,
        PropertySetList = 21
    }
}