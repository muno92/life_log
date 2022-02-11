using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace NatureRemoMonitor.SpreadSheet;

public class Client
{
    private readonly SheetsService _sheetsService;

    public Client(string base64EncodedCredential)
    {
        using var stream = new MemoryStream(Convert.FromBase64String(base64EncodedCredential));

        var credential = GoogleCredential.FromStream(stream)
            .CreateScoped(SheetsService.Scope.Spreadsheets).UnderlyingCredential;

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

    public void Append(string spreadsheetId, string range, IList<object> row)
    {
        var value = new ValueRange { Values = new List<IList<object>> { row } };
        var request = _sheetsService.Spreadsheets.Values.Append(value, spreadsheetId, range);
        //RAWでも表示上は問題無いが、セルの中身を見たときに文字列の先頭にシングルクオートがついていた
        request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

        request.Execute();
    }
}
