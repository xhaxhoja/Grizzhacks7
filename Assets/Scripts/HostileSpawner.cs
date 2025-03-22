using UnityEngine;

public class HostileSpawner : MonoBehaviour
{
    public GameObject hostilePrefab;  // The hostile object to spawn
    public float spawnInterval = 2f;  // Time between spawns
    public float moveSpeed = 3f;     // Speed at which the hostile object moves
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        InvokeRepeating("SpawnHostile", 0f, spawnInterval);  // Repeatedly spawn hostiles at set intervals
    }

    private void SpawnHostile()
    {
        // Calculate spawn position just outside the camera bounds
        float spawnX = 0f;
        float spawnY = 0f;

        // Randomly choose whether the object will spawn from the left, right, top, or bottom
        if (Random.value > 0.5f) // Horizontal (left or right)
        {
            spawnX = Random.value > 0.5f ? mainCamera.orthographicSize * mainCamera.aspect * 1.5f : -mainCamera.orthographicSize * mainCamera.aspect * 1.5f;
            spawnY = Random.Range(-mainCamera.orthographicSize, mainCamera.orthographicSize); // Random vertical position inside the camera view
        }
        else // Vertical (top or bottom)
        {
            spawnY = Random.value > 0.5f ? mainCamera.orthographicSize * 1.5f : -mainCamera.orthographicSize * 1.5f;
            spawnX = Random.Range(-mainCamera.orthographicSize * mainCamera.aspect, mainCamera.orthographicSize * mainCamera.aspect); // Random horizontal position inside the camera view
        }

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

        // Instantiate hostile object
        GameObject hostile = Instantiate(hostilePrefab, spawnPosition, Quaternion.identity);

        // Random movement direction (angle in degrees)
        float randomAngle = Random.Range(0f, 360f);
        Vector3 direction = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0).normalized;

        // Assign the movement script to the spawned hostile
        hostile.AddComponent<HostileMovement>().Initialize(direction, moveSpeed, mainCamera);
    }
}
