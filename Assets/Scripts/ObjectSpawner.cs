using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject hostilePrefab;
    public float baseSpawnInterval = 10f;  // Base time between spawns
    public Vector2 spawnVelocity = new Vector2(0f, 3f);  // Default velocity
    public float minSpawnDistance = 2.5f;  // Minimum distance between objects to avoid clipping
    public float maxNearbyObjects =1;  // Max number of objects close to each other before preventing spawn

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



                if (hostilePrefab != null)
                {
                    // Spawn the hostile object
                    GameObject hostile = Instantiate(hostilePrefab, transform.position, Quaternion.identity);
                    hostile.tag = "idk";  // Change the tag of the spawned object
                    // Get the HostileMovement component safely
                    HostileMovement hostileMovement = hostile.GetComponent<HostileMovement>();
                    if (hostileMovement != null)
                    {
                        // Set a random direction for the hostile object (1-4)
                        int randomDirection = Random.Range(1, 5);
                        //hostileMovement.direction = randomDirection;

                        // Optionally, set a random velocity if desired
                        hostileMovement.speed = Random.Range(3f,5f);

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

                // Spawn the hostile object
             

                // Get the HostileMovement component safely
            
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
            if (hostile != null && Vector3.Distance(spawnPos, hostile.transform.position) < minSpawnDistance)
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
            if (hostile != null && Vector3.Distance(transform.position, hostile.transform.position) < minSpawnDistance * 2)
            {
                nearbyObjects++;
            }
        }

        // Increase spawn interval more dramatically based on nearby objects
        return baseSpawnInterval * (1f + (nearbyObjects * 0.3f));
    }
}
