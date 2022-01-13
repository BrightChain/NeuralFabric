namespace NeuralFabric.Enumerations;

public enum QuoromTransactionStatus
{
    /// <summary>
    ///     Validation is Pending.
    /// </summary>
    Pending,

    /// <summary>
    ///     Validation is Completed, not yet completed with Quorum.
    /// </summary>
    Completed,

    /// <summary>
    ///     Quorum has accepted the completed transaction.
    /// </summary>
    Accepted,

    /// <summary>
    ///     Quorum has rejected the completed transaction.
    /// </summary>
    Rejected
}
