using System.Security.Cryptography;
using FASTER.core;
using NeuralFabric.Helpers;
using NeuralFabric.Interfaces;
using ProtoBuf;

namespace NeuralFabric.Models.Hashes;

/// <summary>
///     Type box for the sha hashes.
/// </summary>
[ProtoContract]
public class DataHash : IDataHash, IComparable<DataHash>, IEquatable<DataHash>, IFasterEqualityComparer<DataHash>
{
    /// <summary>
    ///     Size in bits of the hash.
    /// </summary>
    public const int HashSize = 256;

    /// <summary>
    ///     Size in bytes of the hash.
    /// </summary>
    public const int HashSizeBytes = HashSize / 8;

    /// <summary>
    ///     Initializes a new instance of the <see cref="DataHash" /> class.
    /// </summary>
    /// <param name="dataBytes">Data to compute hash from.</param>
    public DataHash(ReadOnlyMemory<byte> dataBytes)
    {
        using (var mySHA256 = SHA256.Create())
        {
            this.HashBytes = mySHA256.ComputeHash(buffer: dataBytes.ToArray());
        }

        this.Computed = true;
        this.SourceDataLength = dataBytes.Length;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DataHash" /> class.
    /// </summary>
    /// <param name="dataBytes">Data to compute hash from.</param>
    public DataHash(IEnumerable<byte> dataBytes)
    {
        using (var mySHA256 = SHA256.Create())
        {
            this.HashBytes = mySHA256.ComputeHash(buffer: (byte[])dataBytes);
        }

        this.Computed = true;
        this.SourceDataLength = dataBytes.Count();
    }

    public DataHash(Stream stream)
    {
        using (var sha = SHA256.Create())
        {
            var streamStart = stream.Position;
            sha.ComputeHash(inputStream: stream);
            var streamLength = stream.Position - streamStart;
            this.HashBytes = sha.Hash;
            this.SourceDataLength = streamLength;
            this.Computed = true;
        }
    }

    public DataHash(FileInfo fileInfo)
    {
        using (Stream stream = File.OpenRead(path: fileInfo.FullName))
        {
            using (var sha = SHA256.Create())
            {
                var streamStart = stream.Position;
                sha.ComputeHash(inputStream: stream);
                var streamLength = stream.Position - streamStart;
                this.HashBytes = sha.Hash;
                this.SourceDataLength = streamLength;
                this.Computed = true;
            }

            if (this.SourceDataLength != fileInfo.Length)
            {
                throw new Exception(message: nameof(this.SourceDataLength));
            }
        }
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DataHash" /> class.
    /// </summary>
    /// <param name="providedHashBytes">Hash bytes to accept as the hash.</param>
    /// <param name="computed">A boolean value indicating whether the source bytes were computed internally or externally (false).</param>
    /// <param name="sourceDataLength">A long indicating the length of the source data.</param>
    public DataHash(ReadOnlyMemory<byte> providedHashBytes, long sourceDataLength, bool computed)
    {
        if (this.HashBytes.Length != sourceDataLength)
        {
            throw new ArgumentException(message: "Hash length mismatch");
        }

        this.HashBytes = providedHashBytes;
        this.Computed = computed;
        this.SourceDataLength = sourceDataLength;
    }

    /// <summary>
    ///     Compares the raw bytes of the hash.
    /// </summary>
    /// <param name="other">Other DataHash to compare bytes with.</param>
    /// <returns>Returns a standard comparison result, -1, 0, 1 for less than, equal, greater than.</returns>
    /// TODO: verify -1/1 correctness
    public int CompareTo(DataHash other)
    {
        return other.SourceDataLength == this.SourceDataLength
            ? Helpers.ReadOnlyMemoryComparer<byte>.Compare(ar1: this.HashBytes, ar2: other.HashBytes)
            : other.SourceDataLength > this.SourceDataLength
                ? -1
                : 1;
    }

    /// <summary>
    ///     Gets a ReadOnlyMemory<byte> containing the raw hash result bytes.
    /// </summary>
    [ProtoMember(tag: 1)]
    public ReadOnlyMemory<byte> HashBytes { get; }

    /// <summary>
    ///     Gets a long containing the length of the source data the hash was computed on.
    /// </summary>
    [ProtoMember(tag: 2)]
    public long SourceDataLength { get; }

    /// <summary>
    ///     Gets a value indicating whether trusted code calculated this hash.
    /// </summary>
    [ProtoMember(tag: 3)]
    public bool Computed { get; }

    /// <summary>
    ///     Returns a formatted hash string as a series of lowercase hexadecimal characters.
    /// </summary>
    /// <param name="_">Ignored.</param>
    /// <param name="__">Ignored also.</param>
    /// <returns>Returns a formatted hash string.</returns>
    public string ToString(string _, IFormatProvider __)
    {
        return Utilities.HashToFormattedString(hashBytes: this.HashBytes.ToArray());
    }

    /// <summary>
    ///     Returns a boolean whether the two objects contain the same series of bytes.
    /// </summary>
    /// <param name="other">Other DataHash to compare bytes with.</param>
    /// <returns>Returns the standard comparison result, -1, 0, 1 for less than, equal, greater than.</returns>
    public bool Equals(DataHash other)
    {
        return !(other is null)
            ? other.SourceDataLength == this.SourceDataLength &&
              Helpers.ReadOnlyMemoryComparer<byte>.Compare(ar1: this.HashBytes, ar2: other.HashBytes) == 0
            : false;
    }

    public long GetHashCode64(ref DataHash k)
    {
        return (long)Crc64Iso.ComputeChecksum(k.HashBytes.ToArray());
    }

    public bool Equals(ref DataHash k1, ref DataHash k2)
    {
        return !(k2 is null)
            ? k2.SourceDataLength == k1.SourceDataLength &&
              Helpers.ReadOnlyMemoryComparer<byte>.Compare(ar1: k1.HashBytes, ar2: k2.HashBytes) == 0
            : false;
    }

    public static bool operator ==(DataHash a, DataHash b)
    {
        return a.SourceDataLength == b.SourceDataLength &&
               Helpers.ReadOnlyMemoryComparer<byte>.Compare(ar1: a.HashBytes, ar2: b.HashBytes) == 0;
    }

    public static bool operator ==(ReadOnlyMemory<byte> b, DataHash a)
    {
        return a.SourceDataLength == b.Length && Helpers.ReadOnlyMemoryComparer<byte>.Compare(ar1: a.HashBytes, ar2: b) == 0;
    }

    public static bool operator !=(ReadOnlyMemory<byte> b, DataHash a)
    {
        return !(b == a);
    }

    public static bool operator !=(DataHash a, DataHash b)
    {
        return !(b == a);
    }

    /// <summary>
    ///     Returns a formatted hash string as a series of lowercase hexadecimal characters.
    /// </summary>
    /// <returns>Returns a formatted hash string.</returns>
    public new string ToString()
    {
        return Utilities.HashToFormattedString(hashBytes: this.HashBytes.ToArray());
    }

    /// <summary>
    ///     Compares the raw bytes of the hash with a DataHash classed as a plain object.
    /// </summary>
    /// <param name="obj">Should be of DataHash type.</param>
    /// <returns>Returns a boolean indicating whether the bytes are the same in both objects.</returns>
    public override bool Equals(object? obj)
    {
        return obj is IDataHash iDataHash && (iDataHash.SourceDataLength == this.SourceDataLength &&
                                              Helpers.ReadOnlyMemoryComparer<byte>.Compare(ar1: this.HashBytes, ar2: iDataHash.HashBytes) == 0);
    }

    /// <summary>
    ///     Computes and returns the hash code for the HashBytes in this object.
    /// </summary>
    /// <returns>Returns the hash code for the HashBytes in this object.</returns>
    public override int GetHashCode()
    {
        return (int)Crc32.ComputeChecksum(bytes: this.HashBytes.ToArray());
    }
}
