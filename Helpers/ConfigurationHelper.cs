using Microsoft.Extensions.Configuration;

namespace NeuralFabric.Helpers;

public static class ConfigurationHelper
{
    /// <summary>
    ///     Gets a string containing the directory to look for configuration files in.
    /// </summary>
    public static string ConfigurationBaseDirectory
        => AppDomain.CurrentDomain.BaseDirectory;

    /// <summary>
    ///     Gets a string containing the non-pathed configuration file name to look for within the base path.
    /// </summary>
    public static string ConfigurationFileName
        => $"{AppDomain.CurrentDomain.FriendlyName}-config.json";

    public static string FullyQualifiedConfigurationFileName
        => Path.Combine(
            path1: ConfigurationBaseDirectory,
            path2: ConfigurationFileName);

    public static IConfiguration LoadConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(basePath: ConfigurationBaseDirectory)
            .AddJsonFile(
                path: ConfigurationFileName,
                optional: false,
                reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
    }
}
