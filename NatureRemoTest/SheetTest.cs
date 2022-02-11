using System;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using NUnit.Framework;

namespace NatureRemoTest;

public class SheetTest
{
    [Test]
    public void ReadSample()
    {
        var base64EncodedCredential = Environment.GetEnvironmentVariable("GOOGLE_SHEET_CREDENTIAL") ?? "";

        using var stream = new MemoryStream(Convert.FromBase64String(base64EncodedCredential));

        var credential = GoogleCredential.FromStream(stream)
            .CreateScoped(SheetsService.Scope.DriveReadonly).UnderlyingCredential;

        var service = new SheetsService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = "Sheet Api Sample",
        });

        var spreadsheetId = Environment.GetEnvironmentVariable("TEST_SPREADSHEET_ID") ?? "";
        var range = "Sheet1!A1:B2";

        var request = service.Spreadsheets.Values.Get(spreadsheetId, range);
        var response = request.Execute();
        var values = response.Values;

        Assert.That(values, Is.EquivalentTo(new[] { new[] { "a1", "b1" }, new[] { "a2", "b2" } }));
    }
}
