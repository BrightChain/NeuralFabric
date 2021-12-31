namespace NeuralFabric.Models.Nodes;

/// <summary>
///     Data Object Model containing node statistics and features.
/// </summary>
public struct NeuralFabricNodeInfo
{
    /// <summary>
    ///     Reserved concept property to indicate either positive or negative adjustments to the public info/statistics about this node.
    ///     All entries must be signed and match quorum in order to participate.
    /// </summary>
    public readonly List<object> QuorumAdjustments;

    /// <summary>
    ///     Last time the node came online.
    /// </summary>
    public readonly ulong LastUptime;

    /// <summary>
    ///     Numbers of unannounced disconnections without indicating stored block fate.
    /// </summary>
    public readonly ulong UnannouncedDisconnections;

    /// <summary>
    ///     Number of seconds total downtime during unnannounced disconnection events.
    ///     Avg flap duration = flapSeconds / unannouncedDisconnections.
    /// </summary>
    public readonly ulong FlapSeconds = 0;
}
