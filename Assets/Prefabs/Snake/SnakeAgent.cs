using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeAgent : MonoBehaviour {

    public GameObject tailPrefab;

    private SnakeDirection requestedDirection;
    private SnakeDirection currentDirection;
    private SnakeCallback callback;
    private List<Transform> tail = new List<Transform>();
    private Vector2 lastHeadPosition;
    private bool growing = false;

    public void requestDirection(SnakeDirection direction) {
        requestedDirection = direction;
    }

    public void addCallback(SnakeCallback snakeCallback) {
        callback = snakeCallback;
    }

    private void changeDirection() {
        if (currentDirection == SnakeDirection.Up && requestedDirection == SnakeDirection.Down) return;
        if (currentDirection == SnakeDirection.Down && requestedDirection == SnakeDirection.Up) return;
        if (currentDirection == SnakeDirection.Left && requestedDirection == SnakeDirection.Right) return;
        if (currentDirection == SnakeDirection.Right && requestedDirection == SnakeDirection.Left) return;

        currentDirection = requestedDirection;
    }

    private void moveHead() {
        lastHeadPosition = transform.position;
        transform.Translate(currentDirection.toVector2());
    }

    private void addTailChild() {
        GameObject tailChild = (GameObject)Instantiate(tailPrefab, lastHeadPosition, Quaternion.identity);
        tail.Insert(0, tailChild.transform);
    }

    private void moveLastChild() {
        tail.Last().position = lastHeadPosition;
        tail.Insert(0, tail.Last());
        tail.RemoveAt(tail.Count - 1);
    }

    private void moveTail() {
        if (growing) {
            addTailChild();
            growing = false;
        } else if (tail.Count > 0) {
            moveLastChild();
        }
    }

    public void setTailSize(int size) {
        tail.Clear();
        for (int i = 0; i < size; i++) {
            addTailChild();
        }
    }

    public int getSize() {
        return tail.Count;
    }

    public void eat() {
        growing = true;
    }

    public void nextMovement() {
        changeDirection();
        moveHead();
        moveTail();
    }

    private void OnTriggerEnter2D(Collider2D itemCollided) {
        callback.snakeCollision(itemCollided);
    }
}

public interface SnakeCallback {
    void snakeCollision(Collider2D itemCollided);
}
