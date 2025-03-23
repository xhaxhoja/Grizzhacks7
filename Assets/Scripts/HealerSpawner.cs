using UnityEngine;
using System.Collections.Generic;

public class HealerSpawner : MonoBehaviour
{
    public GameObject yellowduckPrefab;
    public float spawnRate = 3f;
    public float spawnRadius = 5f;
    public int maxYellowducks = 5;
    private float spawnTimer;
    private List<GameObject> activeYellowducks = new List<GameObject>();

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate && activeYellowducks.Count < maxYellowducks)
        {
            SpawnYellowduck();
            spawnTimer = 0f;
        }
        CleanupYellowducks();
    }

    void SpawnYellowduck()
    {
        Vector3 spawnPosition = transform.position + (Vector3)Random.insideUnitCircle * spawnRadius;
        GameObject yellowduck = Instantiate(yellowduckPrefab, spawnPosition, Quaternion.identity);
        activeYellowducks.Add(yellowduck);
    }

    void CleanupYellowducks()
    {
        activeYellowducks.RemoveAll(yellowduck => yellowduck == null);
    }
}