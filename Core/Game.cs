namespace Core;

public class Game : IGameLoseHandler
{
    public Game(int width, int height, IGameRenderer renderer, IInputProvider inputProvider)
    {
        _gameGrid = new GameGrid(width, height);
        _renderer = renderer;
        _scoreHandler = new ScoreHandler();
        _gameSystems = new IGameSystem[]
        {
            new SnakeController(_gameGrid, inputProvider, this, _scoreHandler),
            new AppleFactory(_gameGrid)
        };
    }
    
    public bool IsOver { get; private set; }
    public int Score => _scoreHandler.Current;

    private readonly GameGrid _gameGrid;
    private readonly IGameRenderer _renderer;
    private readonly IGameSystem[] _gameSystems;
    private readonly IScoreHandler _scoreHandler;

    public void Init()
    {
        _renderer.Init();
        _gameGrid.CellChanged += OnCellChanged;
        CreateSurroundingWalls();
    }

    public void Tick()
    {
        if (IsOver)
            throw new InvalidOperationException("Game is over and should not be updated");
        
        foreach(var gameSystem in _gameSystems)
            gameSystem.Tick();
        _renderer.Draw();
    }

    public void Lose()
    {
        IsOver = true;
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