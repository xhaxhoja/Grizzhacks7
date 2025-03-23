using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject hostilePrefab;
    public float baseSpawnInterval = 2f;  // Base time between spawns
    public Vector2 spawnVelocity = new Vector2(0f, 3f);  // Default velocity
    public float minSpawnDistance = 1.5f;  // Minimum distance between objects to avoid clipping
    public float maxNearbyObjects = 3;  // Max number of objects close to each other before preventing spawn

    private float spawnTimer;  // Timer to manage spawn intervals
    private List<GameObject> activeHostiles = new List<GameObject>();  // List of currently active hostiles

    private void Start()
    {
        spawnTimer = Random.Range(0f, baseSpawnInterval);  // Start the timer with a random delay
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        // Adjust the spawn interval dynamically to avoid overcrowding (clipping)
        float adjustedSpawnInterval = GetDynamicSpawnInterval();

        // If it's time to spawn a new hostile object
        if (spawnTimer >= adjustedSpawnInterval)
        {
            spawnTimer = 0f;  // Reset spawn timer

            if (CanSpawnHere(transform.position))
            {
                // Spawn the hostile object
                GameObject hostile = Instantiate(hostilePrefab, transform.position, Quaternion.identity);

                // Set a random direction for the hostile object (1-4)
                int randomDirection = Random.Range(1, 5);  // Random direction between 1 and 4
                //hostile.GetComponent<HostileMovement>().direction = randomDirection;

                // Optionally, set a random velocity if desired
                hostile.GetComponent<HostileMovement>().speed = Random.Range(3f, 7f);

                activeHostiles.Add(hostile);
            }
        }

        // Remove destroyed hostiles from the list
        activeHostiles.RemoveAll(item => item == null);
    }

    private bool CanSpawnHere(Vector3 spawnPos)
    {
        int nearbyObjects = 0;

        foreach (GameObject hostile in activeHostiles)
        {
            if (Vector3.Distance(spawnPos, hostile.transform.position) < minSpawnDistance)
            {
                nearbyObjects++;
                if (nearbyObjects >= maxNearbyObjects) return false;  // Too many objects nearby, don't spawn
            }
        }
        return true;
    }

    private float GetDynamicSpawnInterval()
    {
        int nearbyObjects = 0;
        foreach (GameObject hostile in activeHostiles)
        {
            if (Vector3.Distance(transform.position, hostile.transform.position) < minSpawnDistance * 2)
            {
                nearbyObjects++;
            }
        }

        // Make the spawn interval longer if there are more objects close by
        return baseSpawnInterval + (nearbyObjects * 0.5f);
    }
}
