namespace Core;

public struct Vector2
{
    public Vector2(int x, int y)
    {
        X = x;
        Y = y;
    }
    public int X;
    public int Y;
    public static Vector2 operator +(Vector2 lhs, Vector2 rhs) => new(lhs.X + rhs.X, lhs.Y + rhs.Y);
    public static Vector2 operator -(Vector2 lhs, Vector2 rhs) => new(lhs.X - rhs.X, lhs.Y - rhs.Y);
}