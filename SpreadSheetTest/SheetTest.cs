using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SpreadSheet;

namespace SpreadSheetTest;

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

    [Test]
    public void WriteTest()
    {
        var base64EncodedCredential = Environment.GetEnvironmentVariable("GOOGLE_SHEET_CREDENTIAL") ?? "";
        var client = new Client(base64EncodedCredential);

        var spreadsheetId = Environment.GetEnvironmentVariable("TEST_SPREADSHEET_ID") ?? "";
        var range = "Sheet2!A1:D";

        var valuesBeforeInsert = client.Read(spreadsheetId, range);

        var insertRow = new List<object>() { DateTime.Now.ToString("yyyy/MM/dd H:mm:ss"), "20.5", "32", "0" };

        // 元がdoubleだとしてもスプレッドシートから取得すると文字列になる。
        // 追記出来ているか確認出来れば良いので、文字列で追加・検証する
        client.Append(spreadsheetId, range, insertRow);

        var valuesAfterInsert = client.Read(spreadsheetId, range);

        Assert.That(valuesAfterInsert, Is.EquivalentTo(valuesBeforeInsert.Append(insertRow)));
    }
}
