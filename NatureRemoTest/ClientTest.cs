using System.Net.Http;
using NatureRemoMonitor.API;
using NatureRemoMonitor.Exception;
using NUnit.Framework;

namespace NatureRemoTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    [Test]
    public void TestTokenMustNotBeEmpty(string token)
    {
        Assert.That(() => new Client(new HttpClient(), token), Throws.TypeOf<InvalidArgumentException>());
    }
}
