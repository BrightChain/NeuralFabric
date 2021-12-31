using FASTER.core;
using Neural.NET;
using Neural.NET.LayerInformation;

namespace NeuralFabric.Models.Serializers;

internal partial class NeuralNetworkSerializer :
    IObjectSerializer<ConvolutionalNetwork>,
    IObjectSerializer<FullyConnectedNetwork>,
    IObjectSerializer<ConvolutionalLayerInformation>,
    IObjectSerializer<FullyConnectedLayerInformation>,
    IObjectSerializer<NonLinearLayerInformation>,
    IObjectSerializer<PoolingLayerInformation>
{
}
