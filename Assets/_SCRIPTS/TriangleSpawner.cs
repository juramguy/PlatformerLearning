using UnityEngine;

public class TriangleSpawner : MonoBehaviour
{

    public GameObject trianglePrefab;
    public float spawnInterval = 1.5f;
    public float spawnHeight = 6f;
    public float spawnXMin = -8f;
    public float spawnXMax = 8f;

    private float timer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnTriangle();
            timer = 0;
        }

    }

    void SpawnTriangle()
    {
        float randomX = Random.Range(spawnXMin, spawnXMax);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y + spawnHeight, 0f);
        Instantiate(trianglePrefab, spawnPosition, Quaternion.identity);
    }



}
