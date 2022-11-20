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
        var testServer = SelectTestServer();
        var serverOption = "";
        if (testServer != null)
        {
            serverOption = $" --server-id={testServer.Id}";
            Console.WriteLine($"Test Server: {testServer.Name}");
        }

        // このプロジェクト自身を実行しようとしてしまわないよう、絶対パスで指定する
        var startInfo = new ProcessStartInfo("/opt/homebrew/bin/speedtest")
        {
            // スペースを含まない方法でオプションを指定する
            // -f、-pだと「\"\"-f json\"\"」のように書かなければならず、見通しが悪くなるため
            Arguments = $"--format=json --progress=no{serverOption}",
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

    /// <summary>
    /// 不安定なテストサーバーを使わないようにする
    /// </summary>
    /// <returns></returns>
    private Server? SelectTestServer()
    {
        // このプロジェクト自身を実行しようとしてしまわないよう、絶対パスで指定する
        var startInfo = new ProcessStartInfo("/opt/homebrew/bin/speedtest")
        {
            // スペースを含まない方法でオプションを指定する
            // -f、-pだと「\"\"-f json\"\"」のように書かなければならず、見通しが悪くなるため
            Arguments = "--servers --format=json",
            UseShellExecute = false,
            RedirectStandardOutput = true,
        };

        using var process = new Process
        {
            StartInfo = startInfo
        };

        ServerList? serverList;

        process.Start();

        var stdout = process.StandardOutput.ReadToEnd();
        serverList = JsonSerializer.Deserialize<ServerList>(stdout, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return serverList?.Servers.SingleOrDefault(s => s.Name == "Verizon");
    }
}
