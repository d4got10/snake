namespace Core;

public class ScoreHandler : IScoreHandler
{
    public int Current { get; private set; }
    
    public void Increase()
    {
        Current++;
    }
}