﻿// See https://aka.ms/new-console-template for more information

using RemoClient = NatureRemoMonitor.API.Client;
using SheetClient = SpreadSheet.Client;

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

var sheetClient = new SheetClient(base64EncodedCredential);

var newestEvents = devices.Single(d => d.Name == "Remo 2nd").NewestEvents;

var utcNow = DateTime.UtcNow;
var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
var jstNow = TimeZoneInfo.ConvertTimeFromUtc(utcNow, timeZoneInfo);

var insertRow = new List<object>()
{
    jstNow.ToString("yyyy/MM/dd HH:mm:ss"),
    newestEvents.Temperature.Val,
    newestEvents.Humidity.Val,
    newestEvents.Illumination.Val,
};

sheetClient.Append(spreadsheetId, "SensorValues!A1:D", insertRow);
