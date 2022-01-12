using System.Globalization;
using System.Reflection;
using NeuralFabric.Models;
using NeuralFabric.Models.Hashes;

namespace NeuralFabric.Helpers;

public static class Utilities
{
    public static bool IsDebugMode
    {
        get
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }

    public static Version GetAssemblyVersionForType(Type assemblyType = null)
    {
        return Assembly.GetAssembly(
            type: assemblyType is null ? typeof(Tapestry) : assemblyType).GetName().Version;
    }

    public static async IAsyncEnumerable<byte> ReadOnlyMemoryToAsyncEnumerable(ReadOnlyMemory<byte> source)
    {
        foreach (var b in source.ToArray())
        {
            yield return b;
        }
    }

    public static async IAsyncEnumerable<byte> ParallelReadOnlyMemoryXORToAsyncEnumerable(ReadOnlyMemory<byte> sourceA,
        ReadOnlyMemory<byte> sourceB)
    {
        if (sourceA.Length != sourceB.Length)
        {
            throw new Exception(message: nameof(sourceB.Length));
        }

        var aArray = sourceA.ToArray();
        var bArray = sourceB.ToArray();
        for (var i = 0; i < aArray.Length; i++)
        {
            yield return (byte)(aArray[i] ^ bArray[i]);
        }
    }


    public static string HashToFormattedString(byte[] hashBytes)
    {
        return BitConverter.ToString(value: hashBytes)
            .Replace(oldValue: "-", newValue: string.Empty)
            .ToLower(culture: CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///     Generate a hash of an empty array to determine the block hash byte length
    ///     Used during testing.
    /// </summary>
    /// <param name="blockSize">Block size to generate zero vector for.</param>
    /// <param name="blockHash">Hash of the zero vector for the block.</param>
    public static DataHash GenerateZeroVector(int blockSize)
    {
        var blockBytes = new byte[blockSize];
        Array.Fill<byte>(array: blockBytes, value: 0);
        var blockHash = new DataHash(dataBytes: new ReadOnlyMemory<byte>(array: blockBytes));
        if (blockHash.HashBytes.Length != DataHash.HashSize / 8)
        {
            throw new Exception(message: "BlockHash size mismatch.");
        }

        return blockHash;
    }

    public static DirectoryInfo EnsuredDirectory(string dir)
    {
        if (!Directory.Exists(path: dir))
        {
            return Directory.CreateDirectory(path: dir);
        }

        return new DirectoryInfo(path: dir);
    }
}
