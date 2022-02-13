using System.Diagnostics;
using System.Text.Json;
using SpeedTest.Resource;

namespace SpeedTest;

public class Executor
{
    /// <summary>
    /// SpeedTest CLIを実行する
    /// </summary>
    /// <returns>実行結果のJSONをデシリアライズしたオブジェクト</returns>
    public Output? Exec()
    {
        // このプロジェクト自身を実行しようとしてしまわないよう、絶対パスで指定する
        var startInfo = new ProcessStartInfo("/opt/homebrew/bin/speedtest")
        {
            // スペースを含まない方法でオプションを指定する
            // -f、-pだと「\"\"-f json\"\"」のように書かなければならず、見通しが悪くなるため
            Arguments = "--format=json --progress=no",
            UseShellExecute = false,
            RedirectStandardOutput = true,
        };

        using var process = new Process
        {
            StartInfo = startInfo
        };

        Output? output;

        process.Start();
        Console.WriteLine("Process start.");

        var stdout = process.StandardOutput.ReadToEnd();
        output = JsonSerializer.Deserialize<Output>(stdout, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        process.WaitForExit();
        Console.WriteLine("Process finished.");

        return output;
    }
}
