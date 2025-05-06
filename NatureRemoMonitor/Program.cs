// See https://aka.ms/new-console-template for more information

using NatureRemoMonitor.Database;
using RemoClient = NatureRemoMonitor.API.Client;

var token = Environment.GetEnvironmentVariable("NATURE_REMO_ACCESS_TOKEN") ?? "";
var base64EncodedCredential = Environment.GetEnvironmentVariable("GOOGLE_SHEET_CREDENTIAL") ?? "";
var spreadsheetId = Environment.GetEnvironmentVariable("SPREADSHEET_ID_FOR_REMO") ?? "";

if (new[] { token, base64EncodedCredential, spreadsheetId }.Any(s => string.IsNullOrEmpty(s)))
{
    Console.WriteLine("Environment Variables is not set.");
    Environment.Exit(1);
}

var remoClient = new RemoClient(new HttpClient(), token);
var devices = await remoClient.FetchNewestSensorValue();

var device = devices.Single(d => d.Name == "Remo 2nd");
var newestEvents = device.NewestEvents;

await using var db = new PostgresContext();
var sensorValue = new SensorValue()
{
    DeviceId = new Guid(device.Id),
    Temperature = newestEvents.Temperature.Val,
    Humidity = newestEvents.Humidity.Val,
    Illumination = newestEvents.Illumination.Val,
};

db.SensorValues.Add(sensorValue);
await db.SaveChangesAsync();
