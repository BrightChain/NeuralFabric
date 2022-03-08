using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using NeuralFabric.Helpers;
using NeuralFabric.Models.Hashes;
using NeuralFabric.Models.Keys;

namespace NeuralFabric.Models.Agents;

public class NeuralFabricAgent
{
    private readonly IEnumerable<NeuralFabricAgentKey> AgentKeys;

    public readonly X509KeyHash DefaultKeyId;

    public readonly GuidId Id;
    private IConfiguration _configuration;

    public NeuralFabricAgent(IConfiguration configuration, NeuralFabricAgentKey? agentKey = null)
    {
        this._configuration = configuration;
        // TODO: load key from config
        this.AgentKeys = new[] {agentKey is null ? new NeuralFabricAgentKey(configuration: configuration) : agentKey};
    }

    public NeuralFabricAgentKey DefaultKey => this.FindKey(keyId: this.DefaultKeyId);

    public ECDiffieHellmanCngPublicKey DefaultPublicKey
    {
        get
        {
            var keyInfo = this.DefaultKey.ExportSubjectPublicKeyInfo();

            return ECDiffieHellmanCngPublicKey.FromByteArray(publicKeyBlob: keyInfo,
                    format: CngKeyBlobFormat.EccPublicBlob) as
                ECDiffieHellmanCngPublicKey;
        }
    }

    public NeuralFabricAgentKey FindKey(X509KeyHash keyId)
    {
        var certHashBytes = keyId.X509Certificate.GetCertHash();
        var foundKeys = this.AgentKeys
            .Where(predicate: k =>
                k.X509Certificate.GetCertHash().SequenceEqual(second: certHashBytes));
        var neuralFabricAgentKeys = foundKeys.ToArray();
        if (!neuralFabricAgentKeys.Any())
        {
            throw new KeyNotFoundException(
                message: Utilities.HashToFormattedString(
                    hashBytes: certHashBytes));
        }

        return neuralFabricAgentKeys.First();
    }
}
