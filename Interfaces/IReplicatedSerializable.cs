using System.Diagnostics.CodeAnalysis;

namespace NeuralFabric.Interfaces;

[SuppressMessage(category: "Usage", checkId: "CA2252:This API requires opting into preview features")]
public interface IReplicatedSerializable<TBase>
{
    /// <summary>
    ///     Serialize the given object to bytes.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static abstract void Serialize(in TBase obj, out ReadOnlyMemory<byte> serializedObject);

    /// <summary>
    ///     Deserialize the bytes to an object of the ordained type.
    /// </summary>
    /// <param name="objectData"></param>
    /// <returns></returns>
    public static abstract void Deserialize(in ReadOnlyMemory<byte> objectData, out TBase rematerializedObject);
}
