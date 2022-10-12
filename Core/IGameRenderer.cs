namespace Core;

public interface IGameRenderer
{
    void Init();
    void SetPixel(int column, int row, int data);
    void Draw();
}