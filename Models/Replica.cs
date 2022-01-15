using System.Collections.Immutable;
using NeuralFabric.Interfaces;
using NeuralFabric.Models.Hashes;
using NeuralFabric.Models.Locations;

namespace NeuralFabric.Models;

public record Replica : IReplicatedSerializable<Replica>
{
    /// <summary>
    ///     Unique id of this replica
    /// </summary>
    public readonly GuidId Id;

    /// <summary>
    ///     most recent successful ping as witnessed by this node
    /// </summary>
    public readonly ulong LastPing;

    /// <summary>
    ///     unix timestamp of missed reads, as witnessed by other nodes
    /// </summary>
    public readonly ImmutableDictionary<GuidId, IEnumerable<ulong>> MissedReadsByNodeId;

    /// <summary>
    ///     Node the replica is located on
    /// </summary>
    public readonly NetworkNodeBase NetworkNetworkNodeBaseLocation;

    /// <summary>
    ///     Hash of the replicated object
    /// </summary>
    public readonly DataHash ReplicatedObjectId;

    /// <summary>
    ///     Simple count of successful reads, as witnessed by other nodes
    ///     (ulong, data signature)?
    /// </summary>
    public readonly ImmutableDictionary<GuidId, ulong> SuccessfulReadsByNodeId;

    public static void Serialize(in Replica obj, out ReadOnlyMemory<byte> serializedObject)
    {
        throw new NotImplementedException();
    }

    public static void Deserialize(in ReadOnlyMemory<byte> objectData, out Replica rematerializedObject)
    {
        throw new NotImplementedException();
    }
}
