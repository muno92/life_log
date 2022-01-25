using System.Text.Json;
using System.Text.RegularExpressions;

namespace NatureRemoMonitor.API;

public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        return string.Join('_', Regex.Matches(name, "[A-Z][a-z]+").Select(m => m.Value.ToLower()));
    }
}
