namespace NeuralFabric.Models;

/// <summary>
///     NeuralFabric intends to utilize FasterKV to store Neural Network state as well as use the neural network to distribute replicas to
///     desired areas.
///     NeuralFabric ensures the
/// </summary>
public partial class Tapestry : IDisposable
{
    public void Dispose()
    {
        this._fasterKv.Dispose();
        this._fasterDevice.Dispose();
    }
}
