namespace Core;

public interface IScoreHandler
{
    int Current { get; }
    void Increase();
}