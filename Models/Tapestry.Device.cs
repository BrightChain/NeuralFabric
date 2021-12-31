using System.Globalization;
using FASTER.core;

namespace NeuralFabric.Models;

/// <summary>
///     NeuralFabric intends to utilize FasterKV to store Neural Network state as well as use the neural network to distribute replicas to
///     desired areas.
///     NeuralFabric ensures the
/// </summary>
public partial class Tapestry : IDisposable
{
    /// <summary>
    ///     Backing storage device.
    /// </summary>
    private readonly IDevice _fasterDevice;

    private readonly IDevice _logDevice;

    protected DirectoryInfo GetDiskCacheDirectory()
    {
        return Directory.CreateDirectory(
            path: Path.Combine(
                path1: this._baseDirectory.FullName,
                path2: string.Format(
                    provider: CultureInfo.InvariantCulture,
                    format: "{0}-{1}",
                    arg0: nameof(Tapestry),
                    arg1: this._collectionName)));
    }

    protected string GetDevicePath(string nameSpace, out DirectoryInfo cacheDirectoryInfo)
    {
        cacheDirectoryInfo = this.GetDiskCacheDirectory();

        return Path.Combine(
            path1: cacheDirectoryInfo.FullName,
            path2: string.Format(
                provider: CultureInfo.InvariantCulture,
                format: "{0}-{1}-{2}.log",
                arg0: nameof(Tapestry),
                arg1: this._collectionName,
                arg2: nameSpace));
    }

    protected IDevice OpenDevice(string nameSpace)
    {
        var devicePath = this.GetDevicePath(nameSpace: nameSpace, cacheDirectoryInfo: out var _);

        return Devices.CreateLogDevice(
            logPath: devicePath);
    }
}
