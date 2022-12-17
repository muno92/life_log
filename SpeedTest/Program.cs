// See https://aka.ms/new-console-template for more information

using SpeedTest;
using SpreadSheet;

var base64EncodedCredential = Environment.GetEnvironmentVariable("GOOGLE_SHEET_CREDENTIAL") ?? "";
var spreadsheetId = Environment.GetEnvironmentVariable("SPREADSHEET_ID_FOR_SPEED_TEST") ?? "";

if (new[] { base64EncodedCredential, spreadsheetId }.Any(s => string.IsNullOrEmpty(s)))
{
    Console.WriteLine("Environment Variables is not set.");
    Environment.Exit(1);
}

var output = new Executor().Exec();
if (output == null)
{
    Console.WriteLine("SpeedTest Result is null.");
    Environment.Exit(1);
}

var utcNow = DateTime.UtcNow;
var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
var jstNow = TimeZoneInfo.ConvertTimeFromUtc(utcNow, timeZoneInfo);

var insertRow = new List<object>()
{
    jstNow.ToString("yyyy/MM/dd HH:mm:ss"),
    output.Ping.Jitter.ToString("F"),
    output.Ping.Latency.ToString("F"),
    output.PacketLoss.ToString("F"),
    ((double)output.Download.Bandwidth * 8 / 1000 / 1000).ToString("F"),
    ((double)output.Upload.Bandwidth * 8 / 1000 / 1000).ToString("F"),
    output.Server.Name,
};

// dummy
var sheetClient = new Client(base64EncodedCredential);
sheetClient.Append(spreadsheetId, "Sheet1!A1:F", insertRow);
