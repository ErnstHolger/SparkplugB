using System;
using ProtoBuf;

namespace SparkplugB
{
    [ProtoContract]
    public class Parameter
    {
        [ProtoMember(1)]
        public string name { set; get; }
        [ProtoMember(2)]
        public UInt32 type { set; get; }
        [ProtoMember(3)]
        public UInt32 int_value { set; get; }
        [ProtoMember(4)]
        public ulong long_value { set; get; }
        [ProtoMember(5)]
        public float float_value { set; get; }
        [ProtoMember(6)]
        public double double_value { set; get; }
        [ProtoMember(7)]
        public bool bool_value { set; get; }
        [ProtoMember(8)]
        public string string_value { set; get; }
    }
}