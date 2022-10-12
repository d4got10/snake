namespace Core;

public class KeyboardInputProvider : IInputProvider, IGameSystem
{
    public int MoveDirection { get; private set; }
    
    public void Tick()
    {
        if (Console.ReadKey(false).Key == ConsoleKey.D)
            MoveDirection = 0;
        else if (Console.ReadKey(false).Key == ConsoleKey.S)
            MoveDirection = 1;
        else if (Console.ReadKey(false).Key == ConsoleKey.A)
            MoveDirection = 2;
        else if (Console.ReadKey(false).Key == ConsoleKey.W)
            MoveDirection = 3;
    }
}