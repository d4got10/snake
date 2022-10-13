namespace Core;

public class ConsoleRenderer : IGameRenderer
{
    public ConsoleRenderer(int width, int height)
    {
        _width = width;
        _height = height;
    }
    
    private readonly int _width;
    private readonly int _height;
    private readonly Queue<(int, int, int)> _commandQueue = new();

    private readonly Dictionary<int, (char, ConsoleColor)> _pixelDataMap = new()
    {
        {0, (' ', ConsoleColor.Black)},
        {1, ('S', ConsoleColor.DarkGreen)},
        {2, ('A', ConsoleColor.Red)},
        {3, ('W', ConsoleColor.Gray)},
        {4, ('H', ConsoleColor.Green)},
    };

    public void Init()
    {
        Console.Clear();
        Console.CursorVisible = false;

        for (int row = 0; row < _height; row++)
        {
            for (int column = 0; column < _width; column++)
            {
                SetPixel(column, row, 0);
            }
        }
    }

    public void SetPixel(int column, int row, int data)
    {
        _commandQueue.Enqueue((column, row, data));
    }

    public void Draw()
    {
        while (_commandQueue.Count > 0)
        {
            var (column, row, data) = _commandQueue.Dequeue();
            Console.SetCursorPosition(2 * column, row);
            Console.BackgroundColor = _pixelDataMap[data].Item2;
            Console.Write(_pixelDataMap[data].Item1 + " ");
        }

        Console.SetCursorPosition(0, _height);
        Console.BackgroundColor = ConsoleColor.Black;
    }
}