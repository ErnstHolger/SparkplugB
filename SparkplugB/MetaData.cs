using ProtoBuf;

namespace SparkplugB
{
    [ProtoContract]
    public class MetaData{
        [ProtoMember(1)]
        public bool is_multi_part { get; set; }
        [ProtoMember(2)]
        public string content_type { get; set; }
        [ProtoMember(3)]
        public ulong size { get; set; }
        [ProtoMember(4)]
        public ulong seq { get; set; }
        [ProtoMember(5)]
        public string file_name { get; set; }
        [ProtoMember(6)]
        public string file_type { get; set; }
        [ProtoMember(7)]
        public string md5 { get; set; }
        [ProtoMember(8)]
        public string description { get; set; }
    }
}