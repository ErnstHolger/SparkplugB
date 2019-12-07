using System;
using System.Collections.Generic;
using SparkplugB;
using ProtoBuf;

namespace SparkplugB
{
    [ProtoContract]
    public class DataSet
    {
        [ProtoMember(1)]
        public ulong num_of_columns { get; set; }
        [ProtoMember(2)]
        public List<string> columns { get; set; }
        [ProtoMember(3)]
        public List<UInt32> types { get; set; }
        [ProtoMember(4)]
        public List<Row> rows { get; set; }
    }
}