using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;

namespace NatureRemoMonitor.SpreadSheet;

public class Client
{
    private readonly SheetsService _sheetsService;

    public Client(string base64EncodedCredential)
    {
        using var stream = new MemoryStream(Convert.FromBase64String(base64EncodedCredential));

        var credential = GoogleCredential.FromStream(stream)
            .CreateScoped(SheetsService.Scope.DriveReadonly).UnderlyingCredential;

        _sheetsService = new SheetsService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = "life_log",
        });
    }

    public IEnumerable<IEnumerable<object>> Read(string spreadsheetId, string range)
    {
        var request = _sheetsService.Spreadsheets.Values.Get(spreadsheetId, range);
        var response = request.Execute();

        return response.Values;
    }
}
