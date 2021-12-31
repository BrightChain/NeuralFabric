using System.Globalization;
using System.Security.Cryptography;
using NeuralFabric.Helpers;
using NeuralFabric.Interfaces;
using ProtoBuf;

namespace NeuralFabric.Models.Hashes;

/// <summary>
///     Type box for the sha hashes of signatures.
/// </summary>
[ProtoContract]
public class DataSignature : IDataSignature, IComparable<DataSignature>
{
    /// <summary>
    ///     Size in bits of the hash.
    /// </summary>
    public const int SignatureHashSize = 256;

    /// <summary>
    ///     Size in bytes of the hash.
    /// </summary>
    public const int SignatureHashSizeBytes = SignatureHashSize / 8;

    public DataSignature(ReadOnlyMemory<byte> providedHashBytes, bool computed = false)
    {
        if (providedHashBytes.Length != SignatureHashSizeBytes)
        {
            throw new ArgumentException(message: "Hash length mismatch");
        }

        this.SignatureHashBytes = providedHashBytes;
        this.Computed = computed;
    }

    public DataSignature(ReadOnlyMemory<byte> dataBytes)
    {
        using (var mySHA256 = SHA256.Create())
        {
            this.SignatureHashBytes = mySHA256.ComputeHash(buffer: dataBytes.ToArray());
        }

        this.Computed = true;
    }

    [ProtoMember(tag: 2)] public bool Computed { get; }

    public int CompareTo(DataSignature other)
    {
        return ReadOnlyMemoryComparer<byte>.Compare(ar1: this.SignatureHashBytes, ar2: other.SignatureHashBytes);
    }

    [ProtoMember(tag: 1)] public ReadOnlyMemory<byte> SignatureHashBytes { get; protected set; }

    public string ToString(string format, IFormatProvider _)
    {
        return BitConverter.ToString(value: this.SignatureHashBytes.ToArray()).Replace(oldValue: "-", newValue: string.Empty)
            .ToLower(culture: CultureInfo.InvariantCulture);
    }

    public static bool operator ==(DataSignature a, DataSignature b)
    {
        return ReadOnlyMemoryComparer<byte>.Compare(ar1: a.SignatureHashBytes, ar2: b.SignatureHashBytes) == 0;
    }

    public static bool operator ==(ReadOnlyMemory<byte> b, DataSignature a)
    {
        return ReadOnlyMemoryComparer<byte>.Compare(ar1: a.SignatureHashBytes, ar2: b) == 0;
    }

    public static bool operator !=(ReadOnlyMemory<byte> b, DataSignature a)
    {
        return !(b == a);
    }

    public static bool operator !=(DataSignature a, DataSignature b)
    {
        return !a.Equals(obj: b);
    }

    public new string ToString()
    {
        return BitConverter.ToString(value: this.SignatureHashBytes.ToArray()).Replace(oldValue: "-", newValue: string.Empty)
            .ToLower(culture: CultureInfo.InvariantCulture);
    }

    public override bool Equals(object obj)
    {
        return obj is DataSignature
            ? ReadOnlyMemoryComparer<byte>.Compare(ar1: this.SignatureHashBytes, ar2: (obj as DataSignature).SignatureHashBytes) == 0
            : false;
    }

    public override int GetHashCode()
    {
        return this.SignatureHashBytes.GetHashCode();
    }
}
