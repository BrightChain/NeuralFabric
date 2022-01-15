using System.Diagnostics.CodeAnalysis;
using FASTER.core;
using NeuralFabric.Interfaces;
using NeuralFabric.Models.Hashes;

namespace NeuralFabric.Models;

[SuppressMessage(category: "Usage", checkId: "CA2252:This API requires opting into preview features")]
public abstract record ReplicatedObject<TReplicatedObject> : IReplicatedSerializable<TReplicatedObject>,
    IObjectSerializer<ReplicatedObject<TReplicatedObject>>
    where TReplicatedObject : IReplicatedSerializable<TReplicatedObject>
{
    public readonly TReplicatedObject Object;
    public readonly ReadOnlyMemory<byte> ObjectData;
    public readonly DataHash ObjectId;
    public readonly Type ObjectType;
    public readonly IEnumerable<Replica> Replicas;
    private Stream? _deserializationStream;

    private Stream? _serializationStream;

    public ReplicatedObject(in TReplicatedObject obj, Replica initialReplica) : this(obj: obj, replicas: new[] {initialReplica})
    {
    }

    public ReplicatedObject(in TReplicatedObject obj, IEnumerable<Replica> replicas)
    {
        Serialize(obj: in obj, serializedObject: out var serializedObject);
        this.ObjectData = serializedObject;
        this.ObjectType = typeof(TReplicatedObject);
        this.Replicas = replicas;
        this.Object = obj;
        this.ObjectId = new DataHash(dataBytes: this.ObjectData);
    }

    public ReplicatedObject(in ReadOnlyMemory<byte> objectData)
    {
        this.ObjectType = typeof(TReplicatedObject);
        this.Replicas = Enumerable.Empty<Replica>();
        this.ObjectData = objectData;
        Deserialize(objectData: in this.ObjectData, rematerializedObject: out var rematerializedObject);
        this.Object = rematerializedObject;
        this.ObjectId = new DataHash(dataBytes: this.ObjectData);
    }

    public void BeginSerialize(Stream stream)
    {
        if (this._serializationStream is not null)
        {
            throw new Exception(message: "Previous serialization incomplete");
        }

        this._serializationStream = stream;
    }

    public void Serialize(ref ReplicatedObject<TReplicatedObject> obj)
    {
        throw new NotImplementedException();
    }

    public void EndSerialize()
    {
        if (this._serializationStream is null)
        {
            throw new Exception(message: "serialization not begun");
        }

        this._serializationStream.Flush();
        this._serializationStream = null;
    }

    public void BeginDeserialize(Stream stream)
    {
        if (this._deserializationStream is not null)
        {
            throw new Exception(message: "Previous deserialization incomplete");
        }

        this._deserializationStream = stream;
    }

    public void Deserialize(out ReplicatedObject<TReplicatedObject> obj)
    {
        throw new NotImplementedException();
    }

    public void EndDeserialize()
    {
        if (this._deserializationStream is null)
        {
            throw new Exception(message: "deserialization not begun");
        }

        this._deserializationStream.Flush();
        this._deserializationStream = null;
    }

    public static void Serialize(in TReplicatedObject obj, out ReadOnlyMemory<byte> serializedObject)
    {
        throw new NotImplementedException();
    }

    public static void Deserialize(in ReadOnlyMemory<byte> objectData, out TReplicatedObject rematerializedObject)
    {
        throw new NotImplementedException();
    }
}
