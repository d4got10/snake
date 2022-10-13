namespace Core;

public class ConsoleRenderer : IGameRenderer
{
    public ConsoleRenderer(int width, int height)
    {
        _width = width;
        _height = height;
        _frame = new char[height, width];
    }
    
    private readonly int _width;
    private readonly int _height;
    private readonly char[,] _frame;

    private readonly Dictionary<int, char> _symbolsMap = new()
    {
        {0, ' '},
        {1, 'S'},
        {2, 'A'},
        {3, 'W'},
        {4, 'H'},
    };
    
    private readonly Dictionary<char, ConsoleColor> _colorsMap = new()
    {
        {' ', ConsoleColor.Black},
        {'S', ConsoleColor.DarkGreen},
        {'A', ConsoleColor.Red},
        {'W', ConsoleColor.Gray},
        {'H', ConsoleColor.Green},
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
        _frame[row, column] = _symbolsMap[data];
    }

    public void Draw()
    {
        Console.SetCursorPosition(0, 0);

        for (int row = 0; row < _height; row++)
        {
            for (int column = 0; column < _width; column++)
            {
                Console.BackgroundColor = _colorsMap[_frame[row, column]];
                Console.Write(_frame[row, column] + " ");
            }
            Console.WriteLine();
        }

        Console.BackgroundColor = ConsoleColor.Black;
    }
}