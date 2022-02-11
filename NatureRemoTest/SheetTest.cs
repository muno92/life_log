using System;
using NatureRemoMonitor.SpreadSheet;
using NUnit.Framework;

namespace NatureRemoTest;

public class SheetTest
{
    [Test]
    public void ReadTest()
    {
        var base64EncodedCredential = Environment.GetEnvironmentVariable("GOOGLE_SHEET_CREDENTIAL") ?? "";
        var client = new Client(base64EncodedCredential);

        var spreadsheetId = Environment.GetEnvironmentVariable("TEST_SPREADSHEET_ID") ?? "";
        var range = "Sheet1!A1:B2";

        var values = client.Read(spreadsheetId, range);

        Assert.That(values, Is.EquivalentTo(new[] { new[] { "a1", "b1" }, new[] { "a2", "b2" } }));
    }
}
