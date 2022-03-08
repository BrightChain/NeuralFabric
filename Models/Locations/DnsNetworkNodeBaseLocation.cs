using System.Runtime.Serialization;
using ProtoBuf;

namespace NeuralFabric.Models.Locations;

[ProtoContract]
[ProtoInclude(tag: 1,
    knownType: typeof(NetworkNodeBase))]
public abstract record DnsNetworkNodeBaseLocation : NetworkNodeBase
{
    private readonly string Host;

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
    }
}
