using NeuralFabric.Interfaces;
using NeuralFabric.Models.Agents;
using NeuralFabric.Models.Hashes;
using NeuralFabric.Models.Keys;

namespace NeuralFabric.Models.Quorum;

public record QuorumMembershipStore : IReplicatedSerializable<QuorumMembershipStore>
{
    /// <summary>
    /// </summary>
    public readonly IEnumerable<NeuralFabricAgentKey> QuorumAgentPublicKeys;

    /// <summary>
    /// Current key to perform authorized actions on behalf of the quorum.
    /// </summary>
    public readonly NeuralFabricAgentKey QuarumOperationsKey;
    
    /// <summary>
    /// </summary>
    public readonly IEnumerable<NeuralFabricAgent> QuorumAgents;

    /// <summary>
    /// </summary>
    public readonly GuidId QuorumId;

    /// <summary>
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="serializedObject"></param>
    /// <exception cref="NotImplementedException"></exception>
    public static void Serialize(ref QuorumMembershipStore obj, out ReadOnlyMemory<byte> serializedObject)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// </summary>
    /// <param name="objectData"></param>
    /// <param name="rematerializedObject"></param>
    /// <exception cref="NotImplementedException"></exception>
    public static void Deserialize(ReadOnlyMemory<byte> objectData, out QuorumMembershipStore rematerializedObject)
    {
        throw new NotImplementedException();
    }
}
