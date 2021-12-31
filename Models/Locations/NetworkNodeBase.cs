using System.Runtime.Serialization;
using NetTopologySuite.Geometries;
using NeuralFabric.Enumerations;
using NeuralFabric.Models.Hashes;
using ProtoBuf;

namespace NeuralFabric.Models.Locations;

[ProtoContract]
[ProtoInclude(tag: 1, knownType: typeof(UriNetworkNodeBaseLocation))]
public abstract record NetworkNodeBase : ISerializable
{
    private readonly Point GeographicLocation;
    public readonly GuidId NodeId;
    public readonly LocationType NodeLocationType;
    public abstract void GetObjectData(SerializationInfo info, StreamingContext context);
}
