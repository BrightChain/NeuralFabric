using NeuralFabric.Interfaces;
using NeuralFabric.Models.Agents;
using NeuralFabric.Models.Hashes;
using NeuralFabric.Models.Keys;
using NeuralFabric.Models.Nodes;

namespace NeuralFabric.Models.Quorum;

public record QuorumMembershipStore : IReplicatedSerializable<QuorumMembershipStore>
{
    /// <summary>
    ///     Current key to perform authorized actions on behalf of the quorum.
    ///     Must be unlockd by AuthorizeAndReleaseQuorumKey().
    /// </summary>
    private readonly NeuralFabricAgentKey? QuarumOperationsKey = null;

    /// <summary>
    /// </summary>
    public readonly IEnumerable<NeuralFabricAgentKey> QuorumAgentPublicKeys;

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
    public static void Serialize(in QuorumMembershipStore obj, out ReadOnlyMemory<byte> serializedObject)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// </summary>
    /// <param name="objectData"></param>
    /// <param name="rematerializedObject"></param>
    /// <exception cref="NotImplementedException"></exception>
    public static void Deserialize(in ReadOnlyMemory<byte> objectData, out QuorumMembershipStore rematerializedObject)
    {
        throw new NotImplementedException();
    }

    public void AuthorizeAndReleaseQuorumKey(NeuralFabricNode originatingNode, NeuralFabricAgent requestingAgent,
        NeuralFabricAgentKey? alternateKey = null)
    {
        // TODO: Check if the requesting agent is authorized to perform this action.
        // TODO: retrieve quorum key from the cache and decrypt it(?)
        throw new NotImplementedException();
    }

    public DataSignature SignOnBehalfOfQuorum<TObject>(TObject obj)
    {
        if (this.QuarumOperationsKey is null)
        {
            // TODO: change this
            throw new AccessViolationException();
        }

        throw new NotImplementedException();
    }
}
