using NatureRemoMonitor.API;
using NUnit.Framework;

namespace NatureRemoTest;

public class SnakeCaseNamingPolicyTest
{
    [TestCase("Name", "name")]
    [TestCase("CreatedAt", "created_at")]
    [Test]
    public void ConvertTest(string from, string to)
    {
        Assert.That(new SnakeCaseNamingPolicy().ConvertName(from), Is.EqualTo(to));
    }
}
