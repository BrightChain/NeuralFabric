using System.Runtime.InteropServices;

namespace NeuralFabric.Helpers;

/// <summary>
///     Poor implementation of a 1:1 comparator on a ReadOnlyMemory<typeparamref name="T" />
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class ReadOnlyMemoryComparer<T> : IEqualityComparer<ReadOnlyMemory<T>>, IComparer<ReadOnlyMemory<T>>
    where T : IEquatable<T>, IComparable<T>
{
    /// <summary> Compares the contents of the byte arrays and returns the result. </summary>
    int IComparer<ReadOnlyMemory<T>>.Compare(ReadOnlyMemory<T> x, ReadOnlyMemory<T> y)
    {
        return Compare(ar1: x, ar2: y);
    }

    /// <summary> Returns true if the two objects are the same instance </summary>
    bool IEqualityComparer<ReadOnlyMemory<T>>.Equals(ReadOnlyMemory<T> x, ReadOnlyMemory<T> y)
    {
        return 0 == Compare(ar1: x, ar2: y);
    }

    /// <summary> Returns a hash code the instance of the object </summary>
    int IEqualityComparer<ReadOnlyMemory<T>>.GetHashCode(ReadOnlyMemory<T> bytes)
    {
        return GetHashCode(memoryT: bytes);
    }

    /// <summary> returns true if both arrays contain the exact same set of bytes. </summary>
    public static bool Equals(ReadOnlyMemory<T> ar1, ReadOnlyMemory<T> ar2)
    {
        return 0 == Compare(ar1: ar1, ar2: ar2);
    }

    /// <summary> Compares the contents of the byte arrays and returns the result. </summary>
    public static int Compare(ReadOnlyMemory<T> ar1, ReadOnlyMemory<T> ar2)
    {
        if (ar1.IsEmpty)
        {
            return ar2.IsEmpty ? 0 : -1;
        }

        if (ar2.IsEmpty)
        {
            return 1;
        }

        var result = 0;
        int i = 0, stop = Math.Min(val1: ar1.Length, val2: ar2.Length);

        for (; 0 == result && i < stop; i++)
        {
            var a = ar1.Slice(start: i).ToArray()[0];
            var b = ar2.Slice(start: i).ToArray()[0];
            result = a.CompareTo(other: b);
        }

        if (result != 0)
        {
            return result;
        }

        if (i == ar1.Length)
        {
            return i == ar2.Length ? 0 : -1;
        }

        return 1;
    }

    /// <summary> Returns a hash code the instance of the object </summary>
    public static int GetHashCode(ReadOnlyMemory<T> memoryT)
    {
        var tArray = memoryT.ToArray();

        var size = Marshal.SizeOf(structure: tArray);
        if (size == 0)
        {
            return 0;
        }

        // Both managed and unmanaged buffers required.
        var bytes = new byte[size];
        var ptr = Marshal.AllocHGlobal(cb: size);
        // Copy object byte-to-byte to unmanaged memory.
        Marshal.StructureToPtr(structure: tArray, ptr: ptr, fDeleteOld: false);
        // Copy data from unmanaged memory to managed buffer.
        Marshal.Copy(source: ptr, destination: bytes, startIndex: 0, length: size);
        // Release unmanaged memory.
        Marshal.FreeHGlobal(hglobal: ptr);

        return (int)Crc32.ComputeChecksum(bytes: bytes);
    }
}
