namespace Core;

public class ConsoleRenderer : IGameRenderer
{
    public ConsoleRenderer(int width, int height)
    {
        _width = width;
        _height = height;
        _frame = new char[height, width];
    }
    
    private int _width;
    private int _height;
    private char[,] _frame;

    private Dictionary<int, char> _symbolsMap = new()
    {
        {0, ' '},
        {1, 'S'},
        {2, 'A'},
        {3, 'W'}
    };

    public void Init()
    {
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
        Console.Clear();
        for (int row = 0; row < _height; row++)
        {
            for (int column = 0; column < _width; column++)
            {
                Console.Write(_frame[row, column]);
            }
            Console.WriteLine();
        }
    }
}