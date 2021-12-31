using FASTER.core;
using Neural.NET;
using Neural.NET.LayerInformation;

namespace NeuralFabric.Models.Serializers;

internal partial class NeuralNetworkSerializer : IObjectSerializer<ConvolutionalNetwork>, IObjectSerializer<FullyConnectedNetwork>,
    IObjectSerializer<ConvolutionalLayerInformation>, IObjectSerializer<FullyConnectedLayerInformation>,
    IObjectSerializer<NonLinearLayerInformation>, IObjectSerializer<PoolingLayerInformation>
{
    void IObjectSerializer<ConvolutionalLayerInformation>.BeginSerialize(Stream stream)
    {
        throw new NotImplementedException();
    }

    void IObjectSerializer<ConvolutionalLayerInformation>.EndSerialize()
    {
        throw new NotImplementedException();
    }

    void IObjectSerializer<ConvolutionalLayerInformation>.BeginDeserialize(Stream stream)
    {
        throw new NotImplementedException();
    }

    public void Serialize(ref ConvolutionalLayerInformation obj)
    {
        throw new NotImplementedException();
    }

    public void Deserialize(out ConvolutionalLayerInformation obj)
    {
        throw new NotImplementedException();
    }

    void IObjectSerializer<ConvolutionalLayerInformation>.EndDeserialize()
    {
        throw new NotImplementedException();
    }
}
