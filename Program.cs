using System.Diagnostics;
using Core;

int width = args.Length > 0 ? int.Parse(args[0]) : 10;
int height = args.Length > 1 ? int.Parse(args[1]) : 10;
int targetFrameRate = args.Length > 2 ? int.Parse(args[2]) : 2;

var renderer = new ConsoleRenderer(width, height);
var inputProvider = new KeyboardInputProvider();
var game = new Game(width, height, renderer, inputProvider);

inputProvider.Init();
game.Init();

Console.WriteLine("Press Enter to play");
Console.ReadLine();
Console.Clear();

int tickNumber = 0;
Stopwatch stopwatch = new Stopwatch();
while (!game.IsOver)
{
    Thread.Sleep(Math.Max(1000 / targetFrameRate - (int)stopwatch.ElapsedMilliseconds, 0));
    stopwatch.Restart();
    inputProvider.Tick();
    game.Tick();
    Console.WriteLine($"Tick number {++tickNumber}");
    Console.WriteLine($"DeltaTime: {stopwatch.ElapsedMilliseconds / 1000f}");
    Console.WriteLine($"Total Score: {game.Score}");
    stopwatch.Stop();
}

inputProvider.Dispose();
Console.WriteLine("GAME OVER!");
Console.ReadLine();