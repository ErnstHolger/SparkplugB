using System.Collections.Generic;
using ProtoBuf;


namespace SparkplugB
{
    [ProtoContract]
    public class Template
    {
        [ProtoMember(1)]
        public string version { set; get; }
        [ProtoMember(2)]
        public List<Metric> metrics { set; get; }

        [ProtoMember(3)]
        public List<Parameter> parameters { set; get; }

        [ProtoMember(4)]
        public string template_ref { set; get; }

        [ProtoMember(5)]
        public bool  is_definition { set; get; }

    }
}