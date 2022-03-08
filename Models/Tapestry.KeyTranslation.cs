using System.Security.Cryptography;
using System.Text;

namespace NeuralFabric.Models;

/// <summary>
///     NeuralFabric intends to utilize FasterKV to store Neural Network state as well as use the neural network to distribute replicas to
///     desired areas.
///     NeuralFabric ensures the
/// </summary>
public partial class Tapestry : IDisposable
{
    // TODO: rename these something smarter
    private const char Separator = ':';
    private const char KeySeparator = '|';

    private static string GetTypeString(Type valueType)
    {
        return valueType.FullName ?? throw new InvalidOperationException();
    }

    private static string GetFormattedKey(Type valueType, string keyName)
    {
        return GetFormattedKey(
            typeString: GetTypeString(valueType: valueType),
            keyName: keyName);
    }

    private static string GetFormattedKey(string typeString, string keyName)
    {
        return $"{typeString}{KeySeparator}{keyName}";
    }

    private static string EncodeKeyString(Type type, string keyName, out byte[] hashBytes)
    {
        return EncodeKeyString(
            typeString: GetTypeString(valueType: type),
            keyName: keyName,
            hashBytes: out hashBytes);
    }

    private static string EncodeKeyString(string typeString, string keyName, out byte[] hashBytes)
    {
        var keyBytes = Encoding.UTF8.GetBytes(s: GetFormattedKey(typeString: typeString,
            keyName: keyName));
        hashBytes = MD5.HashData(source: keyBytes);
        var verifyHashString = Convert.ToHexString(inArray: hashBytes).ToLowerInvariant();
        return verifyHashString;
    }

    public static string TranslateKey(Type valueType, string keyName)
    {
        var typeString = GetTypeString(valueType: valueType);
        return string.Join(
            separator: Separator,
            typeString,
            EncodeKeyString(typeString: typeString,
                keyName: keyName,
                hashBytes: out _));
    }

    public static bool ValidateKey(string combinedKey, string keyName, out Type? expectedType)
    {
        var parts = combinedKey.Split(separator: Separator,
            count: 2);
        if (parts.Length != 2)
        {
            expectedType = null;
            return false;
        }

        var (typeString, hashString) = (parts[0], parts[1]);

        expectedType = Type.GetType(
            typeName: typeString,
            throwOnError: false);

        if (expectedType is null)
        {
            return false;
        }

        return hashString == EncodeKeyString(
            typeString: typeString,
            keyName: keyName,
            hashBytes: out _);
    }
}
