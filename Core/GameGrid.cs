namespace Core;

public class GameGrid
{
    public event Action<int, int, CellType>? CellChanged;
    
    public GameGrid(int width, int height)
    {
        Width = width;
        Height = height;
        _data = new CellType[Height, Width];
    }
    
    public int Width { get; }
    public int Height { get; }

    private readonly CellType[,] _data;

    public CellType Get(int column, int row) => _data[row, column];
    public void Set(int column, int row, CellType type)
    {
        _data[row, column] = type;
        CellChanged?.Invoke(column, row, type);
    }
}