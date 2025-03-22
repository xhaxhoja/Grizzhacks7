using UnityEngine;

public class HostileMovement : MonoBehaviour
{
    public Vector2 velocity = new Vector2(3f, 0f); // Default velocity to move right

    private void Update()
    {
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
