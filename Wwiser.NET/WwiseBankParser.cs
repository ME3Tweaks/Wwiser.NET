using System.Text;
using BinarySerialization;
using ME3Tweaks.Wwiser.Model;

namespace ME3Tweaks.Wwiser;

public class WwiseBankParser
{
    public uint Version { get; set; }
    public WwiseBank? WwiseBank { get; private set; }
    
    private BinarySerializer _serializer;
    private string _fileName;
    private Stream _stream;
    
    public WwiseBankParser(string fileName)
    {
        _serializer = new BinarySerializer();
        _fileName = fileName;
        if (!File.Exists(fileName))
        {
            throw new FileNotFoundException("Bank file does not exist", fileName);
        }

        _stream = File.OpenRead(_fileName);
        Version = GetWwiseVersionNumber(_stream);
        _stream.Position = 0;
    }

    public WwiseBankParser()
    {
        
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
    public uint GetWwiseVersionNumber(Stream stream)
    {
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
        
        //TODO: Handle custom versions and strange variations
        
        //TODO: Handle slightly encrypted headers in LIMBO demo and World of Tanks

        stream.Position = 0;
        return version;
    }

    public async void Deserialize()
    {
        if (_stream is null or { Length : 0 })
        {
            throw new Exception("Inner stream is null or empty, cannot deserialize.");
        }
        
        _stream.Position = 0;
        WwiseBank = await _serializer
            .DeserializeAsync<WwiseBank>(_stream, new BankSerializationContext(Version));
    }
}