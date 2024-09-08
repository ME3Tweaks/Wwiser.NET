namespace ME3Tweaks.Wwiser.Tests.FormatTests;

public class BankHeaderPaddingTests
{
    [TestCase(8)]
    [TestCase(24)]
    [TestCase(0x158)]
    public void DataProperlyAligned_PaddingIsZero(long dataOffset)
    {
        var padding = new BankHeaderPadding();
        padding.SetPadding(dataOffset);
        
        Assert.That(padding.Padding.Length, Is.EqualTo(0));
    }
    
    [TestCase(0, 8)]
    [TestCase(4, 4)]
    [TestCase(33, 7)]
    public void DataNotAligned_PaddingAlignsProperly(long dataOffset, long expectedPadding)
    {
        var padding = new BankHeaderPadding();
        padding.SetPadding(dataOffset);
        
        Assert.That(padding.Padding.Length, Is.LessThanOrEqualTo(8));
        Assert.That(padding.Padding.Length, Is.EqualTo(expectedPadding));
    }
    
    [TestCase(15, 9)]
    [TestCase(12, 12)]
    [TestCase(0x15E, 10)]
    public void DataNotAligned_PaddingProperlyOffsetBy8(long dataOffset, long expectedPadding)
    {
        var padding = new BankHeaderPadding();
        padding.SetPadding(dataOffset);
        
        Assert.That(padding.Padding.Length, Is.GreaterThan(8));
        Assert.That(padding.Padding.Length, Is.EqualTo(expectedPadding));
    }
}