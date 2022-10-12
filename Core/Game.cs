namespace Core;

public class Game
{
    public Game(int width, int height, IGameRenderer renderer)
    {
        _gameGrid = new GameGrid(width, height);
        _renderer = renderer;
        var inputProvider = new KeyboardInputProvider();
        _gameSystems = new IGameSystem[]
        {
            inputProvider,
            new SnakeController(_gameGrid, inputProvider)
        };
    }

    private readonly GameGrid _gameGrid;
    private readonly IGameRenderer _renderer;
    private readonly IGameSystem[] _gameSystems;

    public void Init()
    {
        _renderer.Init();
        _gameGrid.CellChanged += OnCellChanged;
        CreateSurroundingWalls();
    }

    public void Tick()
    {
        foreach(var gameSystem in _gameSystems)
            gameSystem.Tick();
        _renderer.Draw();
    }

    private void OnCellChanged(int column, int row, CellType type)
    {
        _renderer.SetPixel(column, row, (int)type);
    }

    private void CreateSurroundingWalls()
    {
        for (int row = 0; row < _gameGrid.Height; row++)
        {
            for (int column = 0; column < _gameGrid.Width; column++)
            {
                if (column == 0 || row == 0 || column == _gameGrid.Width - 1 || row == _gameGrid.Height - 1)
                {
                    _gameGrid.Set(column, row, CellType.Wall);
                }
            }
        }
    }
}