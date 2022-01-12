namespace NeuralFabric.Enumerations;

public enum AgentKeyType
{
    /// <summary>
    ///     Key is not enabled for use.
    /// </summary>
    Disabled,

    /// <summary>
    ///     Key logs in normally.
    /// </summary>
    Login,

    /// <summary>
    ///     Key triggers event on login, but login fails.
    /// </summary>
    Trigger,

    /// <summary>
    ///     Key is to login normally, but trigger associated action/event. normally normally normally
    /// </summary>
    SilentTrigger,

    /// <summary>
    ///     Key is for identity only. Can not log in, but can sign/authenticate
    /// </summary>
    IdentityOnly
}
