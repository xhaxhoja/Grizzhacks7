using UnityEngine;

public class HostileMovement : MonoBehaviour
{
    public int direction = 1;  // 1: Up, 2: Down, 3: Left, 4: Right
    public float speed = 3f;   // Movement speed
    private Vector2 movementDirection;

    private void Start()
    {
        // Set the initial movement direction based on the input value (1-4)
        SetMovementDirection(direction);
    }

    private void Update()
    {
        // Move the hostile object in the assigned direction
        transform.position += (Vector3)movementDirection * speed * Time.deltaTime;
    }

    private void SetMovementDirection(int dir)
    {
        // Set the movement direction based on the input value (1-4)
        switch (dir)
        {
            case 1:  // Move up
                movementDirection = Vector2.up;
                break;
            case 2:  // Move down
                movementDirection = Vector2.down;
                break;
            case 3:  // Move left
                movementDirection = Vector2.left;
                break;
            case 4:  // Move right
                movementDirection = Vector2.right;
                break;
            default:
                movementDirection = Vector2.zero;  // No movement if invalid direction
                break;
        }
    }

    // Handle collision detection and self-destruction
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If this object collides with any other object, destroy both
        if (gameObject.CompareTag("IndestructibleHostile"))
        {
            // You can log or handle other behaviors if needed
            Debug.Log("Indestructible hostile collided, but will not be destroyed.");
            return; // Don't do anything else to this object
        }

        // If it's not an indestructible object, destroy it on collision
        if (gameObject.CompareTag("IndestructibleHostile"))
        {
            Destroy(gameObject);  // Destroy it as usual for other objects
        }
    }

}
