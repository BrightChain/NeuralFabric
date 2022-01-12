using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;
using NeuralFabric.Enumerations;
using NeuralFabric.Interfaces;
using NeuralFabric.Models.Hashes;

namespace NeuralFabric.Models.Keys;

public sealed class NeuralFabricAgentKey : ECDsa, IAgentKey
{
    private readonly IConfiguration _configuration;

    public NeuralFabricAgentKey(IConfiguration configuration, AgentKeyType defaultType = AgentKeyType.Disabled)
    {
        this._configuration = configuration;
        this.KeyType = defaultType;
    }

    public NeuralFabricAgentKey(AgentKeyType defaultType = AgentKeyType.Disabled)
    {
        this._configuration = new ConfigurationBuilder().Build();
        this.KeyType = defaultType;
    }

    public X509KeyHash KeyId => new(certificate: this.X509Certificate);
    public AgentKeyType KeyType { get; private set; }

    public X509Certificate X509Certificate
    {
        get
        {
            var keyInfo = this.ExportSubjectPublicKeyInfo();
            return new X509Certificate(data: keyInfo);
        }
    }

    public void Enable(AgentKeyType newType = AgentKeyType.Login)
    {
        if (this.KeyType != AgentKeyType.Disabled)
        {
            throw new Exception(message: "Already enabled");
        }

        this.SetType(type: newType);
    }

    public void Disable()
    {
        if (this.KeyType == AgentKeyType.Disabled)
        {
            throw new Exception(message: "Already disabled");
        }

        this.SetType(type: AgentKeyType.Disabled);
    }

    public void SetType(AgentKeyType type)
    {
        this.KeyType = type;
    }

    public static new NeuralFabricAgentKey Create()
    {
        return Create();
    }

    public static new NeuralFabricAgentKey Create(ECCurve curve)
    {
        return Create(curve: curve);
    }

    public override byte[] SignHash(byte[] hash)
    {
        throw new NotImplementedException();
    }

    public override bool VerifyHash(byte[] hash, byte[] signature)
    {
        throw new NotImplementedException();
    }
}
