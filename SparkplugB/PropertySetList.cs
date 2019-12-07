using ProtoBuf;

namespace SparkplugB
{
    [ProtoContract]
    public class PropertySetList
    {
        [ProtoMember(1)]
        public PropertySet propertyset { get; set; }
    }
}