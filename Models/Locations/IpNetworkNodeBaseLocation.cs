using System.Net;
using System.Runtime.Serialization;
using ProtoBuf;

namespace NeuralFabric.Models.Locations;

[ProtoContract]
[ProtoInclude(tag: 1, knownType: typeof(NetworkNodeBase))]
public abstract record IpNetworkNodeBaseLocation : NetworkNodeBase
{
    private readonly IPAddress Address;

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
    }
}
