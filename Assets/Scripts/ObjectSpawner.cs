using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject hostilePrefab;  // The hostile object to spawn
    public float spawnInterval = 2f;  // Time interval between spawns
    public Vector2 spawnVelocity = new Vector2(3f, 0f); // Default velocity to move right
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f; // Reset the timer

            // Spawn hostile object at this spawner's position
            GameObject hostile = Instantiate(hostilePrefab, transform.position, Quaternion.identity);

            // Assign velocity directly
            HostileMovement hostileMovement = hostile.GetComponent<HostileMovement>();
            if (hostileMovement != null)
            {
                hostileMovement.velocity = spawnVelocity;
            }
        }
    }
}
