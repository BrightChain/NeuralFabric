using FASTER.core;
using Neural.NET;

namespace NeuralFabric.Models;

/// <summary>
///     NeuralFabric intends to utilize FasterKV to store Neural Network state as well as use the neural network to distribute replicas to
///     desired areas.
///     NeuralFabric ensures the
/// </summary>
public partial class Tapestry : IDisposable
{
    private const string NeuralNetworkKeyName = "ConvolutionalNeuralNetwork";
    private readonly ConvolutionalNetwork NeuralNetwork;

    internal void SyncNetwork()
    {
        // setup
        var session = this._fasterKv.NewSession(functions: new SimpleFunctions<string, object, NeuralKvContext>());
        var networkKey = TranslateKey(valueType: this.NeuralNetwork.GetType(), keyName: NeuralNetworkKeyName);

        // TODO: Long-Term- track changes and store each dendrite in its own KV pair, etc.
        //                  eg foreach through all nodes and individually commit changes.
        // perform update(s)
        session.Upsert(key: networkKey, desiredValue: this.NeuralNetwork, userContext: new NeuralKvContext());

        // commit
        session.CompletePendingWithOutputs(completedOutputs: out var _, wait: true);
    }
}
