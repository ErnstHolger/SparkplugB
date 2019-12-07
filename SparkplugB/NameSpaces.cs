using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkplugB
{
    // example: namespace/group_id/NDATA/edge_node_id
    public enum NameSpaces
    {
    NBIRTH=0, //Birth certificate for MQTT EoN nodes.
    NDEATH, //Death certificate for MQTT EoN nodes.
    DBIRTH, //Birth certificate for Devices.
    DDEATH, //Death certificate for Devices.
    NDATA,//Node data message.
    DDATA, //Device data message.
    NCMD, //Node command message.
    DCMD, //Device command message.
    STATE, //Critical application state message.
    }
}
