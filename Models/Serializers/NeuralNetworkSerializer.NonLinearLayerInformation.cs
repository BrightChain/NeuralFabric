using FASTER.core;
using Neural.NET;
using Neural.NET.LayerInformation;

namespace NeuralFabric.Models.Serializers;

internal partial class NeuralNetworkSerializer : IObjectSerializer<ConvolutionalNetwork>, IObjectSerializer<FullyConnectedNetwork>,
    IObjectSerializer<ConvolutionalLayerInformation>, IObjectSerializer<FullyConnectedLayerInformation>,
    IObjectSerializer<NonLinearLayerInformation>, IObjectSerializer<PoolingLayerInformation>
{
    void IObjectSerializer<NonLinearLayerInformation>.BeginSerialize(Stream stream)
    {
        throw new NotImplementedException();
    }

    void IObjectSerializer<NonLinearLayerInformation>.EndSerialize()
    {
        throw new NotImplementedException();
    }

    void IObjectSerializer<NonLinearLayerInformation>.BeginDeserialize(Stream stream)
    {
        throw new NotImplementedException();
    }

    public void Serialize(ref NonLinearLayerInformation obj)
    {
        throw new NotImplementedException();
    }

    public void Deserialize(out NonLinearLayerInformation obj)
    {
        throw new NotImplementedException();
    }

    void IObjectSerializer<NonLinearLayerInformation>.EndDeserialize()
    {
        throw new NotImplementedException();
    }
}
