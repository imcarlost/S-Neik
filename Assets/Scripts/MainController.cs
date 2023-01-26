using System;
using System.Collections;
using UnityEngine;

public class MainController : MonoBehaviour, SnakeCallback {

    public SnakeAgent snake;
    public FoodAgent food;
    public float gameSpeed = 0.8f;
    public int initialSnakeLenght = 5;
    public GameState gameState = GameState.Wait;

    private void Start() {
        initializeGame();
    }

    private void Update() {
        handleStartListener();
        handleSnakeMovement();
        updateGameState();
    }

    private void initializeGame() {
        snake.setTailSize(initialSnakeLenght);
        snake.addCallback(this);
    }

    private void handleStartListener() {
        if (Input.GetMouseButtonDown(0) && gameState == GameState.Wait) {
            gameState = GameState.Begin;
        }
    }

    private void handleSnakeMovement() {
        if (Input.GetKey(KeyCode.W)) snake.requestDirection(SnakeDirection.Up);
        if (Input.GetKey(KeyCode.S)) snake.requestDirection(SnakeDirection.Down);
        if (Input.GetKey(KeyCode.A)) snake.requestDirection(SnakeDirection.Left);
        if (Input.GetKey(KeyCode.D)) snake.requestDirection(SnakeDirection.Right);
    }

    private void updateGameState() {
        switch (gameState) {
            case GameState.Begin:
                gameState = GameState.Running;
                food.spawn();
                StartCoroutine(SnakeMovement());
                break;
            case GameState.Gameover:
                //Show gameover
                gameState = GameState.Wait;
                break;
        }
    }

    private IEnumerator SnakeMovement() {
        do {
            snake.nextMovement();
            yield return new WaitForSeconds(gameSpeed);
        } while (gameState == GameState.Running);
    }

    public void snakeCollision(Collider2D itemCollided) {

        Debug.LogWarning(itemCollided.tag + " ");

        if (itemCollided.tag == "Food") {
            snake.eat();
            food.spawn();
            gameSpeed = 0.08f;
            Destroy(itemCollided.gameObject);
        }

        if (itemCollided.tag == "Tail" && snake.getSize() > initialSnakeLenght) {
            gameState = GameState.Gameover;
        }

        if (itemCollided.tag == "GameArea") {
            gameState = GameState.Gameover;
        }
    }

    //crear mapa procedural
}

public enum GameState {
    Wait,
    Begin,
    Running,
    Gameover
}
