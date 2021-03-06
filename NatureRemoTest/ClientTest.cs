using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using NatureRemoMonitor.API;
using NatureRemoMonitor.Exception;
using NUnit.Framework;

namespace NatureRemoTest;

public class ClientTest
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

    [Test]
    public async Task TestFetchNewestSensorValue()
    {
        var token = Environment.GetEnvironmentVariable("NATURE_REMO_ACCESS_TOKEN") ?? "";
        var client = new Client(new HttpClient(), token);

        var devices = await client.FetchNewestSensorValue();

        Assert.That(devices.Select(d => d.Name), Is.All.Not.Null);
    }
}
