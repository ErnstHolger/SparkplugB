using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SparkplugB;

namespace SparkplugB
{
    // Timestamp at message sending time
    // Repeated forever - no limit in Google Protobufs
    // Sequence number
    // UUID to track message type in terms of schema definitions
    // To optionally bypass the whole definition above
    // For third party extensions
    public interface ITimeStamp
    {
        ulong timestamp { get; set; }
    }
    public static class MQTTExtension
    {
        private static readonly Random Random = new Random();
        public static void SetGuid(this Payload payLoad, int length = 10)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            payLoad.uuid = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
        #region unix time
        public static ulong ToUnixTimeULong(this DateTime timeStamp)
        {
            return (ulong)(new DateTimeOffset(timeStamp).ToUnixTimeMilliseconds());
        }
        public static DateTime FromUnixTimeULong(this ulong timeStamp)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds((long)timeStamp).ToLocalTime().DateTime;
        }
        public static void SetTimeStamp(this ITimeStamp iTimeStamp, DateTime timeStamp)
        {
            iTimeStamp.timestamp = (ulong)(new DateTimeOffset(timeStamp).ToUnixTimeMilliseconds());
        }
        public static DateTime GetTimeStamp(this ITimeStamp iTimeStamp)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds((long)iTimeStamp.timestamp).ToLocalTime().DateTime;
        }
#endregion
        public static bool ToDateTimeValue(this Metric metric, out object value,
         out DateTime timeStamp, out bool isHistorical)
        {
            value = null;
            timeStamp = DateTime.MinValue;
            timeStamp = metric.GetTimeStamp();
            isHistorical = metric.is_historical;
            switch (metric.datatype)
            {
                case 11:
                    value = metric.boolean_value;
                    break;
                case 10:
                    value = metric.double_value;
                    break;
                case 9:
                    value = metric.float_value;
                    break;
                case 8:
                    value = metric.long_value;
                    break;
                case 7:
                    value = metric.int_value;
                    break;
                case 12:
                    value = metric.string_value;
                    break;
                default:
                    value = metric.string_value;
                    break;
            }

            return true;
        }

        #region new
        public static void New(this Metric metric, string name, List<object> value,
            List<DateTime> timeStamp, bool isHistorical)
        {
            metric.New(new List<string> { name },
                new List<List<object>> { value },
                timeStamp, isHistorical);
        }
        public static void New(this Metric metric, List<string> name, List<List<object>> value,
            List<DateTime> timeStamp, bool isHistorical)
        {
            metric.name = "datatable";
            metric.is_historical = isHistorical;

            if (value == null || timeStamp == null)
            {
                metric.is_null = true;
                return;
            }
            metric.SetTimeStamp(timeStamp[0]);
            metric.dataset_value = new DataSet
            {
                num_of_columns = (ulong)(value.Count + 1),
                columns = new List<string> { "timestamp" },
                types = new List<uint> { 8 }
            };
            metric.dataset_value.columns.AddRange(name);
            value.ForEach(n =>
            {
                var dataType = DataTypeClass.DataTypeLookUp.ContainsKey(n[0].GetType()) ?
                    DataTypeClass.DataTypeLookUp[n[0].GetType()] : (uint)12;
                metric.dataset_value.types.Add(dataType);

            });
            var row = new Row { elements = new List<DataSetValue>() };
            value.ForEach(m =>
            {
                for (var index = 0; index < m.Count; index++)
                {
                    row.elements.Add(DataSetValue.ToDataSetValue(
                        timeStamp[index].ToUnixTimeULong()));
                    row.elements.Add(DataSetValue.ToDataSetValue(m[index]));
                }
            });
            metric.datatype = 16;
            metric.dataset_value.rows.Add(row);
        }

        public static bool ToDateTimeValue(this Metric metric, out List<string> name,
            out List<List<object>> value,
            out List<DateTime> timeStamp, out bool isHistorical)
        {
            value = null;
            var rows = metric.dataset_value.rows.Count;
            var columns = (int)metric.dataset_value.num_of_columns;

            isHistorical = metric.is_historical;
            timeStamp = new List<DateTime>();
            // get the name
            name = metric.dataset_value.columns;
            // get the time stamp
            for (var index = 0; index < rows; index++)
            {
                var raw = metric.dataset_value.rows[index].elements[0].long_value;
                timeStamp.Add(raw.FromUnixTimeULong());
            }
            // get the value
            value = new List<List<object>>();
            for (var jndex = 1; jndex < columns; jndex++)
            {
                value.Add(new List<object>());
                for (var index = 0; index < rows; index++)
                {
                    value[jndex - 1].Add(
                        DataSetValue.FromDataSetType(metric.dataset_value.rows[index].elements[jndex],
                            metric.dataset_value.types[jndex]));
                }
            }
            return true;
        }

        public static void New(this Metric metric, ulong alias, object value,
            DateTime timeStamp, bool isHistorical, Hashtable properties)
        {
            if (!Metric.AliasLookUp.TryGetValue(alias, out string name)) return;
            metric.New(name, 0, value, timeStamp, isHistorical, properties);
        }
        public static void New(this Metric metric, string name, object value,
            DateTime timeStamp, bool isHistorical, Hashtable properties)
        {
            metric.New(name,0, value, timeStamp, isHistorical, properties);
        }
        public static void New(this Metric metric, string name, ulong alias, object value,
            DateTime timeStamp, bool isHistorical, Hashtable properties)
        {
            if (alias != 0) Metric.AliasLookUp.TryAdd(alias, name);
            metric.New(name, value, timeStamp, isHistorical);
            metric.properties = new PropertySet
            {
                keys = new List<string>(), values = new List<PropertyValue>()
            };
            foreach (DictionaryEntry property in properties)
            {
                metric.properties.keys.Add(property.Key.ToString());
                metric.properties.values.Add(new PropertyValue
                {
                    string_value = property.Value.ToString()
                });
            }
        }
        public static void New(this Metric metric, string name, ulong alias, object value,
            DateTime timeStamp, bool isHistorical)
        {
            Metric.AliasLookUp.TryAdd(alias, name);
            metric.New(name, value, timeStamp, isHistorical);
            metric.is_historical = true;
        }

        public static void New(this Metric metric, ulong alias, object value,
            DateTime timeStamp, bool isHistorical)
        {
            if (!Metric.AliasLookUp.TryGetValue(alias, out string name)) return;
            metric.New(name, value, timeStamp, isHistorical);
            metric.is_historical = true;
        }

        public static void New(this Metric metric, string name, object value,
            DateTime timeStamp, bool isHistorical)
        {
            metric.name = name;
            metric.is_historical = isHistorical;
            if (value == null)
            {
                metric.is_null = true;
                return;
            }

            metric.SetTimeStamp(timeStamp);
            switch (value)
            {
                case bool b:
                    metric.boolean_value = b;
                    metric.datatype = 11;
                    break;
                case double d:
                    metric.double_value = d;
                    metric.datatype = 10;
                    break;
                case float f:
                    metric.float_value = f;
                    metric.datatype = 9;
                    break;
                case long l:
                    metric.long_value = Convert.ToUInt64(value);
                    metric.datatype = 8;
                    break;
                case int _:
                case short _:
                    metric.int_value = Convert.ToUInt32(value);
                    metric.datatype = 7;
                    break;
                case string s:
                    metric.string_value = s;
                    metric.datatype = 12;
                    break;
                default:
                    metric.string_value = value.ToString();
                    metric.datatype = 14;
                    break;
            }

        }
#endregion
    }
}
