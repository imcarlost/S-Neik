using UnityEngine;

public class FoodAgent : MonoBehaviour {

    public GameObject foodPrefab;
    public GameObject spawnArea;
    private SpriteRenderer containerCollider;

    void Start() {
        containerCollider = spawnArea.GetComponent<SpriteRenderer>();
    }

    public void spawn() {
        Vector2 containerSize = containerCollider.transform.localScale;
        float x = (float)System.Math.Round(Random.Range(-containerSize.x / 2, containerSize.x / 2));
        float y = (float)System.Math.Round(Random.Range(-containerSize.y / 2, containerSize.y / 2));
        Vector2 containerPos = containerCollider.transform.position;
        Vector2 spawnPos = new Vector2(containerPos.x + x, containerPos.y + y);
        Instantiate(foodPrefab, spawnPos, Quaternion.identity);
    }
}
