using UnityEngine;

public enum SnakeDirection {
    Up,
    Down,
    Left,
    Right
}

public static class SnakeDirectionExtension {
    public static Vector2 toVector2(this SnakeDirection value) {
        switch (value) {
            case SnakeDirection.Up:
                return Vector2.up;
            case SnakeDirection.Down:
                return Vector2.down;
            case SnakeDirection.Left:
                return Vector2.left;
            case SnakeDirection.Right:
                return Vector2.right;
            default:
                throw new System.ArgumentException();
        }
    }
}
