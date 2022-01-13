using NeuralFabric.Models.Hashes;

namespace NeuralFabric.Models;

public record ValidationResult<TObject, TResult>
{
    public readonly TObject InputObject;
    public readonly GuidId NodeId;
    public readonly TResult NodeResult;
    public readonly DataSignature NodeSignature;
}
