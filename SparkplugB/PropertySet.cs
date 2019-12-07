using System.Collections.Generic;
using ProtoBuf;

namespace SparkplugB
{
    [ProtoContract]
    public class PropertySet
    {
        [ProtoMember(1)]
        public List<string> keys { get; set; }
        [ProtoMember(2)]
        public List<PropertyValue> values { get; set; }
    }
}