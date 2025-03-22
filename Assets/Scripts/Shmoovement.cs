using UnityEngine;

public class Shmoovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed at which the object moves

    private void Update()
    {
        // Get horizontal and vertical input (WASD or arrow keys)
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Create a movement vector based on input
        Vector2 movement = new Vector2(moveX, moveY) * moveSpeed * Time.deltaTime;

        // Apply the movement to the object's position
        transform.Translate(movement);
    }
}