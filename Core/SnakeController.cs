namespace Core;

public class SnakeController : IGameSystem
{
    public SnakeController(GameGrid gameGrid, IInputProvider inputProvider)
    {
        _gameGrid = gameGrid;
        _inputProvider = inputProvider;
        _headPosition = new Vector2
        {
            X = _gameGrid.Width / 2,
            Y = _gameGrid.Height / 2
        };
    }

    private readonly GameGrid _gameGrid;
    private readonly IInputProvider _inputProvider;

    private Vector2 _headPosition;
    
    public void Tick()
    {
        _gameGrid.Set(_headPosition.X, _headPosition.Y, CellType.Empty);
        HandleInput();
        _gameGrid.Set(_headPosition.X, _headPosition.Y, CellType.Snake);
    }

    private void HandleInput()
    {
        switch (_inputProvider.MoveDirection)
        {
            case 0:
                _headPosition.X++;
                break;
            case 1:
                _headPosition.Y++;
                break;
            case 2:
                _headPosition.X--;
                break;
            case 3:
                _headPosition.Y--;
                break;
        }
    }
}