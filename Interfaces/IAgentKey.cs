using NeuralFabric.Enumerations;

namespace NeuralFabric.Interfaces;

public interface IAgentKey
{
    public void Enable(AgentKeyType newType = AgentKeyType.Login);
    public void Disable();
    public void SetType(AgentKeyType type);
}
