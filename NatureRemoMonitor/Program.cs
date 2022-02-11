// See https://aka.ms/new-console-template for more information

using RemoClient = NatureRemoMonitor.API.Client;
using SheetClient = NatureRemoMonitor.SpreadSheet.Client;

var token = Environment.GetEnvironmentVariable("NATURE_REMO_ACCESS_TOKEN") ?? "";
var base64EncodedCredential = Environment.GetEnvironmentVariable("GOOGLE_SHEET_CREDENTIAL") ?? "";
var spreadsheetId = Environment.GetEnvironmentVariable("SPREADSHEET_ID_FOR_RECORD") ?? "";

if (new[] { token, base64EncodedCredential, spreadsheetId }.Any(s => string.IsNullOrEmpty(s)))
{
    Console.WriteLine("Environment Variables is not set.");
    Environment.Exit(1);
}

var remoClient = new RemoClient(new HttpClient(), token);
var devices = await remoClient.FetchNewestSensorValue();

var sheetClient = new SheetClient(base64EncodedCredential);

// 今のところ使っているのは1台だけ
var newestEvents = devices.Single().NewestEvents;

var insertRow = new List<object>()
{
    DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
    newestEvents.Temperature.Val,
    newestEvents.Humidity.Val,
    newestEvents.Illumination.Val,
};

sheetClient.Append(spreadsheetId, "SensorValues!A1:D", insertRow);
