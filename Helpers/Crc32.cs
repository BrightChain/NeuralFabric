namespace NeuralFabric.Helpers;

public class Crc32
{
    private readonly uint[] table;

    public Crc32()
    {
        var poly = 0xedb88320;
        this.table = new uint[256];
        uint temp = 0;
        for (uint i = 0; i < this.table.Length; ++i)
        {
            temp = i;
            for (var j = 8; j > 0; --j)
            {
                if ((temp & 1) == 1)
                {
                    temp = (temp >> 1) ^ poly;
                }
                else
                {
                    temp >>= 1;
                }
            }

            this.table[i] = temp;
        }
    }

    public uint ComputeChecksum(byte[] bytes)
    {
        var crc = 0xffffffff;
        for (var i = 0; i < bytes.Length; ++i)
        {
            var index = (byte)((crc & 0xff) ^ bytes[i]);
            crc = (crc >> 8) ^ this.table[index];
        }

        return ~crc;
    }

    public byte[] ComputeChecksumBytes(byte[] bytes)
    {
        return BitConverter.GetBytes(value: this.ComputeChecksum(bytes: bytes));
    }

    public static uint ComputeNewChecksum(byte[] bytes)
    {
        return new Crc32().ComputeChecksum(bytes: bytes);
    }
}
