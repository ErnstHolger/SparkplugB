using System.Collections.Generic;
using ProtoBuf;
using SparkplugB;

namespace SparkplugB
{
    [ProtoContract]
    public class Row
    {
        [ProtoMember(1)]
        public List<DataSetValue> elements { get; set; }
    }
}