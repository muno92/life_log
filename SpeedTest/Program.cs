// See https://aka.ms/new-console-template for more information

using SpeedTest;

var output = new Executor().Exec();
if (output == null)
{
    Console.WriteLine("SpeedTest Result is null.");
    Environment.Exit(1);
}

Console.WriteLine(output.Download.Bandwidth * 8 / 1000 / 1000);
Console.WriteLine(output.Upload.Bandwidth * 8 / 1000 / 1000);
