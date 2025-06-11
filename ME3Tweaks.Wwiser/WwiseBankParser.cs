using System.Text;
using BinarySerialization;
using ME3Tweaks.Wwiser.Model;

namespace ME3Tweaks.Wwiser;

public static class WwiseBankParser
{
    public static WwiseBank Deserialize(Stream stream)
    {
        if (stream is null or { Length : 0 })
        {
            throw new Exception("Inner stream is null or empty, cannot deserialize.");
        }
        
        var serializer = new BinarySerializer();
        var (version, useFeedback) = ReadWwiseHeaderInfo(stream);
        var context = new BankSerializationContext(version, false, useFeedback);
        
        var root = serializer.Deserialize<WwiseBankRoot>(stream, context);
        return root.ToBank();
    }

    public static Task<WwiseBank> DeserializeAsync(Stream stream)
    {
        if (stream is null or { Length : 0 })
        {
            throw new Exception("Inner stream is null or empty, cannot deserialize.");
        }
        
        var serializer = new BinarySerializer();
        var (version, useFeedback) = ReadWwiseHeaderInfo(stream);
        var context = new BankSerializationContext(version, false, useFeedback);
        
        return serializer.DeserializeAsync<WwiseBankRoot>(stream, context)
            .ContinueWith(t => t.Result.ToBank());
    }

    public static void Serialize(WwiseBank bank, Stream stream)
    {
        SetBankHeaderPadding(bank);
        
        var serializer = new BinarySerializer();
        serializer.Serialize(stream, WwiseBankRoot.FromBank(bank), BankSerializationContext.FromBank(bank));
    }

    public static Task SerializeAsync(WwiseBank bank, Stream stream, CancellationToken cancellationToken = default)
    {
        SetBankHeaderPadding(bank);
        
        var serializer = new BinarySerializer();
        return serializer.SerializeAsync(stream, WwiseBankRoot.FromBank(bank), BankSerializationContext.FromBank(bank), cancellationToken);
    }
    
    /// <summary>
    /// Determines the wwise build version of a bank
    /// </summary>
    /// <remarks>
    /// We do this ahead of full deserialization so that we can pass the
    /// version in to the serializer as a globally available parameter.
    ///
    /// Ported from _check_header() in wwiser
    /// </remarks>
    /// <param name="stream">Stream of a complete Wwise bank</param>
    /// <returns>Tuple of (WwiseVersion, UseFeedback)</returns>
    internal static (uint, bool) ReadWwiseHeaderInfo(Stream stream)
    {
        var initialPosition = stream.Position;
        var reader = new BinaryReader(stream, Encoding.UTF8);
        var firstHeader = new string(reader.ReadChars(4));

        // Mini header before BKHD on early versions
        if (firstHeader == "AKBK")
        {
            reader.BaseStream.Seek(sizeof(uint) * 2, SeekOrigin.Current);
            // TODO: Python code checks for endianness right here
            firstHeader = new string(reader.ReadChars(4));
        }
        
        if (firstHeader != "BKHD")
        {
            throw new Exception("Not a Wwise bank");
        }
        // Else bank header is "BKHD"

        reader.BaseStream.Seek(sizeof(uint), SeekOrigin.Current);
        var version = reader.ReadUInt32();

        if (version is 0 or 1)
        {
            // Actual version in very early banks
            reader.BaseStream.Seek(sizeof(uint), SeekOrigin.Current);
            version = reader.ReadUInt32();
        }
        
        // SoundBank ID
        reader.BaseStream.Seek(sizeof(uint), SeekOrigin.Current);
        
        // Language ID
        reader.BaseStream.Seek(sizeof(uint), SeekOrigin.Current);
        
        // Timestamp?
        if (version <= 26)
        {
            reader.BaseStream.Seek(sizeof(ulong), SeekOrigin.Current);
        }

        bool feedback = false;
        if (version >= 27 && version <= 126)
        {
            var value = reader.ReadUInt32();
            feedback = value != 0;
        }
        
        //TODO: Handle custom versions and strange variations
        //TODO: Handle slightly encrypted headers in LIMBO demo and World of Tanks

        stream.Seek(initialPosition, SeekOrigin.Begin);
        return (version, feedback);
    }
    
    internal static void SetBankHeaderPadding(WwiseBank bank)
    {
        if (bank.DATA is not null)
        {
            var serializer = new BinarySerializer();
            var context = new BankSerializationContext(bank.BKHD.BankGeneratorVersion, false, bank.BKHD.FeedbackInBank);
            bank.BKHD.Padding = new BankHeaderPadding();
            
            var bkhdSize= serializer.SizeOf(new ChunkContainer(bank.BKHD), context);
            var didxSize = (bank.DIDX != null) ? 
                serializer.SizeOf(new ChunkContainer(bank.DIDX), context) : 0;

            var dataOffset = bkhdSize + didxSize;
            bank.BKHD.Padding.SetPadding(dataOffset);
        }
    }
}