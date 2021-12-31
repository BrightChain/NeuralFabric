using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using NeuralFabric.Models.Hashes;
using NeuralFabric.Models.Keys;

namespace NeuralFabric.Models.Agents;

public class NeuralFabricAgent
{
    private readonly NeuralFabricAgentKey AgentKey;
    private IConfiguration _configuration;

    public NeuralFabricAgent(IConfiguration configuration, NeuralFabricAgentKey? agentKey = null)
    {
        this._configuration = configuration;
        // TODO: load key from config
        this.AgentKey = agentKey is null ? new NeuralFabricAgentKey(configuration: configuration) : agentKey;
    }

    public GuidId Id { get; }

    public ECDiffieHellmanCngPublicKey PublicKey
    {
        get
        {
            var keyInfo = this.AgentKey.ExportSubjectPublicKeyInfo();

            return ECDiffieHellmanCngPublicKey.FromByteArray(publicKeyBlob: keyInfo, format: CngKeyBlobFormat.EccPublicBlob) as
                ECDiffieHellmanCngPublicKey;
        }
    }
}
