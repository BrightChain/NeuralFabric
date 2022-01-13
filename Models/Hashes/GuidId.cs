using Microsoft.Extensions.Configuration;

namespace NeuralFabric.Models.Hashes;

/// <summary>
///     Notably a Guid is not a hash, but this is a convenient container.. maybe a refactor.
/// </summary>
public class GuidId : DataHash
{
    /// <summary>
    ///     Size in bits of the hash.
    /// </summary>
    public const int HashSize = 128;

    /// <summary>
    ///     Size in bytes of the hash.
    /// </summary>
    public const int HashSizeBytes = HashSize / 8;

    public readonly Guid Guid;

    public GuidId()
        : base(providedHashBytes: Guid.NewGuid().ToByteArray(), sourceDataLength: HashSizeBytes, computed: true)
    {
        this.Guid = new Guid(b: this.HashBytes.ToArray());
    }

    public GuidId(Guid guid)
        : base(providedHashBytes: guid.ToByteArray(), sourceDataLength: HashSizeBytes, computed: true)
    {
        this.Guid = guid;
    }

    public static GuidId FromConfiguration(IConfiguration configuration)
    {
        throw new NotImplementedException();
    }
}
