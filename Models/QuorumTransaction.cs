using NeuralFabric.Enumerations;
using NeuralFabric.Models.Hashes;
using NeuralFabric.Models.Nodes;

namespace NeuralFabric.Models;

public class QuorumTransaction<TObject, TResult>
{
    /// <summary>
    ///     TODO: this also needs an action, upsert, delete, etc.
    /// </summary>
    public readonly TObject ObjectBeingValidated;

    public readonly GuidId TransactionId;
    public readonly int ValidatingNodesRequired;
    public readonly Dictionary<NeuralFabricNode, ValidationResult<TObject, TResult>> ValidationsByNode;

    /// <summary>
    ///     To be called when the transaction is ultimately accepted or rejected by the quorum.
    /// </summary>
    public IEnumerable<Func<QuorumTransaction<TObject, TResult>>> CompletionCallbacks;

    public QuoromTransactionStatus ValidationStatus { get; private set; }

    /// <summary>
    ///     Once the transaction is Accepted, this will have the signature for the transaction.
    /// </summary>
    public DataSignature QuorumSignature { get; } = null;
}
