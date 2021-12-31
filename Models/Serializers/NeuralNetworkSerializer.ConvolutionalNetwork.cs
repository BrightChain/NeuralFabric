using FASTER.core;
using Neural.NET;
using Neural.NET.LayerInformation;

namespace NeuralFabric.Models.Serializers;

internal partial class NeuralNetworkSerializer : IObjectSerializer<ConvolutionalNetwork>, IObjectSerializer<FullyConnectedNetwork>,
    IObjectSerializer<ConvolutionalLayerInformation>, IObjectSerializer<FullyConnectedLayerInformation>,
    IObjectSerializer<NonLinearLayerInformation>, IObjectSerializer<PoolingLayerInformation>
{
    void IObjectSerializer<ConvolutionalNetwork>.BeginSerialize(Stream stream)
    {
        throw new NotImplementedException();
    }

    public void Serialize(ref ConvolutionalNetwork obj)
    {
        throw new NotImplementedException();
    }

    void IObjectSerializer<ConvolutionalNetwork>.EndSerialize()
    {
        throw new NotImplementedException();
    }

    void IObjectSerializer<ConvolutionalNetwork>.BeginDeserialize(Stream stream)
    {
        throw new NotImplementedException();
    }

    public void Deserialize(out ConvolutionalNetwork obj)
    {
        throw new NotImplementedException();
    }

    void IObjectSerializer<ConvolutionalNetwork>.EndDeserialize()
    {
        throw new NotImplementedException();
    }
}
