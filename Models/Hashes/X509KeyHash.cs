using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;

namespace NeuralFabric.Models.Hashes;

/// <summary>
///     Notably a Guid is not a hash, but this is a convenient container.. maybe a refactor.
/// </summary>
public class X509KeyHash : DataHash
{
    /// <summary>
    ///     Size in bits of the hash.
    /// </summary>
    public const int HashSize = 128;

    /// <summary>
    ///     Size in bytes of the hash.
    /// </summary>
    public const int HashSizeBytes = HashSize / 8;

    public readonly X509Certificate X509Certificate;

    public X509KeyHash(X509Certificate certificate)
        : base(providedHashBytes: certificate.GetCertHash(),
            sourceDataLength: HashSizeBytes,
            computed: false)
    {
        this.X509Certificate = certificate;
    }

    public static GuidId FromConfiguration(IConfiguration configuration)
    {
        throw new NotImplementedException();
    }
}
