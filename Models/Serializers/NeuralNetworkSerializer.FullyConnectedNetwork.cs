using FASTER.core;
using Neural.NET;
using Neural.NET.LayerInformation;

namespace NeuralFabric.Models.Serializers;

internal partial class NeuralNetworkSerializer : IObjectSerializer<ConvolutionalNetwork>, IObjectSerializer<FullyConnectedNetwork>,
    IObjectSerializer<ConvolutionalLayerInformation>, IObjectSerializer<FullyConnectedLayerInformation>,
    IObjectSerializer<NonLinearLayerInformation>, IObjectSerializer<PoolingLayerInformation>
{
    void IObjectSerializer<FullyConnectedNetwork>.BeginSerialize(Stream stream)
    {
        throw new NotImplementedException();
    }

    public void Serialize(ref FullyConnectedNetwork obj)
    {
        throw new NotImplementedException();
    }

    void IObjectSerializer<FullyConnectedNetwork>.EndSerialize()
    {
        throw new NotImplementedException();
    }

    void IObjectSerializer<FullyConnectedNetwork>.BeginDeserialize(Stream stream)
    {
        throw new NotImplementedException();
    }

    public void Deserialize(out FullyConnectedNetwork obj)
    {
        throw new NotImplementedException();
    }

    void IObjectSerializer<FullyConnectedNetwork>.EndDeserialize()
    {
        throw new NotImplementedException();
    }
}
