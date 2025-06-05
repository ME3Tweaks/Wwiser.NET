using ME3Tweaks.Wwiser.Model.ParameterNode.Positioning;

namespace ME3Tweaks.Wwiser.Tests.ParameterNodeTests;

public class SpatializationModeTests
{
    private static readonly byte[] Data = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 16, 32, 64, 128];

    [TestCase(122)]
    [TestCase(126)]
    [TestCase(129)]
    public void GetHasAutomationFromSpatializationMode_BehaviorMatchesWwiser(int version)
    {
        var uVersion = (uint)version;
        Assert.Multiple(() =>
        {
            foreach (var d in Data)
            {
                var originalResult = OriginalGetHasAutomation(d, false, uVersion);
                var mode = SpatializationHelpers.GetModeFromByte(d, uVersion);
                var newResult = SpatializationHelpers.GetHasAutomationFromMode(mode, false, uVersion);
                Assert.That(newResult, Is.EqualTo(originalResult), $"{d} returned {newResult}");
            }
        });
    }

    [TestCase(122)]
    [TestCase(126)]
    [TestCase(129)]
    public void GetSpatializationModeWithAutomation_IsProperlyReadByWwiser(int version)
    {
        var uVersion = (uint)version;
        Assert.Multiple(() =>
        {
            foreach (var d in Data)
            {
                var s = (SpatializationMode)d;
                var hasAutomation = SpatializationHelpers.GetModeFromHasAutomation(true, s, uVersion);
                var noAutomation = SpatializationHelpers.GetModeFromHasAutomation(false, s, uVersion);

                var hasAutoByte = SpatializationHelpers.GetByteFromMode(hasAutomation, uVersion);
                var noAutoByte = SpatializationHelpers.GetByteFromMode(noAutomation, uVersion);
                
                Assert.That(OriginalGetHasAutomation(hasAutoByte, false, uVersion), Is.True, $"{d} True");
                Assert.That(OriginalGetHasAutomation(noAutoByte, false, uVersion), Is.False, $"{d} False");
            }
        });
    }


    /// <summary>
    /// Original implementation from Wwiser python code
    /// </summary>
    /// <param name="uBits3D"></param>
    /// <param name="initialAutomation"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    private static bool OriginalGetHasAutomation(byte uBits3D, bool initialAutomation, uint version)
    {
        if (version <= 122)
        {
            return ((uBits3D >> 0) & 3) != 1;
        } 
        if (version <= 126)
        {
            return ((uBits3D >> 4) & 1) != 1;
        }
        if (version <= 129)
        {
            return ((uBits3D >> 6) & 1) != 1;
        }
        return initialAutomation;
    }
}