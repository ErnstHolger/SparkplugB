using System;
using SparkplugB;
using ProtoBuf;

namespace SparkplugB
{
    [ProtoContract]
    public class DataSetValue
    {
        [ProtoMember(1)]
        public UInt32 int_value { get; set; }
        [ProtoMember(2)]
        public ulong long_value { get; set; }
        [ProtoMember(3)]
        public float float_value { get; set; }
        [ProtoMember(4)]
        public double double_value { get; set; }
        [ProtoMember(5)]
        public bool boolean_value { get; set; }
        [ProtoMember(6)]
        public string string_value { get; set; }
        [ProtoMember(7)]
        public DataSetValueExtension extension_value { get; set; }

        public static object FromDataSetType(DataSetValue dataSetValue, uint dataType)
        {
            switch (dataType)
            {
                case (int)DataType.Boolean:
                    return dataSetValue.boolean_value;
                case (int)DataType.Double:
                    return dataSetValue.double_value;
                case (int)DataType.Float:
                    return dataSetValue.float_value;
                case (int)DataType.UInt64:
                    return dataSetValue.long_value;
                case (int)DataType.UInt32:
                    return dataSetValue.int_value;
                case  (int)DataType.String:
                    return dataSetValue.string_value;
                default:
                    return dataSetValue.string_value;
            }
        }
        public static DataSetValue ToDataSetValue(object value)
        {
            var dataSetValue = new DataSetValue();
            if (value == null)
            {
                return dataSetValue;
            }

            //dataSetValue.timestamp = (ulong)(new DateTimeOffset(timeStamp).ToUnixTimeMilliseconds());
            switch (value)
            {
                case bool b:
                    dataSetValue.boolean_value = b;
                    break;
                case double d:
                    dataSetValue.double_value = d;
                    break;
                case float f:
                    dataSetValue.float_value = f;
                    break;
                case long l:
                    dataSetValue.long_value = (ulong)value;
                    break;
                case int _:
                case short _:
                    dataSetValue.int_value = (uint)value;
                    break;
                case string s:
                    dataSetValue.string_value = s;
                    break;
                default:
                    dataSetValue.string_value = value.ToString();
                    break;
            }
            return dataSetValue;
        }
    }
}