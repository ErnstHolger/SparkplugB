using System;
using ProtoBuf;

namespace SparkplugB
{
    [ProtoContract]
    public class PropertyValue
    {
        [ProtoMember(1)]
        public UInt32 type { get; set; }
        [ProtoMember(2)]
        public bool is_nul { get; set; }
        [ProtoMember(3)]
        public UInt32 int_value { get; set; }
        [ProtoMember(4)]
        public ulong long_value { get; set; }
        [ProtoMember(5)]
        public float float_value { get; set; }
        [ProtoMember(6)]
        public double double_value { get; set; }
        [ProtoMember(7)]
        public bool boolean_value { get; set; }
        [ProtoMember(8)]
        public string string_value { get; set; }
        [ProtoMember(9)]
        public PropertySet propertyset_value { get; set; }
        [ProtoMember(10)]
        public PropertySetList propertysets_value { get; set; }
        [ProtoMember(11)]
        public PropertyValueExtension extension_valu { get; set; }
    }
}