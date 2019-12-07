using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using SparkplugB;
using ProtoBuf;
using SparkplugB;

namespace SparkplugB
{
    [ProtoContract]
    public class Metric:ITimeStamp
    {
        public static ConcurrentDictionary<ulong, string> AliasLookUp =
            new ConcurrentDictionary<ulong, string>();

        [ProtoMember(1)] public string name { get; set; }
        [ProtoMember(2)] public ulong alias { get; set; }
        [ProtoMember(3)] public ulong timestamp { get; set; }
        [ProtoMember(4)] public ulong datatype { get; set; }
        [ProtoMember(5)] public bool is_historical { get; set; }
        [ProtoMember(6)] public bool is_transient { get; set; }
        [ProtoMember(7)] public bool is_null { get; set; }
        [ProtoMember(8)] public MetaData metadata { get; set; }
        [ProtoMember(9)] public PropertySet properties { get; set; }
        [ProtoMember(10)] public UInt32 int_value { get; set; }
        [ProtoMember(11)] public ulong long_value { get; set; }
        [ProtoMember(12)] public float float_value { get; set; }
        [ProtoMember(13)] public double double_value { get; set; }
        [ProtoMember(14)] public bool boolean_value { get; set; }
        [ProtoMember(15)] public string string_value { get; set; }
        [ProtoMember(16)] public byte[] bytes_value { get; set; }
        [ProtoMember(17)] public DataSet dataset_value { get; set; }
        [ProtoMember(18)] public Template template_value { get; set; }
        [ProtoMember(19)] public MetricValueExtension extension_value { get; set; }
    }
}