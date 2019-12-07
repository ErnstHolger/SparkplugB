using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;
using SparkplugB;

namespace SparkplugB
{
    public enum DataTypes
    {
         Unknown = 0,
         Int8 = 1,
         Int16 = 2,
         Int32 = 3,
         Int64 = 4,
         UInt8 = 5,
         UInt16 = 6,
         UInt32 = 7,
         UInt64 = 8,
         Float = 9,
         Double = 10,
         Boolean = 11,
         String = 12,
         DateTime = 13,
         Text = 14,
        // Additional Metric Types
         UUID = 15,
         DataSet = 16,
         Bytes = 17,
         File = 18,
         Template = 19,
        // Additional PropertyValue Types
        PropertySet = 20,
        PropertySetList = 21
    }
    [ProtoContract]
    public class Payload:ITimeStamp
    {
        [ProtoMember(1)]
        public ulong timestamp { get; set; }
        [ProtoMember(2)]
        public List<Metric> metrics { get; set; }
        [ProtoMember(3)]
        public ulong seq { get; set; }
        [ProtoMember(4)]
        public string uuid { get; set; }
        [ProtoMember(5)]
        public byte[] body { get; set; }
        //[ProtoMember(6)]
        //public string extensions { get; set; }
        public Payload()
        {
            metrics=new List<Metric>();
        }
    }
}
