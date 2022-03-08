using NeuralFabric.Interfaces;

namespace NeuralFabric.Models;

public record ReplicatedTaggedObject<TTaggedObject> : ReplicatedObject<TTaggedObject>
    where TTaggedObject : IReplicatedSerializable<ReplicatedTaggedObject<TTaggedObject>>, IReplicatedSerializable<TTaggedObject>
{
    public readonly IEnumerable<string> Tags;

    public ReplicatedTaggedObject(TTaggedObject obj, IEnumerable<string> tags)
        : base(obj: obj,
            initialReplica: default)
    {
        this.Tags = tags;
    }
}
