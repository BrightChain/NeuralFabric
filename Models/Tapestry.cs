using FASTER.core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NeuralFabric.Helpers;
using NeuralFabric.Models.Agents;
using NeuralFabric.Models.Hashes;
using NeuralFabric.Models.Nodes;
using NeuralFabric.Models.Serializers;

namespace NeuralFabric.Models;

/// <summary>
///     NeuralFabric intends to utilize FasterKV to store Neural Network state as well as use the neural network to distribute replicas to
///     desired areas.
///     NeuralFabric ensures the
/// </summary>
public partial class Tapestry : IDisposable
{
    /// <summary>
    ///     This the number of buckets allocated to FASTER, where each bucket is 64 bytes (size of a cache line).
    /// </summary>
    private const long HashtableSize = 1L << 20;

    // Whether we enable a read cache
    private readonly bool _useReadCache = false;

    /// <summary>
    ///     Directory where the block tree root will be placed.
    /// </summary>
    private readonly DirectoryInfo _baseDirectory;

    private readonly string _collectionName;

    /// <summary>
    ///     Authoritative/creating agent- mainly relevant during bootstrap phase.
    /// </summary>
    private readonly NeuralFabricAgent _fabricAuthoritativeAgent;

    private readonly FasterKV<string, object> _fasterKv;

    /// <summary>
    ///     Information for this node.
    /// </summary>
    private readonly NeuralFabricNode _node;

    /// <summary>
    ///     Authoritative agent for the node- authorization to perform actions, used for crypto/signatures and node-node interaction.
    /// </summary>
    private readonly NeuralFabricAgent _nodeAuthoritativeAgent;

    private readonly IConfiguration _configuration;


    /// <summary>
    ///     Initializes a new instance of the <see cref="Tapestry" /> class.
    /// </summary>
    /// <param name="logger">Instance of the logging provider.</param>
    /// <param name="configuration">Instance of the configuration provider.</param>
    /// <param name="collectionName">Database/directory name for the store.</param>
    public Tapestry(ILogger logger, IConfiguration configuration, string collectionName)
    {
        this._collectionName = collectionName;
        var nodeOptions = configuration.GetSection("NodeOptions");
        if (nodeOptions is null || !nodeOptions.Exists())
        {
            this._configuration = ConfigurationHelper.LoadConfiguration();
            nodeOptions = this._configuration.GetSection("NodeOptions");
        }

        if (nodeOptions is null || !nodeOptions.Exists())
        {
            throw new Exception(string.Format(format: "'NodeOptions' config section must be defined, but is not"));
        }

        var configuredDbName
            = nodeOptions.GetSection("DatabaseName");

        var dbNameConfigured = configuredDbName is not null && configuredDbName.Value.Any();
        Guid serviceUnifiedStoreGuid = dbNameConfigured ? Guid.Parse(configuredDbName.Value) : Guid.NewGuid();

        var configOption = nodeOptions.GetSection("BasePath");
        var dir = configOption is not null && configOption.Value.Any() ? configOption.Value : Path.Join(Path.GetTempPath(), "brightchain");

        this._baseDirectory = Utilities.EnsuredDirectory(dir);

        if (configuredDbName is null || !configuredDbName.Value.Any())
        {
            //ConfigurationHelper.AddOrUpdateAppSetting("NodeOptions:DatabaseName", this.databaseName);
        }
        else
        {
            var expectedGuid = Guid.Parse(configuredDbName.Value);
            //if (expectedGuid != this.RootBlock.Guid)
            //{
            //    throw new BrightChainException("Provided root block does not match configured root block guid");
            //}
        }

        var readCache = nodeOptions.GetSection("EnableReadCache");
        this._useReadCache = readCache is null || readCache.Value is null ? false : Convert.ToBoolean(readCache.Value);

        this._baseDirectory = new DirectoryInfo(path: dir);
        this._logDevice = this.OpenDevice(nameSpace: string.Format(format: "{0}-log", arg0: this._collectionName));
        this._fasterDevice = this.OpenDevice(nameSpace: string.Format(format: "{0}-data", arg0: this._collectionName));
        this._fasterKv = new FasterKV<string, object>(
            size: HashtableSize, // hash table size (number of 64-byte buckets)
            logSettings: new LogSettings // log settings (devices, page size, memory size, etc.)
            {
                LogDevice = this._fasterDevice,
                ObjectLogDevice = this._fasterDevice,
                ReadCacheSettings = _useReadCache ? new ReadCacheSettings() : null
            },
            checkpointSettings: new CheckpointSettings
            {
                CheckpointDir = this.GetDiskCacheDirectory().FullName
            }, // Define serializers; otherwise FASTER will use the slower DataContract
            serializerSettings: new SerializerSettings<string, object>
            {
                keySerializer = () => new BinaryStringSerializer(), valueSerializer = BaseValueSerializer.GetInstance
            });

        this._nodeAuthoritativeAgent = new NeuralFabricAgent(configuration: configuration);
        this._node = new NeuralFabricNode(
            configuration: configuration,
            id: GuidId.FromConfiguration(
                configuration: configuration),
            agent: this._nodeAuthoritativeAgent);
    }

    private Tapestry()
    {
        throw new NotImplementedException();
    }
}
