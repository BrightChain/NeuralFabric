using FASTER.core;
using Neural.NET;
using Neural.NET.LayerInformation;

namespace NeuralFabric.Models.Serializers;

internal partial class NeuralNetworkSerializer : IObjectSerializer<ConvolutionalNetwork>, IObjectSerializer<FullyConnectedNetwork>,
    IObjectSerializer<ConvolutionalLayerInformation>, IObjectSerializer<FullyConnectedLayerInformation>,
    IObjectSerializer<NonLinearLayerInformation>, IObjectSerializer<PoolingLayerInformation>
{
    void IObjectSerializer<PoolingLayerInformation>.BeginSerialize(Stream stream)
    {
        throw new NotImplementedException();
    }

    void IObjectSerializer<PoolingLayerInformation>.EndSerialize()
    {
        throw new NotImplementedException();
    }

    void IObjectSerializer<PoolingLayerInformation>.BeginDeserialize(Stream stream)
    {
        throw new NotImplementedException();
    }

    public void Serialize(ref PoolingLayerInformation obj)
    {
        throw new NotImplementedException();
    }

    public void Deserialize(out PoolingLayerInformation obj)
    {
        throw new NotImplementedException();
    }

    void IObjectSerializer<PoolingLayerInformation>.EndDeserialize()
    {
        throw new NotImplementedException();
    }
}
