using System.Collections.Concurrent;

namespace Core;

public class KeyboardInputProvider : IInputProvider, IGameSystem, IDisposable
{
    public KeyboardInputProvider()
    {
        _keyPressInputThread = new Thread(SavePressedKeys);
    }
    
    public int MoveDirection { get; private set; }


    private readonly ConcurrentQueue<ConsoleKey> _pressedKeys = new();
    private readonly Thread _keyPressInputThread;
    private bool _isDisposed;

    public void Init()
    {
        _keyPressInputThread.Start();
    }
    
    public void Tick()
    {
        while (!_pressedKeys.IsEmpty)
        {
            if (_pressedKeys.TryDequeue(out ConsoleKey key))
            {
                MoveDirection = key switch
                {
                    ConsoleKey.D => 0,
                    ConsoleKey.S => 1,
                    ConsoleKey.A => 2,
                    ConsoleKey.W => 3,
                    _ => MoveDirection
                };
            }
        }
    }

    public void Dispose()
    {
        if (_isDisposed) return;
        
        _isDisposed = true;
    }

    private void SavePressedKeys()
    {
        while(!_isDisposed)
            _pressedKeys.Enqueue(Console.ReadKey(true).Key);
    }
}