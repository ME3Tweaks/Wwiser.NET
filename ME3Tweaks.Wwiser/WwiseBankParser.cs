using System;
using System.IO;
using System.Text;
using BinarySerialization;
using ME3Tweaks.Wwiser.Model;

namespace ME3Tweaks.Wwiser;

public class WwiseBankParser
{
    public uint Version { get; private set; }
    public bool UseFeedback { get; private set; }
    public WwiseBank? WwiseBank { get; private set; }
    
    private BinarySerializer _serializer;
    private Stream _stream;
    
    public WwiseBankParser(string fileName)
    {
        _serializer = new BinarySerializer();
        if (!File.Exists(fileName))
        {
            throw new FileNotFoundException("Bank file does not exist", fileName);
        }

        _stream = File.OpenRead(fileName);
        (Version, UseFeedback) = ReadWwiseHeaderInfo(_stream);
        _stream.Position = 0;
    }

    public WwiseBankParser(Stream stream)
    {
        _serializer = new BinarySerializer();
        _stream = stream;
        (Version, UseFeedback) = ReadWwiseHeaderInfo(_stream);
        _stream.Position = 0;
    }

    public void ConvertVersion(uint version)
    {
        ConvertWithHeader(CreateSerializationContext() with { Version = version });
    }

    public void ConvertWithHeader(BankSerializationContext context)
    {
        if (WwiseBank is null)
        {
            throw new InvalidOperationException("Cannot set header data with no bank.");
        }
        
        var converter = new WwiseBankConverter(CreateSerializationContext(), context);
        converter.ConvertBank(WwiseBank);
        
        Version = context.Version;
        UseFeedback = context.UseFeedback;
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
    /// <returns></returns>
    private (uint, bool) ReadWwiseHeaderInfo(Stream stream)
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

        stream.Position = initialPosition;
        return (version, feedback);
    }

    public async Task Deserialize()
    {
        if (_stream is null or { Length : 0 })
        {
            throw new Exception("Inner stream is null or empty, cannot deserialize.");
        }
        
        _stream.Position = 0;
        var root = await _serializer
            .DeserializeAsync<WwiseBankRoot>(_stream, CreateSerializationContext());
        WwiseBank = new WwiseBank(root);
    }

    public async Task Serialize(Stream stream)
    {
        if (WwiseBank is null)
        {
            throw new InvalidOperationException("Cannot serialize a null WwiseBank");
        }
        await _serializer.SerializeAsync(stream,WwiseBank.ToModel(), CreateSerializationContext());
    }

    private BankSerializationContext CreateSerializationContext()
    {
        return new BankSerializationContext(Version: Version, UseModulator: false, UseFeedback: UseFeedback);
    }
}