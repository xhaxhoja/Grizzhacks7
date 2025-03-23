using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject hostilePrefab;
    public float initialMinSpawnDelay = 8f;  // Initial minimum spawn delay in seconds
    public float initialMaxSpawnDelay = 10f;  // Initial maximum spawn delay in seconds
    public float scaledMinSpawnDelay = 3f;   // Scaled minimum spawn delay after 5 minutes
    public float scaledMaxSpawnDelay = 5f;   // Scaled maximum spawn delay after 5 minutes
    public float scaleTime = 5f * 60f;       // Time (in seconds) to scale the spawn delay (e.g., 5 minutes)
    float minSpawnDelay;
public float maxSpawnDelay; 
    private float spawnTimer;  // Timer to manage spawn intervals
    private float spawnDelay;  // Random spawn delay
    private List<GameObject> activeHostiles = new List<GameObject>();  // List of currently active hostiles
    private float gameStartTime;  // Time at which the game started

    private void Start()
    {
        gameStartTime = Time.time;  // Store the start time of the game

        // Set an initial random delay between spawns
        SetRandomSpawnDelay(initialMinSpawnDelay,initialMaxSpawnDelay);
        spawnTimer = 0f;  // Start the timer from 0
    }
    private void ScaleSpawnDelayBasedOnTime(float elapsedTime)
    {
        // If the game time exceeds the scaleTime (e.g., 5 minutes), scale the delay
        if (elapsedTime >= scaleTime)
        {
            // Use the scaled min and max spawn delays after the scaling period
            minSpawnDelay = scaledMinSpawnDelay;
            maxSpawnDelay = scaledMaxSpawnDelay;
        }
        else
        {
            // Interpolate between the initial and scaled delays based on elapsed time
            float scaleFactor = elapsedTime / scaleTime;
            minSpawnDelay = Mathf.Lerp(initialMinSpawnDelay, scaledMinSpawnDelay, scaleFactor);
            maxSpawnDelay = Mathf.Lerp(initialMaxSpawnDelay, scaledMaxSpawnDelay, scaleFactor);
        }

        //Debug.Log("Elapsed Time: " + elapsedTime + " | Min Spawn Delay: " + InitialminSpawnDelay + " | Max Spawn Delay: " + maxSpawnDelay);
    }
    private void Update()
    {
        // Increase the spawnTimer by the time that has passed since the last frame
        spawnTimer += Time.deltaTime;

        // Calculate time elapsed since the start of the game
        float elapsedTime = Time.time - gameStartTime;

        // Scale spawn delay based on time
        ScaleSpawnDelayBasedOnTime(elapsedTime);

        // If the timer has reached or exceeded the spawn delay
        if (spawnTimer >= spawnDelay)
        {
            // Reset the spawn timer
            spawnTimer = 0f;

            // Spawn the hostile object
            SpawnHostile();

            // After spawning, set a new random spawn delay
            SetRandomSpawnDelay(minSpawnDelay,maxSpawnDelay);
        }
    }

    private void SpawnHostile()
    {
        if (hostilePrefab != null)
        {
            // Spawn the hostile object
            GameObject hostile = Instantiate(hostilePrefab, transform.position, Quaternion.identity);
            hostile.tag = "idk";  // Change the tag of the spawned object

            // Get the HostileMovement component safely
            HostileMovement hostileMovement = hostile.GetComponent<HostileMovement>();
            if (hostileMovement != null)
            {
                // Optionally, set a random velocity if desired
                hostileMovement.speed = Random.Range(3f, 5f);

                activeHostiles.Add(hostile);
            }
            else
            {
                Debug.LogError("Hostile object spawned without HostileMovement component!");
                Destroy(hostile);  // Destroy the object if it doesn't have the necessary component
            }
        }
        else
        {
            Debug.LogError("Hostile prefab is not assigned in the Inspector.");
        }
    }

    // Method to set a new random spawn delay between each spawn
    private void SetRandomSpawnDelay(float min, float max)
    {
        // Random delay between the specified range
        spawnDelay = Random.Range(min, max);
        Debug.Log("Next spawn delay: " + spawnDelay);  // Debug line to confirm the delay
    }
}
