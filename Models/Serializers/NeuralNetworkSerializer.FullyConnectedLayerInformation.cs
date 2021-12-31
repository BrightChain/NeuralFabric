using FASTER.core;
using Neural.NET;
using Neural.NET.LayerInformation;

namespace NeuralFabric.Models.Serializers;

internal partial class NeuralNetworkSerializer : IObjectSerializer<ConvolutionalNetwork>, IObjectSerializer<FullyConnectedNetwork>,
    IObjectSerializer<ConvolutionalLayerInformation>, IObjectSerializer<FullyConnectedLayerInformation>,
    IObjectSerializer<NonLinearLayerInformation>, IObjectSerializer<PoolingLayerInformation>
{
    void IObjectSerializer<FullyConnectedLayerInformation>.BeginSerialize(Stream stream)
    {
        throw new NotImplementedException();
    }

    void IObjectSerializer<FullyConnectedLayerInformation>.EndSerialize()
    {
        throw new NotImplementedException();
    }

    void IObjectSerializer<FullyConnectedLayerInformation>.BeginDeserialize(Stream stream)
    {
        throw new NotImplementedException();
    }

    public void Serialize(ref FullyConnectedLayerInformation obj)
    {
        throw new NotImplementedException();
    }

    public void Deserialize(out FullyConnectedLayerInformation obj)
    {
        throw new NotImplementedException();
    }

    void IObjectSerializer<FullyConnectedLayerInformation>.EndDeserialize()
    {
        throw new NotImplementedException();
    }
}
