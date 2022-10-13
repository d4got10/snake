namespace Core;

public class AppleFactory : IGameSystem
{
    private readonly GameGrid _gameGrid;

    public AppleFactory(GameGrid gameGrid)
    {
        _gameGrid = gameGrid;
    }
    
    private Vector2 _applePosition;
    
    public void Tick()
    {
        if (AppleIsEaten())
        {
            GenerateApple();
        }
    }

    private void GenerateApple()
    {
        var random = new Random();
        var positions = GetEmptyCellPositions();
        _applePosition = positions[random.Next(positions.Count)];
        _gameGrid.Set(_applePosition.X, _applePosition.Y, CellType.Apple);
    }

    private List<Vector2> GetEmptyCellPositions()
    {
        var positions = new List<Vector2>();
        for (int row = 0; row < _gameGrid.Height; row++)
        {
            for (int column = 0; column < _gameGrid.Width; column++)
            {
                if(_gameGrid.Get(column, row) == CellType.Empty)
                    positions.Add(new Vector2(column, row));
            }
        }

        return positions;
    }

    private bool AppleIsEaten()
    {
        return _gameGrid.Get(_applePosition.X, _applePosition.Y) != CellType.Apple;
    }
}