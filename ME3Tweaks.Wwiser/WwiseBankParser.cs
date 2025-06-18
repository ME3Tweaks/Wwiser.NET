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
        var mapper = new WwiseBankMapper();
        
        var root = serializer.Deserialize<WwiseBankRoot>(stream, context);
        return mapper.MapToBank(root);
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
        var mapper = new WwiseBankMapper();
        
        return serializer.DeserializeAsync<WwiseBankRoot>(stream, context)
            .ContinueWith(t => mapper.MapToBank(t.Result));
    }

    public static void Serialize(WwiseBank bank, Stream stream)
    {
        var serializer = new BinarySerializer();
        var mapper = new WwiseBankMapper();
        
        serializer.Serialize(stream, mapper.MapToRoot(bank), BankSerializationContext.FromBank(bank));
    }

    public static Task SerializeAsync(WwiseBank bank, Stream stream, CancellationToken cancellationToken = default)
    {
        var serializer = new BinarySerializer();
        var mapper = new WwiseBankMapper();
        
        return serializer.SerializeAsync(stream, mapper.MapToRoot(bank), BankSerializationContext.FromBank(bank), cancellationToken);
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
    /// <param name="resetStreamPosition">If true, resets the stream back to the initial position after reading header. Default: True</param>
    /// <returns>Tuple of (WwiseVersion, UseFeedback)</returns>
    public static (uint, bool) ReadWwiseHeaderInfo(Stream stream, bool resetStreamPosition = true)
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
        if (version is >= 27 and <= 126)
        {
            var value = reader.ReadUInt32();
            feedback = value != 0;
        }
        
        //TODO: Handle custom versions and strange variations
        //TODO: Handle slightly encrypted headers in LIMBO demo and World of Tanks

        if(resetStreamPosition) stream.Seek(initialPosition, SeekOrigin.Begin);
        return (version, feedback);
    }
}