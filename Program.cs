using Core;

const int width = 10;
const int height = 10;

var game = new Game(width, width, new ConsoleRenderer(width, height));
game.Init();

int tickNumber = 0;
while (true)
{
    Thread.Sleep(1000);
    game.Tick();
    Console.WriteLine($"Tick number {++tickNumber}");
}