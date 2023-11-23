using ME3Tweaks.Wwiser.Model.Hierarchy;

namespace ME3Tweaks.Wwiser.Tests.HierarchyTests;

public class HircTypeTests
{
    [Test]
    public void ToHircType_ReturnsSameEnumMeaning()
    {
        Assert.Multiple(() =>
        {
            Assert.That(HircType128.Action.ToHircType(), Is.EqualTo(HircType.Action));
            Assert.That(HircType128.DialogueEvent.ToHircType(), Is.EqualTo(HircType.DialogueEvent));
            Assert.That(HircType128.FxShareSet.ToHircType(), Is.EqualTo(HircType.FxShareSet));
        });
    }
    
    [Test]
    public void ToHircType128_ReturnsSameEnumMeaning()
    {
        Assert.Multiple(() =>
        {
            Assert.That(HircType.Action.ToHircType128(), Is.EqualTo(HircType128.Action));
            Assert.That(HircType.DialogueEvent.ToHircType128(), Is.EqualTo(HircType128.DialogueEvent));
            Assert.That(HircType.FxShareSet.ToHircType128(), Is.EqualTo(HircType128.FxShareSet));
        });
    }

    [Test]
    public void ToHircType128_UnconvertableType_ThrowsException()
    {
        Assert.Throws<NotSupportedException>(() => { HircType.FeedbackBus.ToHircType128(); });
        Assert.Throws<NotSupportedException>(() => { HircType.FeedbackNode.ToHircType128(); });
    }
    
    [Test]
    public void ToHircType_UnconvertableType_ThrowsException()
    {
        Assert.Throws<NotSupportedException>(() => { HircType128.TimeMod.ToHircType(); });
    }
}