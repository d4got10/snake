using System.Diagnostics;
using Core;

const int width = 10;
const int height = 10;

var renderer = new ConsoleRenderer(width, height);
var inputProvider = new KeyboardInputProvider();
var game = new Game(width, width, renderer, inputProvider);

inputProvider.Init();
game.Init();

int tickNumber = 0;
Stopwatch stopwatch = new Stopwatch();
while (true)
{
    Thread.Sleep(Math.Max(1000 - (int)stopwatch.ElapsedMilliseconds, 10));
    stopwatch.Restart();
    inputProvider.Tick();
    game.Tick();
    Console.WriteLine($"Tick number {++tickNumber}");
    Console.WriteLine($"DeltaTime: {stopwatch.ElapsedMilliseconds / 1000f}");
    stopwatch.Stop();
}