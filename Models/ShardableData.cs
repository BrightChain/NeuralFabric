using NeuralFabric.Interfaces;
using STH1123.ReedSolomon;

namespace NeuralFabric.Models;

public class ShardableData<TObject>
    where TObject : IReplicatedSerializable<TObject>
{
    private readonly GenericGF _galoisField;
    private readonly ReedSolomonEncoder _reedSolomonEncoder;
    public readonly TObject Data;
    public readonly IEnumerable<ReadOnlyMemory<byte>> Shards;

    public ShardableData(TObject data, int totalShardHolderCount, int shardHoldersNeededToReconstitute)
    {
        this.Data = data;
        this._galoisField = new GenericGF(
            primitive: 284,
            size: 255,
            genBase: 0);
        this._reedSolomonEncoder = new ReedSolomonEncoder(field: this._galoisField);
        this.Shards = this.ConstructShards(
            totalShardHolderCount: totalShardHolderCount,
            shardHoldersNeededToReconstitute: shardHoldersNeededToReconstitute);
    }

    private static bool IsGaloisFieldValid(GenericGF galoisField)
    {
        // The easiest (brute-force) way to check if a polynomial is valid is to check if the first ((Galois field size) - 1) values of the generated Galois field are all unique, if they are, the polynomial is valid, if duplicate values are found, the polynomial is invalid.
        // This check can take a long time for larger fields.
        throw new NotImplementedException();
    }

    private ReadOnlyMemory<byte> ComputeFEC(ReadOnlyMemory<byte> block, bool returnFECOnly = false)
    {
        if (block.Length > this._galoisField.Size)
        {
            throw new ArgumentException(message: "Block is too large for the Galois field.");
        }

        // convert serialized byte data array to int array
        var intArray = block.ToArray().Select(selector: Convert.ToInt32).ToArray();
        var ecBytes = block.Length * 2; // as RS can repair up to half the ec bytes
        Array.Resize(array: ref intArray, newSize: block.Length + ecBytes);
        // ensure the block is padded with zeros
        Array.Fill(array: intArray, value: 0, startIndex: block.Length, count: ecBytes);

        this._reedSolomonEncoder.Encode(
            toEncode: intArray,
            ecBytes: ecBytes);

        var byteArray = intArray.Select(selector: Convert.ToByte).ToArray();
        if (!returnFECOnly)
        {
            return byteArray;
        }

        return new ReadOnlyMemory<byte>(
            array: byteArray.Take(
                range: new Range(
                    start: block.Length,
                    end: byteArray.Length - 1)).ToArray());
    }

    private IEnumerable<ReadOnlyMemory<byte>> ConstructShards(int totalShardHolderCount, int shardHoldersNeededToReconstitute)
    {
        if (shardHoldersNeededToReconstitute > totalShardHolderCount)
        {
            throw new ArgumentException(message: "Shards needed to reconstitute cannot be greater than total shard holders");
        }

        TObject.Serialize(
            obj: in this.Data,
            serializedObject: out var serializedData);

        var dataBytes = serializedData.Length;
        // we must pad data to a multiple of the field size,
        // and we must split data longer than the field size into groups of the field size
        // and then generate the FEC for each group
        // each shardholder may end up with more than one shard for a given dataset

        throw new NotImplementedException();
        return Array.Empty<ReadOnlyMemory<byte>>();
    }
}
