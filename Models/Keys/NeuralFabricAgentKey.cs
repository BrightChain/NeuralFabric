using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;

namespace NeuralFabric.Models.Keys;

public class NeuralFabricAgentKey : ECDsa
{
    private readonly IConfiguration _configuration;

    public NeuralFabricAgentKey(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    public NeuralFabricAgentKey()
    {
        this._configuration = new ConfigurationBuilder().Build();
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
