namespace NeuralFabric.Models.Nodes;

/// <summary>
///     Data Object Model containing node statistics and features.
/// </summary>
public record NeuralFabricNodeInfo
{
    /// <summary>
    ///     Data Object Model containing node statistics and features.
    /// </summary>
    public NeuralFabricNodeInfo(List<object> QuorumAdjustments, ulong LastUptime, ulong UnannouncedDisconnections)
    {
        this.QuorumAdjustments = QuorumAdjustments;
        this.LastUptime = LastUptime;
        this.UnannouncedDisconnections = UnannouncedDisconnections;
    }

    /// <summary>
    ///     Reserved concept property to indicate either positive or negative adjustments to the public info/statistics about this node.
    ///     All entries must be signed and match quorum in order to participate.
    /// </summary>
    public List<object> QuorumAdjustments { get; init; }

    /// <summary>
    ///     Last time the node came online.
    /// </summary>
    public ulong LastUptime { get; init; }

    /// <summary>
    ///     Numbers of unannounced disconnections without indicating stored block fate.
    /// </summary>
    public ulong UnannouncedDisconnections { get; init; }

    /// <summary>
    ///     Number of seconds total downtime during unnannounced disconnection events.
    ///     Avg flap duration = flapSeconds / unannouncedDisconnections.
    /// </summary>
    public ulong FlapSeconds { get; init; } = 0;

    public void Deconstruct(out List<object> QuorumAdjustments, out ulong LastUptime, out ulong UnannouncedDisconnections)
    {
        QuorumAdjustments = this.QuorumAdjustments;
        LastUptime = this.LastUptime;
        UnannouncedDisconnections = this.UnannouncedDisconnections;
    }
}
