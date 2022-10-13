namespace Core;

public class SnakeController : IGameSystem
{
    public SnakeController(GameGrid gameGrid, IInputProvider inputProvider, IGameLoseHandler loseHandler, IScoreHandler scoreHandler)
    {
        _gameGrid = gameGrid;
        _inputProvider = inputProvider;
        _loseHandler = loseHandler;
        _scoreHandler = scoreHandler;
        _headPosition = new Vector2
        {
            X = _gameGrid.Width / 2,
            Y = _gameGrid.Height / 2
        };
        _tailPositions = new List<Vector2>
        {
            _headPosition + new Vector2(-1, 0),
            _headPosition + new Vector2(-2, 0),
            _headPosition + new Vector2(-3, 0)
        };
    }

    private readonly GameGrid _gameGrid;
    private readonly IInputProvider _inputProvider;
    private readonly IGameLoseHandler _loseHandler;
    private readonly IScoreHandler _scoreHandler;
    private readonly List<Vector2> _tailPositions;
    
    private Vector2 _prevDirection;
    private Vector2 _headPosition;
    
    public void Tick()
    {
        var prevPosition = _headPosition;
        var direction = GetDirectionFromInput();
        ChangeHeadPosition(prevPosition + direction, prevPosition);
        _prevDirection = direction;
    }

    private Vector2 GetDirectionFromInput()
    {
        switch (_inputProvider.MoveDirection)
        {
            case 0 when _prevDirection.X >= 0: return new Vector2(1, 0);
            case 1 when _prevDirection.Y >= 0: return new Vector2(0, 1);
            case 2 when _prevDirection.X <= 0: return new Vector2(-1, 0);
            case 3 when _prevDirection.Y <= 0: return new Vector2(0, -1);
            default: return _prevDirection;
        }
    }

    private void ChangeHeadPosition(Vector2 newPosition, Vector2 prevPosition)
    {
        var cellType = _gameGrid.Get(newPosition.X, newPosition.Y);
        switch (cellType)
        {
            case CellType.Snake:
                _loseHandler.Lose();
                return;
            case CellType.Apple:
                _tailPositions.Add(_tailPositions[^1]);
                _scoreHandler.Increase();
                break;
            case CellType.Wall:
                _loseHandler.Lose();
                return;
        }
        
        _headPosition = newPosition;
        _gameGrid.Set(prevPosition.X, prevPosition.Y, CellType.Empty);
        _gameGrid.Set(newPosition.X, newPosition.Y, CellType.SnakeHead);
        OnHeadChangedPosition(prevPosition);
    }

    private void OnHeadChangedPosition(Vector2 prevPosition)
    {
        var position = prevPosition;
        
        var lastPosition = _tailPositions[^1];
        _gameGrid.Set(lastPosition.X, lastPosition.Y, CellType.Empty);
        
        for (int i = 0; i < _tailPositions.Count; i++)
        {
            _gameGrid.Set(position.X, position.Y, CellType.Snake);
            (position, _tailPositions[i]) = (_tailPositions[i], position);
        }
    }
}