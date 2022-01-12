using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using NeuralFabric.Models.Agents;
using NeuralFabric.Models.Hashes;

namespace NeuralFabric.Models.Nodes;

/// <summary>
///     Representation of a bright chain participartory node.
/// </summary>
public class NeuralFabricNode
{
    /// <summary>
    ///     Gets the Id of the Node.
    ///     The Id of a Node is tied to its key once the block is accepted.
    ///     Duplicate Ids should not be accepted.
    ///     This will be used in TrustedNode lists.
    /// </summary>
    public readonly GuidId Id;

    /// <summary>
    ///     Entity with keys to perform actions on behalf of the node.
    /// </summary>
    public readonly NeuralFabricAgent NodeAgent;

    public readonly NeuralFabricNodeInfo NodeInfo;

    private IConfiguration _configuration;

    /// <summary>
    ///     Initializes a new instance of the <see cref="NeuralFabricNode" /> class.
    /// </summary>
    public NeuralFabricNode(IConfiguration configuration, GuidId id, NeuralFabricAgent agent)
    {
        // TODO: load NodeInfo from configuration
        this._configuration = configuration;
        this.NodeAgent = agent;
        this.Id = id;
    }

    public NeuralFabricNode(IConfiguration configuration)
    {
        this._configuration = configuration;
        this.NodeAgent = new NeuralFabricAgent(configuration: configuration);
        this.Id = GuidId.FromConfiguration(configuration: configuration);
    }

    /// <summary>
    ///     Gets the node agent's public key. Shortcut.
    /// </summary>
    public ECDiffieHellmanCngPublicKey PublicKey =>
        this.NodeAgent.DefaultPublicKey;
}
