using System.Runtime.Serialization;
using ProtoBuf;

namespace NeuralFabric.Models.Locations;

[ProtoContract]
[ProtoInclude(tag: 1,
    knownType: typeof(NetworkNodeBase))]
public abstract record UriNetworkNodeBaseLocation : NetworkNodeBase
{
    private readonly Uri Uri;

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
    }
}
