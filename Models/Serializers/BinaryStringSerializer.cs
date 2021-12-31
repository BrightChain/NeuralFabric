using FASTER.core;

namespace NeuralFabric.Models.Serializers;

public class BinaryStringSerializer : BinaryObjectSerializer<string>
{
    public override void Deserialize(out string obj)
    {
        obj = this.reader.ReadString();
    }

    public override void Serialize(ref string obj)
    {
        this.writer.Write(value: obj);
    }
}
